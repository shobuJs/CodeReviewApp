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
            public class New_Student_RegController
            {
                private string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
                private int pkId = 0;
                private string Message = string.Empty;

                public int Insert_Update_New_Student(New_Student objNS, string ipAddress)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[16];

                        objParams[0] = new SqlParameter("@P_STUDNAME", objNS.StudName);
                        objParams[1] = new SqlParameter("@P_MOBILENO", objNS.MobileNo);
                        objParams[2] = new SqlParameter("@P_EMAIL", objNS.EmailId);
                        objParams[3] = new SqlParameter("@P_COURSE", objNS.Course);
                        objParams[4] = new SqlParameter("@P_DOB", objNS.Dob);
                        objParams[5] = new SqlParameter("@P_USERNAME", objNS.UserName);
                        objParams[6] = new SqlParameter("@P_PASSWORD", objNS.Password);
                        objParams[7] = new SqlParameter("@P_CATNO", objNS.CatNo);
                        objParams[8] = new SqlParameter("@P_R_IPADDRESS", ipAddress);
                        objParams[9] = new SqlParameter("@P_FILENAME", objNS.FileName);
                        objParams[10] = new SqlParameter("@P_FILEPATH", objNS.FilePath);
                        objParams[11] = new SqlParameter("@P_DEGREENO", objNS.Degree);
                        objParams[12] = new SqlParameter("@P_BRANCHNO", objNS.Branch);
                        objParams[13] = new SqlParameter("@P_ADMISSION_TYPE", objNS.AdmissionType);
                        objParams[14] = new SqlParameter("@P_UA_NO_TRAN", objNS.UA_NO);

                        objParams[15] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[15].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_INSERT_UPDATE_STUDENT_RECORD_STUDINFO", objParams, true);
                        if (ret != null)
                        {
                            if (ret.ToString() == "-99")
                            {
                                retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                            }
                            else if (Convert.ToInt32(ret) > 0)
                            {
                                retStatus = Convert.ToInt32(ret);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentController.updateDetention->" + ex.ToString());
                    }
                    return retStatus;
                }

                public int VerifyOTP(New_Student objNS)
                {
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[5];

                        objParams[0] = new SqlParameter("@P_IDNO", objNS.RegNo);
                        objParams[1] = new SqlParameter("@P_NAME", objNS.UName);
                        objParams[2] = new SqlParameter("@P_MOBILENO", objNS.PhoneNo);
                        objParams[3] = new SqlParameter("@P_SMSBODY", objNS.SmsBody);
                        objParams[4] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[4].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_SEND_SMS_ADMISSION", objParams, true);
                        if (ret != null)
                        {
                            if (ret.ToString() == "-99")
                            {
                                Message = "Error in Procedure";
                            }
                            else
                            {
                                pkId = Convert.ToInt32(ret);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.SchemeController.GetControlsheetRptData --> " + ex.ToString());
                    }
                    return pkId;
                }
            }
        }
    }
}