using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using System;
using System.Data;
using System.Data.SqlClient;

namespace IITMS.UAIMS.BusinessLayer.BusinessLogic
{
    public class DefineTotalCreditController
    {
        private string _UAIMS_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

        public int AddCredit(DefineCreditLimit dci, int College_id, int Module_reg)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;

                //Add New Registered Subject Details
                objParams = new SqlParameter[37];

                objParams[0] = new SqlParameter("@P_SESSIONNO", dci.SESSION);
                objParams[1] = new SqlParameter("@P_SESSIONNAME", dci.SESSIONNAME);
                objParams[2] = new SqlParameter("@P_STUDENTTYPE", dci.STUDENT_TYPE);
                objParams[3] = new SqlParameter("@P_FROM_CGPA", dci.FROM_CGPA);
                objParams[4] = new SqlParameter("@P_TO_CGPA", dci.TO_CGPA);
                objParams[5] = new SqlParameter("@P_ADDITIONAL_COURSE", dci.ADDITIONAL_COURSE);
                objParams[6] = new SqlParameter("@P_ADM_TYPE", dci.ADM_TYPE);
                objParams[7] = new SqlParameter("@P_DEGREE_TYPE", dci.DEGREE_TYPE);
                objParams[8] = new SqlParameter("@P_FROM_CREDIT", dci.FROM_CREDIT);
                objParams[9] = new SqlParameter("@P_TO_CREDIT", dci.TO_CREDIT);
                objParams[10] = new SqlParameter("@P_MAX_SCHEME_LIMIT", dci.MAX_SCHEMELIMIT);
                objParams[11] = new SqlParameter("@P_MIN_SCHEME_LIMIT", dci.MIN_SCHEMELIMIT);
                objParams[12] = new SqlParameter("@P_Semester", dci.Semester);
                objParams[13] = new SqlParameter("@P_Semester_Text", dci.Semester_Text);
                objParams[14] = new SqlParameter("@P_DEGREENO", dci.DEGREENO);
                objParams[15] = new SqlParameter("@P_MIN_REG_CREDIT_LIMIT", dci.MIN_REG_CREDIT_LIMIT);
                objParams[16] = new SqlParameter("@P_CreditStatus", dci.CreditStatus);
                objParams[17] = new SqlParameter("@P_CgpaStatus", dci.CgpaStatus);
                objParams[18] = new SqlParameter("@P_ElectGrp1", dci.ElectGrp1);
                objParams[19] = new SqlParameter("@P_ElectGrp2", dci.ElectGrp2);
                objParams[20] = new SqlParameter("@P_ElectGrp3", dci.ElectGrp3);
                objParams[21] = new SqlParameter("@P_ElectGrp4", dci.ElectGrp4);
                objParams[22] = new SqlParameter("@P_ElectGrp5", dci.ElectGrp5);
                objParams[23] = new SqlParameter("@P_ElectGrp6", dci.ElectGrp6);
                objParams[24] = new SqlParameter("@P_ElectGrp7", dci.ElectGrp7);
                objParams[25] = new SqlParameter("@P_ElectGrp8", dci.ElectGrp8);
                objParams[26] = new SqlParameter("@P_ElectGrp9", dci.ElectGrp9);
                objParams[27] = new SqlParameter("@P_ElectGrp10", dci.ElectGrp10);
                objParams[28] = new SqlParameter("@P_ElectGrp11", dci.ElectGrp11);
                objParams[29] = new SqlParameter("@P_ElectGrp12", dci.ElectGrp12);
                objParams[30] = new SqlParameter("@P_ElectGrp13", dci.ElectGrp13);
                objParams[31] = new SqlParameter("@P_ElectGrp14", dci.ElectGrp14);
                objParams[32] = new SqlParameter("@P_ElectGrp15", dci.ElectGrp15);
                objParams[33] = new SqlParameter("@P_MovableSub", dci.MovableSub);
                objParams[34] = new SqlParameter("@P_COLLEGE_ID", College_id);
                objParams[35] = new SqlParameter("@P_MODULE_REG_TYPE", Module_reg);
                objParams[36] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                objParams[36].Direction = ParameterDirection.Output;

                retStatus = Convert.ToInt32(objSQLHelper.ExecuteNonQuerySP("PKG_DEFINE_TOTAL_CREDIT", objParams, true));
            }
            catch (Exception ex)
            {
                retStatus = -99;
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.AddRegisteredSubjects-> " + ex.ToString());
            }

            return retStatus;
        }

        public int UpdateCredit(DefineCreditLimit dci, int College_id, int Module_reg)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;

                //Add New Registered Subject Details
                objParams = new SqlParameter[38];

                objParams[0] = new SqlParameter("@P_SESSIONNO", dci.SESSION);
                objParams[1] = new SqlParameter("@P_SESSIONNAME", dci.SESSIONNAME);
                objParams[2] = new SqlParameter("@P_STUDENTTYPE", dci.STUDENT_TYPE);
                objParams[3] = new SqlParameter("@P_FROM_CGPA", dci.FROM_CGPA);
                objParams[4] = new SqlParameter("@P_TO_CGPA", dci.TO_CGPA);
                objParams[5] = new SqlParameter("@P_ADDITIONAL_COURSE", dci.ADDITIONAL_COURSE);
                objParams[6] = new SqlParameter("@P_ADM_TYPE", dci.ADM_TYPE);
                objParams[7] = new SqlParameter("@P_DEGREE_TYPE", dci.DEGREE_TYPE);
                objParams[8] = new SqlParameter("@P_FROM_CREDIT", dci.FROM_CREDIT);
                objParams[9] = new SqlParameter("@P_TO_CREDIT", dci.TO_CREDIT);
                objParams[10] = new SqlParameter("@P_MIN_SCHEME_LIMIT", dci.MIN_SCHEMELIMIT);
                objParams[11] = new SqlParameter("@P_MAX_SCHEME_LIMIT", dci.MAX_SCHEMELIMIT);
                objParams[12] = new SqlParameter("@P_Semester", dci.Semester);
                objParams[13] = new SqlParameter("@P_Semester_Text", dci.Semester_Text);
                objParams[14] = new SqlParameter("@P_IDNO", dci.RECORDNO);
                objParams[15] = new SqlParameter("@P_DEGREENO", dci.DEGREENO);
                objParams[16] = new SqlParameter("@P_MIN_REG_CREDIT_LIMIT", dci.MIN_REG_CREDIT_LIMIT);
                objParams[17] = new SqlParameter("@P_CreditStatus", dci.CreditStatus);
                objParams[18] = new SqlParameter("@P_CgpaStatus", dci.CgpaStatus);
                objParams[19] = new SqlParameter("@P_ElectGrp1", dci.ElectGrp1);
                objParams[20] = new SqlParameter("@P_ElectGrp2", dci.ElectGrp2);
                objParams[21] = new SqlParameter("@P_ElectGrp3", dci.ElectGrp3);
                objParams[22] = new SqlParameter("@P_ElectGrp4", dci.ElectGrp4);
                objParams[23] = new SqlParameter("@P_ElectGrp5", dci.ElectGrp5);
                objParams[24] = new SqlParameter("@P_ElectGrp6", dci.ElectGrp6);
                objParams[25] = new SqlParameter("@P_ElectGrp7", dci.ElectGrp7);
                objParams[26] = new SqlParameter("@P_ElectGrp8", dci.ElectGrp8);
                objParams[27] = new SqlParameter("@P_ElectGrp9", dci.ElectGrp9);
                objParams[28] = new SqlParameter("@P_ElectGrp10", dci.ElectGrp10);
                objParams[29] = new SqlParameter("@P_ElectGrp11", dci.ElectGrp11);
                objParams[30] = new SqlParameter("@P_ElectGrp12", dci.ElectGrp12);
                objParams[31] = new SqlParameter("@P_ElectGrp13", dci.ElectGrp13);
                objParams[32] = new SqlParameter("@P_ElectGrp14", dci.ElectGrp14);
                objParams[33] = new SqlParameter("@P_ElectGrp15", dci.ElectGrp15);
                objParams[34] = new SqlParameter("@P_MovableSub", dci.MovableSub);
                objParams[35] = new SqlParameter("@P_COLLEGE_ID", College_id);
                objParams[36] = new SqlParameter("@P_MODULE_REG_TYPE", Module_reg);
                objParams[37] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                objParams[37].Direction = ParameterDirection.Output;

                retStatus = Convert.ToInt32(objSQLHelper.ExecuteNonQuerySP("PKG_UPD_DEFINE_TOTAL_CREDIT", objParams, true));
            }
            catch (Exception ex)
            {
                retStatus = -99;
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.AddRegisteredSubjects-> " + ex.ToString());
            }

            return retStatus;
        }

        public int LockCreditDefination()
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;

                //Add New Registered Subject Details
                objParams = new SqlParameter[1];

                objParams[0] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                objParams[0].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("LOCK_DEFINE_TOTAL_CREDIT", objParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.AddRegisteredSubjects-> " + ex.ToString());
            }
            return retStatus;
        }

        public int AddSemPromotionDefination(DefineCreditLimit dci)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;
                //Add New Registered Subject Details
                objParams = new SqlParameter[9];

                objParams[0] = new SqlParameter("@P_SESSIONNO", dci.SESSION);
                objParams[1] = new SqlParameter("@P_SESSIONNAME", dci.SESSIONNAME);
                objParams[2] = new SqlParameter("@P_DEGREENO", dci.DEGREENO);
                objParams[3] = new SqlParameter("@P_TERM", Convert.ToInt32(dci.Semester));
                objParams[4] = new SqlParameter("@P_MIN_CREDIT", dci.MIN_REG_CREDIT_LIMIT);
                objParams[5] = new SqlParameter("@P_BRANCHNOS", dci.BRANCHNOS);
                objParams[6] = new SqlParameter("@P_BRANCHNOS_TEXT", dci.BRANCHNOS_TEXT);
                objParams[7] = new SqlParameter("@P_TYPE", dci.TYPE);

                objParams[8] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                objParams[8].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_SEMESTER_PROMOTION_DEFINATION", objParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.AddRegisteredSubjects-> " + ex.ToString());
            }
            return retStatus;
        }

        public int AddResultPublishDefination(DefineCreditLimit dci)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[6];

                objParams[0] = new SqlParameter("@P_SESSIONNO", dci.SESSION);
                objParams[1] = new SqlParameter("@P_DEGREENO", dci.DEGREENO);
                objParams[2] = new SqlParameter("@P_BRANCHNO", dci.BRANCHNO);
                objParams[3] = new SqlParameter("@P_TERM", dci.Semester);
                objParams[4] = new SqlParameter("@P_FLAG", dci.PUBLIISH_YES_NO);
                objParams[5] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                objParams[5].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_RESULT_PUBLISH_DEFINATION", objParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.AddRegisteredSubjects-> " + ex.ToString());
            }

            return retStatus;
        }

        public int UpdateSemPromotionDefination(DefineCreditLimit dci)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;

                //Add New Registered Subject Details
                objParams = new SqlParameter[10];

                objParams[0] = new SqlParameter("@P_SESSIONNO", dci.SESSION);
                objParams[1] = new SqlParameter("@P_SESSIONNAME", dci.SESSIONNAME);
                objParams[2] = new SqlParameter("@P_DEGREENO", dci.DEGREENO);
                objParams[3] = new SqlParameter("@P_TERM", Convert.ToInt32(dci.Semester));
                objParams[4] = new SqlParameter("@P_MIN_CREDIT", dci.MIN_REG_CREDIT_LIMIT);
                objParams[5] = new SqlParameter("@P_BRANCHNOS", dci.BRANCHNOS);
                objParams[6] = new SqlParameter("@P_BRANCHNOS_TEXT", dci.BRANCHNOS_TEXT);
                objParams[7] = new SqlParameter("@P_RECORDNO", dci.RECORDNO);
                objParams[8] = new SqlParameter("@P_TYPE", dci.TYPE);
                objParams[9] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                objParams[9].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_UPD_SEMESTER_PROMOTION_DEFINATION", objParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.AddRegisteredSubjects-> " + ex.ToString());
            }
            return retStatus;
        }

        public int UpdateResultPublishDefination(DefineCreditLimit dci)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[7];

                objParams[0] = new SqlParameter("@P_SESSIONNO", dci.SESSION);
                objParams[1] = new SqlParameter("@P_DEGREENO", dci.DEGREENO);
                objParams[2] = new SqlParameter("@P_BRANCHNO", dci.BRANCHNO);
                objParams[3] = new SqlParameter("@P_TERM", dci.Semester);
                objParams[4] = new SqlParameter("@P_FLAG", dci.PUBLIISH_YES_NO);
                objParams[5] = new SqlParameter("@P_PUBNO", dci.RECORDNO);
                objParams[6] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                objParams[6].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_UPD_RESULT_PUBLISH_DEFINATION", objParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.AddRegisteredSubjects-> " + ex.ToString());
            }
            return retStatus;
        }
    }
}