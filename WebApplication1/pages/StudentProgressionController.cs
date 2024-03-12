using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using System;
using System.Data;
using System.Data.SqlClient;

namespace IITMS.UAIMS.BusinessLayer.BusinessLogic
{
    public class StudentProgressionController
    {
        private string uaims_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
        private int status = 0;

        public int AddStudentProgression(StudentProgression objSP, int STUDENT_PROGRESSION_RULEID, string xml, string year, int optionwise, int Active)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(uaims_constr);
                SqlParameter[] objParams = null;

                //Add New User
                objParams = new SqlParameter[13];
                objParams[0] = new SqlParameter("@P_STUDENT_PROGRESSION_RULEID", objSP.student_progression_ruleid);
                objParams[1] = new SqlParameter("@P_RULENAME", objSP.rulename);
                objParams[2] = new SqlParameter("@P_COLLEGE_ID", objSP.college_id);
                objParams[3] = new SqlParameter("@P_UA_SECTION", objSP.ua_section);
                objParams[4] = new SqlParameter("@P_DEGREENO", objSP.degreeno);
                objParams[5] = new SqlParameter("@P_BRANCHNO", objSP.branchno);
                objParams[6] = new SqlParameter("@P_AFFILIATED_NO", objSP.affiliated_no);
                objParams[7] = new SqlParameter("@P_USERID", objSP.userid);
                objParams[8] = new SqlParameter("@P_YEAR", year);
                objParams[9] = new SqlParameter("@P_OPTIONWISE", optionwise);
                objParams[10] = new SqlParameter("@P_XML", xml);
                objParams[11] = new SqlParameter("@P_ACTIVE", Active);
                objParams[12] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[12].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INSERT_UPDATE_STUDENT_PROGRESSION", objParams, true);//) != null)
                retStatus = Convert.ToInt32(ret);
                if (status.ToString() != "-99")
                {
                    if (retStatus == 1)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    else if (retStatus == 2)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    else if (retStatus == -1001)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.DuplicateRecord);
                    }
                }
                else
                {
                    retStatus = Convert.ToInt32(CustomStatus.Error);
                }
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentProgressionController.AddStudentProgression-> " + ex.ToString());
            }
            return retStatus;
        }

        public int AddRuleAllocation(int BatchNo, string StudentProgressionRuleId, int UserId)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(uaims_constr);
                SqlParameter[] objParams = null;

                //Add New User
                objParams = new SqlParameter[4];
                objParams[0] = new SqlParameter("@P_BATCHNO", BatchNo);
                objParams[1] = new SqlParameter("@P_STUDENT_PROGRESSION_RULEID", StudentProgressionRuleId);
                objParams[2] = new SqlParameter("@P_USERID", UserId);
                objParams[3] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[3].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INSERT_UPDATE_RULE_ALLOCATION", objParams, true);//) != null)
                retStatus = Convert.ToInt32(ret);
                if (ret != null && ret.ToString() != "-99" && ret.ToString() != "-1001")
                {
                    if (retStatus == 1)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    else if (retStatus == 2)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                }
                else
                {
                    retStatus = Convert.ToInt32(CustomStatus.Error);
                }
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentProgressionController.AddStudentProgression-> " + ex.ToString());
            }
            return retStatus;
        }

        //ADDED BY AASHNA 01-09-2022

        public int AddStudentBridgingProgression(StudentProgression objSP, int moduleno, int fromsem, int tosem, int briddegreeno, int bridbranchno, int bridaff, int bridsem)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(uaims_constr);
                SqlParameter[] objParams = null;

                //Add New User
                objParams = new SqlParameter[15];
                objParams[0] = new SqlParameter("@P_RULEID", objSP.student_progression_ruleid);
                objParams[1] = new SqlParameter("@P_COLLEGE_ID", objSP.college_id);
                objParams[2] = new SqlParameter("@P_UA_SECTION", objSP.ua_section);
                objParams[3] = new SqlParameter("@P_DEGREENO", objSP.degreeno);
                objParams[4] = new SqlParameter("@P_BRANCHNO", objSP.branchno);
                objParams[5] = new SqlParameter("@P_AFFILIATED_NO", objSP.affiliated_no);
                objParams[6] = new SqlParameter("@P_BRIDGING_DEGREENO", briddegreeno);
                objParams[7] = new SqlParameter("@P_BRIDGING_BRANCHNO", bridbranchno);
                objParams[8] = new SqlParameter("@P_BRIDGING_AFFILIATED_NO", bridaff);
                objParams[9] = new SqlParameter("@P_SEMESTERNO_BRIDGING", bridsem);
                objParams[10] = new SqlParameter("@P_USERID", objSP.userid);
                objParams[11] = new SqlParameter("@P_MAX_FAIL_MODULE", moduleno);
                objParams[12] = new SqlParameter("@P_SEMESTERNO_FROM", fromsem);
                objParams[13] = new SqlParameter("@P_SEMESTERNO_TO", tosem);
                objParams[14] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[14].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INSERT_UPDATE_STUDENT_PROGRESSION_BRIDGING", objParams, true);//) != null)
                retStatus = Convert.ToInt32(ret);
                if (status.ToString() != "-99")
                {
                    if (retStatus == 1)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    else if (retStatus == 2)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    else if (retStatus == -1001)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.DuplicateRecord);
                    }
                }
                else
                {
                    retStatus = Convert.ToInt32(CustomStatus.Error);
                }
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentProgressionController.AddStudentProgression-> " + ex.ToString());
            }
            return retStatus;
        }

        //ADDED BY AASHNA 10-12-202
        public int AddStudentBridgingAllot(string idno, int ua_no, string ipaddress, string name, string regno, int SEMESTERNO)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(uaims_constr);
                SqlParameter[] objParams = null;

                //Add New User
                objParams = new SqlParameter[7];
                objParams[0] = new SqlParameter("@P_IDNO", idno);
                objParams[1] = new SqlParameter("@P_UA_NO", ua_no);
                objParams[2] = new SqlParameter("@P_REGNO", regno);
                objParams[3] = new SqlParameter("@P_SEMESTERNO_FROM", SEMESTERNO);
                objParams[4] = new SqlParameter("@P_NAME", name);
                objParams[5] = new SqlParameter("@P_IPADDRESS", ipaddress);
                objParams[6] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                objParams[6].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_UPD_BRIDGING_SEMESTER", objParams, true);//) != null)
                retStatus = Convert.ToInt32(ret);
                if (status.ToString() != "-99")
                {
                    if (retStatus == 1)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                }
                else
                {
                    retStatus = Convert.ToInt32(CustomStatus.Error);
                }
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentProgressionController.AddStudentProgression-> " + ex.ToString());
            }
            return retStatus;
        }

        //ADDED BY AASHNA 12-12-202
        public int AddStudentBridgingPgmTransfer(string idno, int ua_no, string ipaddress, string name, string regno, int degreeno, int branchno, int newdegreeno, int newbranchno)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(uaims_constr);
                SqlParameter[] objParams = null;

                //Add New User
                objParams = new SqlParameter[10];
                objParams[0] = new SqlParameter("@P_IDNO", idno);
                objParams[1] = new SqlParameter("@P_REGNO", regno);
                objParams[2] = new SqlParameter("@P_NAME", name);
                objParams[3] = new SqlParameter("@P_DEGREENO", degreeno);
                objParams[4] = new SqlParameter("@P_BRANCHNO", branchno);
                objParams[5] = new SqlParameter("@P_NEW_DEGREENO", newdegreeno);
                objParams[6] = new SqlParameter("@P_NEW_BRANCHNO", newbranchno);
                objParams[7] = new SqlParameter("@P_IPADDRESS", ipaddress);
                objParams[8] = new SqlParameter("@P_UA_NO", ua_no);
                objParams[9] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                objParams[9].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_UPDATE_BRIGING_CHANGE_PROGRAM", objParams, true);
                retStatus = Convert.ToInt32(ret);
                if (status.ToString() != "-99")
                {
                    if (retStatus == 1)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                }
                else
                {
                    retStatus = Convert.ToInt32(CustomStatus.Error);
                }
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentProgressionController.AddStudentProgression-> " + ex.ToString());
            }
            return retStatus;
        }
    }
}