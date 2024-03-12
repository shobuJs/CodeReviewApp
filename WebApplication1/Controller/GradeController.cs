using IITMS;
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities;
using System;
using System.Data;
using System.Data.SqlClient;

namespace BusinessLogicLayer.BusinessLogic
{
    public class GradeController
    {
        private string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

        public int AddGrade(Grade objGrade)
        {
            int status = -99;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                    //new SqlParameter("@P_GRADE_TYPE",objGrade.GradeType),
                    new SqlParameter("@P_GRADE_NAME", objGrade.GradeName),
                    new SqlParameter("@P_COLLEGE_CODE", objGrade.CollegeCode),
                    new SqlParameter("@P_OUTPUT", status)
                };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_GRADE_TYPE_INSERT", sqlParams, true);
                status = (Int32)obj;       //Added By Abhinay Lad [14-06-2019]
            }
            catch (Exception ex)
            {
                status = -99;            //Added By Abhinay Lad [14-06-2019]
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.BatchController.AddBatch() --> " + ex.Message + " " + ex.StackTrace);
            }
            return status;
        }

        public int UpdateGrade(Grade objGrade)
        {
            int status = -99;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                    //new SqlParameter("@P_BATCHNAME",objBatch.BatchName),
                    new SqlParameter("@P_GRADE_TYPE_NAME", objGrade.GradeName),
                    new SqlParameter("@P_COLLEGE_CODE", objGrade.CollegeCode),
                    new SqlParameter("@P_GRADE_ID", objGrade.GradeType),
                    new SqlParameter("@P_OUTPUT",status)
                };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_GRADE_TYPE_UPDATE", sqlParams, true);
                status = (Int32)obj; //Added By Abhinay Lad [14-06-2019]
            }
            catch (Exception ex)
            {
                status = -99; //Added By Abhinay Lad [14-06-2019]
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.BatchController.UpdateBatch() --> " + ex.Message + " " + ex.StackTrace);
            }
            return status;
        }

        public DataSet GetAllGrade()
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = new SqlParameter[0];

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GRADE_GET_ALL", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.BatchController.GetAllBatch() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public SqlDataReader GetGradeTypeNo(int gradeNo)
        {
            SqlDataReader dr = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = new SqlParameter[] { new SqlParameter("@P_OUTPUT", gradeNo) };

                dr = objSQLHelper.ExecuteReaderSP("PKG_ACD_GRADE_GET_BY_TYPE_NO", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.BatchController.GetBatchByNo() --> " + ex.Message + " " + ex.StackTrace);
            }
            return dr;
        }

        public int InsertGradeScheme(string xml)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);

            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = null;
                //Add
                objParams = new SqlParameter[2];
                objParams[0] = new SqlParameter("@P_XML", xml);
                objParams[1] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[1].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INSERT_GRADESCHEME", objParams, false);

                if (ret != null && ret.ToString() != "-99" && ret.ToString() != "-1001")
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                else
                    retStatus = Convert.ToInt32(CustomStatus.Error);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.GradeController.InsertGradeScheme-> " + ex.ToString());
            }
            return retStatus;
        }

        public int UpdateGradeScheme(DataTable GRADESCHEME)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);

            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = null;
                //Add
                objParams = new SqlParameter[2];
                objParams[0] = new SqlParameter("@P_GRADESCHEME", GRADESCHEME);
                objParams[1] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[1].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_UPDATE_GRADESCHEME", objParams, false);

                if (ret != null && ret.ToString() != "-99" && ret.ToString() != "-1001")
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                else
                    retStatus = Convert.ToInt32(CustomStatus.Error);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.GradeController.UpdateGradeScheme-> " + ex.ToString());
            }
            return retStatus;
        }

        public int Insert_Update_Grading_Scheme_Allotment(Grade objGrade, string SchemeNo)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[8];
                objParams[0] = new SqlParameter("@P_SRNO", objGrade.SrNo);
                objParams[1] = new SqlParameter("@P_COLLEGEID", objGrade.CollegeId);
                objParams[2] = new SqlParameter("@P_DEGREENO", objGrade.DegreeNo);
                objParams[3] = new SqlParameter("@P_BRANCHNO", objGrade.BranchNo);
                objParams[4] = new SqlParameter("@P_SCHEMENO", SchemeNo);
                objParams[5] = new SqlParameter("@P_SCHEMEID", objGrade.SchemeId);
                objParams[6] = new SqlParameter("@P_USER", objGrade.User);
                objParams[7] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[7].Direction = ParameterDirection.Output;
                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INSERT_UPDATE_GRADING_SCHEME_ALLOTMENT", objParams, true);

                if (Convert.ToInt32(ret) == 1)
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                //else if (Convert.ToInt32(ret) == 2)
                //{
                //    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                //}
                else
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.GradeController.Insert_Update_Grading_Scheme_Allotment-> " + ex.ToString());
            }
            return retStatus;
        }

        public DataSet GetAllotment()
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = new SqlParameter[0];

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_GRADING_SCHEME_ALLOTMENT", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.GradeController.GetAllotment() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public int DeleteAllotment(int SrNo, int SchemeNo)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[3];
                objParams[0] = new SqlParameter("@P_SRNO", SrNo);
                objParams[1] = new SqlParameter("@P_SCHEME_NO", SchemeNo);
                objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[2].Direction = ParameterDirection.Output;
                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_DELETE_GRADING_SCHEME_ALLOTMENT", objParams, true);

                if (Convert.ToInt32(ret) == 3)
                    retStatus = Convert.ToInt32(CustomStatus.RecordDeleted);
                else
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.GradeController.DeleteAllotment-> " + ex.ToString());
            }
            return retStatus;
        }

        public int DeleteGradeScheme(int GradeNo)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[2];
                objParams[0] = new SqlParameter("@P_GRADENO", GradeNo);
                objParams[1] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[1].Direction = ParameterDirection.Output;
                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_DELETE_GRADE_SCHEME", objParams, true);

                if (Convert.ToInt32(ret) == 3)
                    retStatus = Convert.ToInt32(CustomStatus.RecordDeleted);
                else
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.GradeController.DeleteGradeScheme-> " + ex.ToString());
            }
            return retStatus;
        }
    }
}