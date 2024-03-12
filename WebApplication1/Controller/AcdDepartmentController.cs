//======================================================================================
// PROJECT NAME  : UAIMS
// MODULE NAME   : ACADEMIC
// PAGE NAME     : DEPT MASTER CONTROLLER
// CREATION DATE : 30-MAY-2019
// CREATED BY    : IRFAN SHAIKH
// MODIFIED DATE :
// MODIFIED DESC :
//======================================================================================

using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using System;
using System.Data;
using System.Data.SqlClient;

namespace IITMS.UAIMS.BusinessLayer.BusinessLogic
{
    public class AcdDepartmentController
    {
        private string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

        public int AddOrUpdateDept(Department objDept)
        {
            Common objCommon = new Common();
            int status = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_DEPTCODE", objDept.DeptCode),
                    new SqlParameter("@P_DEPTNAME", objDept.DeptName),
                    new SqlParameter("@P_COLLEGE_CODE", objDept.CollegeCode),
                    new SqlParameter("@P_DOJ", objDept.JoinDate),
                    new SqlParameter("@P_HODUANO", objDept.HodUaNo),
                    new SqlParameter("@P_DEPTNO", objDept.DeptNo)
                };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

                int DNo = objCommon.LookUp("ACD_DEPARTMENT", "DEPTNO", "DEPTNO=" + objDept.DeptNo) == string.Empty ? 0 : Convert.ToInt32(objCommon.LookUp("ACD_DEPARTMENT", "DEPTNO", "DEPTNO=" + objDept.DeptNo));

                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_DEPT_INSERT", sqlParams, true);

                if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001" && DNo == 0)
                    status = Convert.ToInt32(CustomStatus.RecordSaved);
                else if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001" && DNo != 0)
                    status = Convert.ToInt32(CustomStatus.RecordUpdated);
                else
                    status = Convert.ToInt32(CustomStatus.Error);
            }
            catch (Exception ex)
            {
                status = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AcdDepartmentController.AddOrUpdateDept() --> " + ex.Message + " " + ex.StackTrace);
            }
            return status;
        }

        public DataSet GetAllDept()
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = new SqlParameter[0];

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_DEPT_GET_ALL", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AcdDepartmentController.GetAllDept() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public SqlDataReader GetDeptByNo(int DeptNo)
        {
            SqlDataReader dr = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = new SqlParameter[] { new SqlParameter("@P_DEPTNO", DeptNo) };

                dr = objSQLHelper.ExecuteReaderSP("PKG_ACAD_GET_DEPT_BY_NO", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AcdDepartmentController.GetDeptByNo() --> " + ex.Message + " " + ex.StackTrace);
            }
            return dr;
        }
    }
}