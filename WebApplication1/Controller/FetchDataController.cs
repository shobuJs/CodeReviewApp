using IITMS.SQLServer.SQLDAL;
using System;
using System.Data;
using System.Data.SqlClient;

namespace IITMS.UAIMS.BusinessLayer.BusinessLogic
{
    public class FetchDataController : IDisposable
    {
        private string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

        public DataSet GetStudentData(int BRANCHNO, int DEGREENO, string REGDATE, int admBatch)
        {
            DataSet ds = new DataSet();
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[4];
                objParams[0] = new SqlParameter("@P_DEGREENO", DEGREENO);
                objParams[1] = new SqlParameter("@P_BRANCHNO", BRANCHNO);
                objParams[2] = new SqlParameter("@P_APPDATE", REGDATE);
                objParams[3] = new SqlParameter("@P_ADMBATCH", admBatch);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_FETCH_USER_DETAILS_EXCEL", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.BatchController.GetBatchByNo() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public DataSet GetApplicantListInBranchPrefOrder(int DEGREENO, int admBatch)
        {
            DataSet ds = new DataSet();
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = new SqlParameter[] {
                new SqlParameter("@P_DEGREENO",DEGREENO),
                new SqlParameter("@P_ADMBATCH",admBatch),
                };

                //ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_STUDENT_LIST_WITH_COURSE_CODE", objParams);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_ONLINE_ADMISSION_REG_DATA_DEGREEWISE", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.BatchController.GetBatchByNo() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public DataSet GetAppliedStudentData(int DEGREENO, int BRANCHNO, string AppDate, int admBatch)
        {
            DataSet ds = new DataSet();
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[4];

                if (DEGREENO > 0)
                {
                    objParams[0] = new SqlParameter("@P_DEGREENO", DEGREENO);
                }
                else
                {
                    objParams[0] = new SqlParameter("@P_DEGREENO", DBNull.Value);
                }
                if (BRANCHNO > 0)
                {
                    objParams[1] = new SqlParameter("@P_BRANCHNO", BRANCHNO);
                }
                else
                {
                    objParams[1] = new SqlParameter("@P_BRANCHNO", DBNull.Value);
                }
                //if (AppDate != DateTime.MinValue)
                if (AppDate != string.Empty)
                {
                    objParams[2] = new SqlParameter("@P_APPDATE", AppDate);
                }
                else
                {
                    objParams[2] = new SqlParameter("@P_APPDATE", DBNull.Value);
                }
                if (admBatch > 0)
                {
                    objParams[3] = new SqlParameter("@P_ADMBATCH", admBatch);
                }
                else
                {
                    objParams[3] = new SqlParameter("@P_ADMBATCH", DBNull.Value);
                }
                ds = objSQLHelper.ExecuteDataSetSP("PKG_FETCH_USER_DETAILS", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.BatchController.GetAppliedStudentData() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public DataSet GetUserData(int AdmBatch, int Degreeno, int Branchno)
        {
            DataSet ds = new DataSet();
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = new SqlParameter[] { new SqlParameter("@P_ADMBATCH", AdmBatch),
                new SqlParameter("@P_DEGREENO",Degreeno),
                new SqlParameter("@P_BRANCHNO",Branchno)};

                ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_USER_DETAILS_FOR_EXCELSHEET", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.BatchController.GetUserData() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public DataSet GetStudentListPaymentDetails(int BRANCHNO, int DEGREENO, string REGDATE, int admBatch, int reportType)
        {
            DataSet ds = new DataSet();
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = new SqlParameter[] {
                new SqlParameter("@P_DEGREENO",DEGREENO),
                new SqlParameter("@P_BRANCHNO", BRANCHNO),
                new SqlParameter("@P_APPDATE",REGDATE),
                new SqlParameter("@P_ADMBATCH",admBatch),
                new SqlParameter("@P_REPORTTYPE",reportType),
                };

                //ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_STUDENT_LIST_WITH_COURSE_CODE", objParams);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_FETCH_USER_DETAILS_EXCEL3", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.BatchController.GetBatchByNo() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public DataSet GetStudentListDatewise(int BRANCHNO, int DEGREENO, string REGDATE, int admBatch)
        {
            DataSet ds = new DataSet();
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = new SqlParameter[] {
                new SqlParameter("@P_DEGREENO",DEGREENO),
                new SqlParameter("@P_BRANCHNO", BRANCHNO),
                new SqlParameter("@P_APPDATE",REGDATE),
                new SqlParameter("@P_ADMBATCH",admBatch)
                };

                //ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_STUDENT_LIST_WITH_COURSE_CODE", objParams);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_FETCH_USER_DETAILS_EXCEL2", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.BatchController.GetBatchByNo() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public int UnlockConfirmStatus(int userno, int unlockby, DateTime uldate, string ipaddress)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = new SqlParameter[]
                        {
                             new SqlParameter("@P_USERNO",userno),
                             new SqlParameter("@P_UNLOKED_BY",unlockby),
                             new SqlParameter("@P_UNLOCK_DATE",uldate),
                             new SqlParameter("@P_IPADDRESS",ipaddress),
                             new SqlParameter("@P_OUT",SqlDbType.Int)
                        };
                objParams[objParams.Length - 1].Direction = ParameterDirection.Output;
                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_UPDATE_CONFIRM_STATUS", objParams, true);

                if (ret.ToString() == "1" && ret != null)
                {
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                }
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentController.AddCopyCase --> " + ex.ToString());
            }
            return retStatus;
        }

        public DataSet GetStandardFeesData(string ReceiptType, int DEGREENO, int BATCHNO, int PAYTYPENO, int COLLEGE_ID, int BRANCHNO)
        {
            DataSet ds = new DataSet();
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[6];
                objParams[0] = new SqlParameter("@P_RECIEPT_CODE", ReceiptType);
                objParams[1] = new SqlParameter("@P_DEGREENO", DEGREENO);
                objParams[2] = new SqlParameter("@P_BATCHNO", BATCHNO);
                objParams[3] = new SqlParameter("@P_PAYTYPENO", PAYTYPENO);
                objParams[4] = new SqlParameter("@P_COLLEGE_ID", COLLEGE_ID);
                objParams[5] = new SqlParameter("@P_BRANCHNO", BRANCHNO);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_FEE_DEFINITION_REPORT", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.BatchController.GetBatchByNo() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        private bool Disposed = false;

        ~FetchDataController()
        {
            //Destructor Called
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Dispose(bool disposing)
        {
            if (!Disposed)
            {
                if (disposing)
                {
                    //Called From Dispose
                    //Clear all the managed resources here
                }
                else
                {
                    //Clear all the unmanaged resources here
                }
                Disposed = true;
            }
        }
    }
}