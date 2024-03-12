using IITMS.SQLServer.SQLDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;

namespace IITMS.UAIMS.BusinessLayer.BusinessLogic
{
    public class WalletController
    {
        private string _UAIMS_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

        public DataSet getWalletBalance(int idno)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_IDNO", idno);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_STUDENT_WALLET_CLOSING_BALANCE", objParams);

                //ds = objSQLHelper.ExecuteDataSetSP("PDP_SHOWDATA_REFUND_POLICY", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.WalletController.getWalletBalance-> " + ex.ToString());
            }
            return ds;
        }

        public DataSet getWalletLedger(int idno, DateTime FromDate, DateTime ToDate, string TranMode, string PaymentStatusID, string TRANTYPE)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[6];
                objParams[0] = new SqlParameter("@P_IDNO", idno);
                objParams[1] = new SqlParameter("@P_FromDate", FromDate);
                objParams[2] = new SqlParameter("@P_ToDate", ToDate);
                objParams[3] = new SqlParameter("@P_TranMode", TranMode);
                objParams[4] = new SqlParameter("@P_PaymentStatusID", PaymentStatusID);
                objParams[5] = new SqlParameter("@P_TRANTYPE", TRANTYPE);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_STUDENT_WALLET_LEDGER", objParams);

                //ds = objSQLHelper.ExecuteDataSetSP("PDP_SHOWDATA_REFUND_POLICY", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.WalletController.getWalletLedger-> " + ex.ToString());
            }
            return ds;
        }

        public string AddStudentOfflineWalletPayment(StudentWallet objW)
        {
            string retStatus = string.Empty;//Convert.ToString(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;

                //Add New User
                objParams = new SqlParameter[19];

                objParams[0] = new SqlParameter("@P_WALLET_ID", objW.wallet_id);
                objParams[1] = new SqlParameter("@P_IDNO", objW.idno);
                objParams[2] = new SqlParameter("@P_TRAN_AMT", objW.tran_amt);
                objParams[3] = new SqlParameter("@P_TRAN_TYPE", objW.tran_type);
                objParams[4] = new SqlParameter("@P_TRAN_MODE", objW.tran_mode);
                objParams[5] = new SqlParameter("@P_CLOSE_BAL", objW.close_bal);
                objParams[6] = new SqlParameter("@P_REMARK", objW.remark);
                objParams[7] = new SqlParameter("@P_RECON", objW.recon);
                objParams[8] = new SqlParameter("@P_RECON_BY", objW.recon_by);
                objParams[9] = new SqlParameter("@P_TRANSACTIONID", objW.transactionid);
                objParams[10] = new SqlParameter("@P_BANKNO", objW.bankno);
                objParams[11] = new SqlParameter("@P_BRANCHNAME", objW.branchname);
                objParams[12] = new SqlParameter("@P_BRANCHADDRESS", objW.branchaddress);
                objParams[13] = new SqlParameter("@P_DOC_FILENAME", objW.doc_filename);
                objParams[14] = new SqlParameter("@P_SERVICE_CHARGE", objW.service_charge);
                objParams[15] = new SqlParameter("@P_ORDER_ID", objW.order_id);
                objParams[16] = new SqlParameter("@P_AP_SECUREHASH", objW.ap_securehash);
                objParams[17] = new SqlParameter("@P_SEMESTERNO", objW.semesterno);

                objParams[18] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[18].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_ACD_STUDENT_WALLET", objParams, true);//) != null)
                // object ret  = 23;
                if (retStatus != null && retStatus.ToString() != "-99" && retStatus.ToString() != "-1001")
                {
                    // retStatus = "1";
                    retStatus = Convert.ToString(CustomStatus.RecordSaved);
                }
                else
                {
                    //   retStatus = "-99";
                    retStatus = Convert.ToString(CustomStatus.Error);
                }
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToString(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.PDPStudentPaymentController.AddPDPStudentPayment-> " + ex.ToString());
            }
            return retStatus;
        }

        public string AddStudentOnlineWalletPayment(StudentWallet objW)
        {
            string retStatus = string.Empty;//Convert.ToString(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;

                //Add New User
                objParams = new SqlParameter[12];

                objParams[0] = new SqlParameter("@P_WALLET_ID", objW.wallet_id);
                objParams[1] = new SqlParameter("@P_IDNO", objW.idno);
                objParams[2] = new SqlParameter("@P_TRAN_AMT", objW.tran_amt);
                objParams[3] = new SqlParameter("@P_TRAN_TYPE", objW.tran_type);
                objParams[4] = new SqlParameter("@P_TRAN_MODE", objW.tran_mode);
                objParams[5] = new SqlParameter("@P_CLOSE_BAL", objW.close_bal);
                objParams[6] = new SqlParameter("@P_REMARK", objW.remark);
                objParams[7] = new SqlParameter("@P_RECON", objW.recon);
                objParams[8] = new SqlParameter("@P_RECON_BY", objW.recon_by);
                objParams[9] = new SqlParameter("@P_SERVICE_CHARGE", objW.service_charge);
                objParams[10] = new SqlParameter("@P_ORDER_ID", objW.order_id);         
                objParams[11] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[11].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_ACD_STUDENT_WALLET_TEMP", objParams, true);//) != null)
                // object ret  = 23;
                if (retStatus != null && retStatus.ToString() != "-99" && retStatus.ToString() != "-1001")
                {
                    // retStatus = "1";
                    retStatus = Convert.ToString(CustomStatus.RecordSaved);
                }
                else
                {
                    //   retStatus = "-99";
                    retStatus = Convert.ToString(CustomStatus.Error);
                }
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToString(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.PDPStudentPaymentController.AddPDPStudentPayment-> " + ex.ToString());
            }
            return retStatus;
        }

        public int OnlinePayment(string order_id, string transaction_id, string response_code, string hash)
        {
            int countrno = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSqlhelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] sqlparam = null;
                {
                    sqlparam = new SqlParameter[5];
                    sqlparam[0] = new SqlParameter("@P_ORDER_ID", order_id);
                    sqlparam[1] = new SqlParameter("@P_TRANSACTION_ID", transaction_id);
                    sqlparam[2] = new SqlParameter("@P_TRANSACTIONSTATUS", response_code);
                    sqlparam[3] = new SqlParameter("@P_HASH", hash);
                    sqlparam[4] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                    sqlparam[4].Direction = ParameterDirection.Output;
                };
                //sqlparam[sqlparam.Length - 1].Direction = ParameterDirection.Output;     
                object studid = objSqlhelper.ExecuteNonQuerySP("PKG_ACD_WALLET_STUDENT_ONLINE_PAYMENT", sqlparam, true);

                if (Convert.ToInt32(studid) == -99)
                {
                    countrno = Convert.ToInt32(CustomStatus.TransactionFailed);
                }
                else
                    countrno = Convert.ToInt32(CustomStatus.RecordUpdated);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.WalletController.OnlinePayment() --> " + ex.Message + " " + ex.StackTrace);
            }
            return countrno;
        }

        public DataSet GetStudentPaymentDetails(int idno, int courseid, string orderid)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = new SqlParameter[2];
                objParams[0] = new SqlParameter("@P_IDNO", idno);
                objParams[1] = new SqlParameter("@P_ORDER_ID", orderid);
                ds = objDataAccess.ExecuteDataSetSP("PKG_ACD_WALLET_GET_STUDENT_PAYMENT_DETAILS", objParams);
            }
            catch (Exception ex)
            {
                return ds;
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.WalletController.GetStudentPaymentDetails-> " + ex.ToString());
            }
            return ds;
        }


    }
}
