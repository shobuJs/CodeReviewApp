//======================================================================================
// PROJECT NAME  : UAIMS
// MODULE NAME   : ACADEMIC
// PAGE NAME     : GRADE ENTRY CONTROLLER
// CREATION DATE : 20-JUNE-2014
// CREATED BY    : VIJAY PAUNIKAR
// MODIFIED DATE :
// MODIFIED DESC :
//======================================================================================

using IITMS;
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities.Academic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace BusinessLogicLayer.BusinessLogic.Academic
{
    public class GradeEntryController
    {
        private string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

        public int AddGradeEntry(GradeEntry objGradeEntry, int Active, int userno)
        {
            int status = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_GRADE", objGradeEntry.Grade),
                    new SqlParameter("@P_GRADE_POINT", objGradeEntry.GradePoint),
                    new SqlParameter("@P_COLLEGE_CODE", objGradeEntry.CollegeCode),
                    new SqlParameter("@P_MAXMARK", objGradeEntry.MaxMark),
                    new SqlParameter("@P_MINMARK", objGradeEntry.MinMark),
                    new SqlParameter("@P_GRADE_DESC", objGradeEntry.GradeDesc),
                    new SqlParameter("@P_GRADE_TYPE", objGradeEntry.GradeType),
                    new SqlParameter("@P_CREATED_BY", userno),
                     new SqlParameter("@P_STATUS", Active),
                    new SqlParameter("@P_GRADENO", status)
                };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_GRADE_INSERT", sqlParams, true);
                //status = Convert.ToInt32(obj);

                if (Convert.ToInt32(status) == -1001)
                    status = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    status = Convert.ToInt32(CustomStatus.RecordSaved);
            }
            catch (Exception ex)
            {
                status = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.GradeEntryController.AddGradeEntry() --> " + ex.Message + " " + ex.StackTrace);
            }
            return status;
        }

        public int UpdateGradeEntry(GradeEntry objGradeEntry, int Active, int userno)
        {
            int status;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_GRADE", objGradeEntry.Grade),
                    new SqlParameter("@P_GRADEPOINT", objGradeEntry.GradePoint),
                    new SqlParameter("@P_MODIFIED_BY", userno),
                    new SqlParameter("@P_COLLEGE_CODE", objGradeEntry.CollegeCode),
                    new SqlParameter("@P_MAXMARK", objGradeEntry.MaxMark),
                    new SqlParameter("@P_MINMARK", objGradeEntry.MinMark),
                    new SqlParameter("@P_DEGREENO", objGradeEntry.MinMark),
                    new SqlParameter("@P_GRADE_TYPE", objGradeEntry.GradeType),
                    new SqlParameter("@P_DESC_GRADE", objGradeEntry.GradeDesc),
                    new SqlParameter("@P_STATUS", Active),
                    new SqlParameter("@P_GRADENO",objGradeEntry.GradeNo)
                };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_GRADE_ENTRY_UPDATE", sqlParams, true);

                if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001")
                    status = Convert.ToInt32(CustomStatus.RecordUpdated);
                else
                    status = Convert.ToInt32(CustomStatus.Error);
            }
            catch (Exception ex)
            {
                status = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.GradeEntryController.UpdateGradeEntry() --> " + ex.Message + " " + ex.StackTrace);
            }
            return status;
        }

        public DataSet GetAllGradeEntry()
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = new SqlParameter[0];

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GRADE_ENTRY_GET_ALL", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.GradeEntryController.GetAllGradeEntry() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public SqlDataReader GetGradeEntryByGradeNo(int gradeNo)
        {
            SqlDataReader dr = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = new SqlParameter[] { new SqlParameter("@P_OUTPUT", gradeNo) };

                dr = objSQLHelper.ExecuteReaderSP("PKG_ACD_GET_GRADE_BY_GRADE_NO", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.GradeEntryController.GetGradeEntryByGradeNo() --> " + ex.Message + " " + ex.StackTrace);
            }
            return dr;
        }
    }
}