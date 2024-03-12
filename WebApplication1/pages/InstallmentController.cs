using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using System;
using System.Data;
using System.Data.SqlClient;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessLogic
        {
            public class InstallmentController
            {
                private string _UAIMS_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                public DataSet GetStudentData(int sessiono, int degreeno, int branch, int scheme, int RECPTNO, int semester)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);

                        SqlParameter[] objParams = new SqlParameter[6];
                        //  objParams[0] = new SqlParameter("@P_ADMBATCH", admbatch);
                        objParams[0] = new SqlParameter("@P_SESSION", sessiono);
                        objParams[1] = new SqlParameter("@P_DEGREENO ", degreeno);
                        objParams[2] = new SqlParameter("@P_BRANCH ", branch);
                        objParams[3] = new SqlParameter("@P_SCHEME", scheme);
                        objParams[4] = new SqlParameter("@P_RECPTNO", RECPTNO);
                        objParams[5] = new SqlParameter("@P_SEMESTRNO", semester);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_INSTALLMENT_STUDENT", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.InstallmentController.GetStudentData-> " + ex.ToString());
                    }

                    return ds;
                }

                public int AddStudentData(Installment objIE)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        //Add New Registered Subject Details
                        objParams = new SqlParameter[9];
                        objParams[0] = new SqlParameter("@P_IDNO", objIE.IDNO);
                        objParams[1] = new SqlParameter("@P_RECIPTNO", objIE.ReciptNo);
                        objParams[2] = new SqlParameter("@P_INSTALL_STATUS", objIE.Install_Status);
                        objParams[3] = new SqlParameter("@P_NO_OF_INSTALL", objIE.NoOfInstallment);
                        objParams[4] = new SqlParameter("@P_UANO", objIE.UANO);
                        objParams[5] = new SqlParameter("@P_TRANSDATE", objIE.TransDate);
                        objParams[6] = new SqlParameter("@P_COLLEGE_CODE", objIE.CollegeCode);
                        objParams[7] = new SqlParameter("@P_IP_ADDRESS", objIE.IP_Address);
                        objParams[8] = new SqlParameter("@P_SESSION", objIE.SessionNo);

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_SAVE_INSTALLMENT_STUDENT", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.FeeInstallment.AddStudentData-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int AddLock(int idno, int flag, int rcptno, int SESSIONNO)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        //Add New Registered Subject Details
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        objParams[1] = new SqlParameter("@P_FLAG", flag);
                        objParams[2] = new SqlParameter("@P_RCPTNO", rcptno);
                        objParams[3] = new SqlParameter("@P_SESSIONNO", SESSIONNO);

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_LOCK_INSTALLMENT_STUDENT", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.FeeInstallment.AddStudentData-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet getCrystalData(int branch, int scheme, int degreeno, int admbatch, int recipttype)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);

                        SqlParameter[] objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_BRANCH", branch);
                        objParams[1] = new SqlParameter("@P_SCHEME", scheme);
                        objParams[2] = new SqlParameter("@P_DEGREENO", degreeno);
                        objParams[3] = new SqlParameter("@P_ADMBATCH", admbatch);
                        objParams[4] = new SqlParameter("@P_RECIPTTYPE", recipttype);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_INSTALLMENT_CRYSTAL_REPORT", objParams);
                    }
                    catch (Exception ex)
                    {
                        //return ds;
                        // throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.InstallmentController.GetCrystalData-> " + ex.ToString());
                    }
                    return ds;
                }

                // ADDED BY NARESH BEERLA ON 12032021 FOR REMOVING THE REGULATION FILTERATION

                public DataSet GetStudentData(int sessiono, int degreeno, int branch, int RECPTNO, int semester) //int scheme,
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);

                        SqlParameter[] objParams = new SqlParameter[5];
                        //  objParams[0] = new SqlParameter("@P_ADMBATCH", admbatch);
                        objParams[0] = new SqlParameter("@P_SESSION", sessiono);
                        objParams[1] = new SqlParameter("@P_DEGREENO ", degreeno);
                        objParams[2] = new SqlParameter("@P_BRANCH ", branch);
                        //  objParams[3] = new SqlParameter("@P_SCHEME", scheme);
                        objParams[3] = new SqlParameter("@P_RECPTNO", RECPTNO);
                        objParams[4] = new SqlParameter("@P_SEMESTRNO", semester);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_INSTALLMENT_STUDENT", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.InstallmentController.GetStudentData-> " + ex.ToString());
                    }

                    return ds;
                }
            }
        }
    }
}