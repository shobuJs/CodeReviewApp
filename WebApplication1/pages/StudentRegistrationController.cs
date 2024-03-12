using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using System;
using System.Data;
using System.Data.SqlClient;

namespace IITMS.UAIMS.BusinessLayer.BusinessLogic
{
    public class StudentRegistrationController
    {
        private string _constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

        /// <summary>
        /// Insert Update Student Online Admission Data
        /// Page : StudentRegistration.aspx
        /// </summary>
        /// <param name="objSR"></param>
        /// <returns>integer</returns>

        /// <summary>
        /// This controller is used to Get Student's detail according to IDNo
        /// Page : StudentRegistration.aspx
        /// </summary>
        /// <param name="idno"></param>
        /// <returns></returns>
        public DataTableReader GetStudentDetails(int idno)
        {
            DataTableReader dtr = null;

            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_IDNO", idno);

                dtr = objSQLHelper.ExecuteDataSetSP("PKG_SP_RET_STUDENT_REG_BYID_STUDINFO", objParams).Tables[0].CreateDataReader();
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentRegistrationController.GetStudentDetails->" + ex.ToString());
            }
            return dtr;
        }

        /// <summary>
        /// This controller is used to Get Student's detail according to IDNo
        /// Page : StudentRegistration.aspx
        /// </summary>
        /// <param name="idno"></param>
        /// <returns></returns>
        public DataTableReader GetStudentDetailsFromSignUp(int idno)
        {
            DataTableReader dtr = null;

            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_IDNO", idno);

                dtr = objSQLHelper.ExecuteDataSetSP("PKG_SP_RET_STUDENT_REG_BYID_FROM_SIGNUP", objParams).Tables[0].CreateDataReader();
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentRegistrationController.GetStudentDetailsFromSignUp->" + ex.ToString());
            }
            return dtr;
        }

        /// <summary>
        /// This controller is used to Get Last exam details
        /// Page : StudentRegistration.aspx
        /// </summary>
        /// <param name="idno"></param>
        /// <returns></returns>
        public DataTableReader GetLastExamDetails(int idno)
        {
            DataTableReader dtr = null;

            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_IDNO", idno);

                dtr = objSQLHelper.ExecuteDataSetSP("PKG_SP_RET_STUDENT_QUALEXAM_BYID_STUDINFO", objParams).Tables[0].CreateDataReader();
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentRegistrationController.GetLastExamDetails->" + ex.ToString());
            }
            return dtr;
        }

        /// <summary>
        /// Insert Update Student Admission Address Data
        /// Page : StudentRegistration.aspx
        /// </summary>
        /// <param name="objSR"></param>
        /// <returns>integer</returns>
        public int AddUpdateStudentRegAddressDetail(StudentRegistrationModel objSR)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_constr);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[33];
                objParams[0] = new SqlParameter("@P_IDNO", objSR.IDNO);
                objParams[1] = new SqlParameter("@P_LADDRESSLINE1", objSR.LAddressLine1);
                objParams[2] = new SqlParameter("@P_LCITY", objSR.Lcity);
                objParams[3] = new SqlParameter("@P_LSTATE", objSR.LState);
                objParams[4] = new SqlParameter("@P_LPINCODE", objSR.LPinCode);
                objParams[5] = new SqlParameter("@P_LSTDCODE", objSR.LSTDCode);
                objParams[6] = new SqlParameter("@P_LLANDLINENO", objSR.LLandLineNo);
                objParams[7] = new SqlParameter("@P_LEMAILID", objSR.LEmailId);
                objParams[8] = new SqlParameter("@P_LSTUDENTMOBILE", objSR.LStudentMobile);
                objParams[9] = new SqlParameter("@P_LSMSSEND", objSR.LSMSSend);
                objParams[10] = new SqlParameter("@P_PADDRESSLINE1", objSR.PAddressLine1);
                objParams[11] = new SqlParameter("@P_PCITY", objSR.PCity);
                objParams[12] = new SqlParameter("@P_PSTATE", objSR.PState);
                objParams[13] = new SqlParameter("@P_PPINCODE", objSR.PPinCode);
                objParams[14] = new SqlParameter("@P_PSTDCODE", objSR.PSTDCode);
                objParams[15] = new SqlParameter("@P_PLANDLINENUMBER", objSR.PLandLineNumber);
                objParams[16] = new SqlParameter("@P_GADDRESSLINE1", objSR.GAddressLine1);
                objParams[17] = new SqlParameter("@P_GCITY", objSR.GCity);
                objParams[18] = new SqlParameter("@P_GSTATE", objSR.GState);
                objParams[19] = new SqlParameter("@P_GPINCODE", objSR.GPinCode);
                objParams[20] = new SqlParameter("@P_GSTDCODE", objSR.GSTDCode);
                objParams[21] = new SqlParameter("@P_GLANDLINENUMBER", objSR.GLandLineNumber);
                objParams[22] = new SqlParameter("@P_GEMAILID", objSR.GEmailId);
                objParams[23] = new SqlParameter("@P_GGAURDIANMOBILE", objSR.GGaurdianMobile);
                objParams[24] = new SqlParameter("@P_LOTHERCITY", objSR.LOthercity);
                objParams[25] = new SqlParameter("@P_POTHERCITY", objSR.POtherCity);
                objParams[26] = new SqlParameter("@P_GOTHERCITY", objSR.GOtherCity);
                objParams[27] = new SqlParameter("@P_EMERGENCY_CONTACT", objSR.EmergencyNum);
                objParams[28] = new SqlParameter("@P_EMERGENCY_NAME", objSR.EmergencyName);
                objParams[29] = new SqlParameter("@P_EMERGENCY_RELATION", objSR.EmergencyRelation);
                objParams[30] = new SqlParameter("@P_EMERGENCY_EMAIL", objSR.EmergencyEmail);
                objParams[31] = new SqlParameter("@P_GRELATION", objSR.GRelation);

                objParams[32] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[32].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_INSERT_STUDENT_ADDRESS_DETAILS_STUDINFO", objParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Academic.AddUpdateOnlineStudentReg-> " + ex.ToString());
            }

            return retStatus;
        }

        public int AddUpdateParentDetail(StudentRegistrationModel objSR)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[57];

                objParams[0] = new SqlParameter("@P_IDNO", objSR.IDNO);
                objParams[1] = new SqlParameter("@P_SINGLEPARENT", objSR.SingleParent);
                objParams[2] = new SqlParameter("@P_FATHERNAME", objSR.FatherName);
                objParams[3] = new SqlParameter("@P_FATHER_LASTNAME", objSR.FatherLastName);
                objParams[4] = new SqlParameter("@P_FATHER_QUAL", objSR.FatherQualification);
                objParams[5] = new SqlParameter("@P_FATHEROCCUPATION", objSR.FatherOccupation);
                objParams[6] = new SqlParameter("@P_FATH_ORG_NAME", objSR.FatherOrgName);
                objParams[7] = new SqlParameter("@P_FATHERDESIG", objSR.FatherDesig);
                objParams[8] = new SqlParameter("@P_FANNUAL_INCOME", objSR.FatherAnnualIncome);
                objParams[9] = new SqlParameter("@P_FATHER_MOBILE", objSR.FatherMobile);
                objParams[10] = new SqlParameter("@P_FATHER_ORG_ADD", objSR.FatherOrgAddress);
                objParams[11] = new SqlParameter("@P_FATHER_ORG_CITY", objSR.FatherOrgCity);
                objParams[12] = new SqlParameter("@P_FATHER_ORG_STATE", objSR.FatherOrgState);
                objParams[13] = new SqlParameter("@P_FATHER_ORG_PIN", objSR.FatherOrgPin);
                objParams[14] = new SqlParameter("@P_FATHER_ORG_STD", objSR.FatherOrgSTD);
                objParams[15] = new SqlParameter("@P_FATHER_ORG_PHONE", objSR.FatherOrgPhone);
                objParams[16] = new SqlParameter("@P_MOTHERNAME", objSR.MotherName);
                objParams[17] = new SqlParameter("@P_MOTHER_LASTNAME", objSR.MotherLastName);
                objParams[18] = new SqlParameter("@P_MOTHER_DESIG", objSR.MotherDesig);
                objParams[19] = new SqlParameter("@P_MOTHER_OTHER_DESIG", objSR.MotherOtherDesig);
                objParams[20] = new SqlParameter("@P_MOTHER_QUAL", objSR.MotherQual);
                objParams[21] = new SqlParameter("@P_MOTHER_OTHER_QUAL", objSR.MotherOtherQual);
                objParams[22] = new SqlParameter("@P_MOTHER_OCCUPATION", objSR.MotherOccupation);
                objParams[23] = new SqlParameter("@P_MOTHER_MOBILE", objSR.MotherMobile);
                objParams[24] = new SqlParameter("@P_MOTHER_ORG_ADD", objSR.MotherOrgAdd);
                objParams[25] = new SqlParameter("@P_MOTHER_ORG_CITY", objSR.MotherOrgCity);
                objParams[26] = new SqlParameter("@P_MOTHER_ORG_STATE", objSR.MotherOrgState);
                objParams[27] = new SqlParameter("@P_MOTHER_ORG_PIN", objSR.MotherOrgPin);
                objParams[28] = new SqlParameter("@P_MOTHER_ORG_STD", objSR.MotherOrgSTD);
                objParams[29] = new SqlParameter("@P_MOTHER_ORG_PHONE", objSR.MotherOrgPhone);
                objParams[30] = new SqlParameter("@P_GUARDIAN_NAME", objSR.GaurdianName);
                objParams[31] = new SqlParameter("@P_GUARDIAN_LASTNAME", objSR.GaurdianLastName);
                objParams[32] = new SqlParameter("@P_G_DESIGNATION", objSR.GuardianDesignation);
                objParams[33] = new SqlParameter("@P_G_OTHER_DESIGNATION", objSR.GuardianOtherDesig);
                objParams[34] = new SqlParameter("@P_GUARDIAN_QUAL", objSR.GuardianQual);
                objParams[35] = new SqlParameter("@P_GUARDIAN_OTHER_QUAL", objSR.GuardianOtherQual);
                objParams[36] = new SqlParameter("@P_G_OCCUPATION", objSR.GuardianOccupation);
                objParams[37] = new SqlParameter("@P_G_ORG_NAME", objSR.GuardianOrgName);
                objParams[38] = new SqlParameter("@P_G_ANNUAL_INCOME", objSR.GuardianAnnualIncome);
                objParams[39] = new SqlParameter("@P_G_ORG_ADD", objSR.GuardianOrgAdd);
                objParams[40] = new SqlParameter("@P_G_ORG_CITY", objSR.GuardianOrgCity);
                objParams[41] = new SqlParameter("@P_G_ORG_STATE", objSR.GuardianOrgState);
                objParams[42] = new SqlParameter("@P_G_ORG_PIN", objSR.GuardianOrgPin);
                objParams[43] = new SqlParameter("@P_G_ORG_STD", objSR.GuardianOrgSTD);
                objParams[44] = new SqlParameter("@P_G_ORG_PHONE", objSR.GuardianOrgPhone);
                objParams[45] = new SqlParameter("@P_F_OTHER_DESIG", objSR.FatherOtherDesig);
                objParams[46] = new SqlParameter("@P_FATHER_OTHER_QUAL", objSR.FatherOtherQual);
                objParams[47] = new SqlParameter("@P_MOTHER_ORGANIZATION", objSR.MotherWorkingOrg);
                objParams[48] = new SqlParameter("@P_MOTHER_INCOME", objSR.MotherAnnualIncome);
                objParams[49] = new SqlParameter("@P_PARENT_TYPE", objSR.Parent_Type);
                objParams[50] = new SqlParameter("@P_FATHER_AADHARNO", objSR.FatherAdhaarcardNo);
                objParams[51] = new SqlParameter("@P_MOTHER_AADHARNO", objSR.MotherAdhaarcardNo);

                objParams[52] = new SqlParameter("@P_FATHER_EMAIL", objSR.FEmailId);
                objParams[53] = new SqlParameter("@P_FATHER_PANNO", objSR.FPANNumber);
                objParams[54] = new SqlParameter("@P_MOTHER_EMAIL", objSR.MEmailId);
                objParams[55] = new SqlParameter("@P_MOTHER_PANNO", objSR.MPANNumber);

                objParams[56] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[56].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_SP_STUDENT_PARENT_DETAILS_UPDATE_STUDINFO", objParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Academic.AddUpdateParentDetail-> " + ex.ToString());
            }

            return retStatus;
        }

        /// <summary>
        /// Insert Student Extra Details
        /// Page : StudentRegistration.aspx
        /// </summary>
        /// <param name="objSR"></param>
        /// <returns>integer</returns>
        public int AddUpdateExtraDetail(StudentRegistrationModel objSR)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_constr);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[20];
                objParams[0] = new SqlParameter("@P_IDNO", objSR.IDNO);
                objParams[1] = new SqlParameter("@P_RELATIVE_DETAIL", objSR.RelativeDetails);
                objParams[2] = new SqlParameter("@P_REASON_TO_CHOOSE", objSR.ReasonToChoose);
                objParams[3] = new SqlParameter("@P_COMMUNITY_CERT_NO", objSR.CommunityCertNo);
                if (objSR.CommunityCertIsueDate == DateTime.MinValue)
                {
                    objParams[4] = new SqlParameter("@P_COMMUNITY_CERT_ISSUE_DATE", DBNull.Value);
                }
                else
                {
                    objParams[4] = new SqlParameter("@P_COMMUNITY_CERT_ISSUE_DATE", objSR.CommunityCertIsueDate);
                }
                objParams[5] = new SqlParameter("@P_COMMUNITY_CERT_AUTHORITY", objSR.CommunityCertAuthority);
                objParams[6] = new SqlParameter("@P_TRANSFER_CERT_NO", objSR.TransferCertNo);
                if (objSR.TransferCertDate == DateTime.MinValue)
                {
                    objParams[7] = new SqlParameter("@P_TRANSFER_CERT_DATE", DBNull.Value);
                }
                else
                {
                    objParams[7] = new SqlParameter("@P_TRANSFER_CERT_DATE", objSR.TransferCertDate);
                }
                objParams[8] = new SqlParameter("@P_CONDUCT_CERT_NO", objSR.ConductCertNo);
                if (objSR.ConductCertDate == DateTime.MinValue)
                {
                    objParams[9] = new SqlParameter("@P_CONDUCT_CERT_DATE", DBNull.Value);
                }
                else
                {
                    objParams[9] = new SqlParameter("@P_CONDUCT_CERT_DATE", objSR.ConductCertDate);
                }
                objParams[10] = new SqlParameter("@P_FIRST_APPEARANCE_REGNO", objSR.FirstAppearanceRegno);
                objParams[11] = new SqlParameter("@P_FIRST_APPEARANCE_YEAR", objSR.FirstAppearanceYear);
                objParams[12] = new SqlParameter("@P_SECOND_APPEARANCE_REGNO", objSR.SecondAppearanceRegno);
                objParams[13] = new SqlParameter("@P_SECOND_APPEARANCE_YEAR", objSR.SecondAppearanceYear);
                objParams[14] = new SqlParameter("@P_THIRD_APPEARANCE_REGNO", objSR.ThirdAppearanceRegno);
                objParams[15] = new SqlParameter("@P_THIRT_APPEARANCE_YEAR", objSR.ThirdAppearanceYear);
                objParams[16] = new SqlParameter("@P_MINORITY_CERT_NO", objSR.MinorityCertificateNo);
                if (objSR.MinorityIssueDate == DateTime.MinValue)
                {
                    objParams[17] = new SqlParameter("@P_MINORITY_CERT_DATE", DBNull.Value);
                }
                else
                {
                    objParams[17] = new SqlParameter("@P_MINORITY_CERT_DATE", objSR.MinorityIssueDate);
                }

                objParams[18] = new SqlParameter("@P_MINORITY_CERT_AUTHORITY", objSR.MinorityCertAuthority);

                objParams[19] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[19].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_SP_STUDENT_EXTRA_DETAILS_UPDATE_STUDINFO", objParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Academic.AddUpdateExtraDetail-> " + ex.ToString());
            }

            return retStatus;
        }

        /// <summary>
        /// Insert Student Fees Details
        /// Page : StudentRegistration.aspx
        /// </summary>
        /// <param name="objSR"></param>
        /// <returns>integer</returns>
        public int AddUpdateFeesDetail(StudentRegistrationModel objSR)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_constr);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[14];
                objParams[0] = new SqlParameter("@P_IDNO", objSR.IDNO);
                objParams[1] = new SqlParameter("@P_FIRST_GRADUATE", objSR.FirstGraduate);
                objParams[2] = new SqlParameter("@P_FIRST_GRADUATE_CERT_NO", objSR.FirstGraduateCertNo);
                if (objSR.FirstGraduateCertDate == DateTime.MinValue)
                {
                    objParams[3] = new SqlParameter("@P_FIRST_GRADUATE_CERT_DATE", DBNull.Value);
                }
                else
                {
                    objParams[3] = new SqlParameter("@P_FIRST_GRADUATE_CERT_DATE", objSR.FirstGraduateCertDate);
                }
                objParams[4] = new SqlParameter("@P_FIRST_GRADUATE_CERT_AUTH", objSR.FirstGraduateCertAuth);
                objParams[5] = new SqlParameter("@P_AICTE_WAIVER", objSR.AICTEWaiver);
                objParams[6] = new SqlParameter("@P_AICTE_CERT_NO", objSR.AICTECertNo);
                if (objSR.AICTECertDate == DateTime.MinValue)
                {
                    objParams[7] = new SqlParameter("@P_AICTE_CERT_DATE", DBNull.Value);
                }
                else
                {
                    objParams[7] = new SqlParameter("@P_AICTE_CERT_DATE", objSR.AICTECertDate);
                }
                objParams[8] = new SqlParameter("@P_AICTE_CERT_AUTH", objSR.AICTECertAuth);
                objParams[9] = new SqlParameter("@P_DRAVIDAR_WELFARE", objSR.DravidarWelfare);
                objParams[10] = new SqlParameter("@P_WELFARE_CERT_NO", objSR.WelfareCertNo);
                if (objSR.WelfareCertDate == DateTime.MinValue)
                {
                    objParams[11] = new SqlParameter("@P_WELFARE_CERT_DATE", DBNull.Value);
                }
                else
                {
                    objParams[11] = new SqlParameter("@P_WELFARE_CERT_DATE", objSR.WelfareCertDate);
                }
                objParams[12] = new SqlParameter("@P_WELFARE_CERT_AUTH", objSR.WelfareCertAuth);

                objParams[13] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[13].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_SP_STUDENT_FEES_DETAILS_UPDATE_STUDINFO", objParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Academic.AddUpdateFeesDetail-> " + ex.ToString());
            }

            return retStatus;
        }

        /// <summary>
        /// Upload Student Document details
        /// Page : StudentRegistration.aspx
        /// </summary>
        /// <param name="objSR"></param>
        /// <returns>integer</returns>
        ///
        public int AddUpdateStudentDocumentsDetail(int idno, int hiddtudocno, string extension, string contentType, string filename, string path)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[7];
                objParams[0] = new SqlParameter("@P_IDNO", idno);
                objParams[1] = new SqlParameter("@P_STU_DOC_NO", hiddtudocno);
                // objParams[2] = new SqlParameter("@P_CHK", chkDocuments);
                objParams[2] = new SqlParameter("@P_EXTENSION", extension);
                objParams[3] = new SqlParameter("@P_CONTENTTYPE", contentType);
                // objParams[5] = new SqlParameter("@P_FILEDATA", document);
                objParams[4] = new SqlParameter("@P_FILEPATH", path);
                objParams[5] = new SqlParameter("@P_FILENAME", filename);
                objParams[6] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[6].Direction = ParameterDirection.Output;
                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INSERT_UPDATE_STUDENT_FILE_UPLOAD_DOCUMENT_STUDINFO", objParams, true);

                if (Convert.ToInt32(ret) == 1)
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                else if (Convert.ToInt32(ret) == 2)
                {
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                }
                else
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.AddUpdateStudentDocumentsDetail-> " + ex.ToString());
            }
            return retStatus;
            //throw new NotImplementedException();
        }

        /// <summary>
        /// Upload Student Photo details
        /// Page : StudentRegistration.aspx
        /// </summary>
        /// <param name="objSR"></param>
        /// <returns>integer</returns>
        public int AddUpdateStudentPhotoSign(int idno, byte[] photo, byte[] sign)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[4];
                objParams[0] = new SqlParameter("@P_IDNO", idno);
                objParams[1] = new SqlParameter("@P_STUDENT_PHOTO", photo);
                objParams[2] = new SqlParameter("@P_STUDENT_SIGN", sign);
                objParams[3] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[3].Direction = ParameterDirection.Output;
                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_SP_STUDENT_PHOTO_SIGN_DETAILS_UPDATE_STUDINFO", objParams, true);

                if (Convert.ToInt32(ret) == 1)
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                else if (Convert.ToInt32(ret) == 2)
                {
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                }
                else
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.AddUpdateStudentPhotoSign-> " + ex.ToString());
            }
            return retStatus;

            // throw new NotImplementedException();
        }

        public DataSet GetDocumentList(int UA_NO)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_constr);
                SqlParameter[] objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_UA_NO", UA_NO);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_DOCUMENT_LIST_STUDINFO", objParams);
            }
            catch (Exception ex)
            {
                return ds;
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentRegistrationController.GetDocumentList-> " + ex.ToString());
            }
            return ds;
        }

        //added by Naresh

        #region Student_deatils

        public int AddProjectDetails(StudentInformation objSI)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_constr);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[20];
                objParams[0] = new SqlParameter("@P_IDNO", objSI.IDNO);
                objParams[1] = new SqlParameter("@P_SEMESTERNO", objSI.Semesterno);
                objParams[2] = new SqlParameter("@P_PROJECT_NAME", objSI.ProjectName);
                objParams[3] = new SqlParameter("@P_INDUSTRY_NAME", objSI.IndustryName);
                objParams[4] = new SqlParameter("@P_ADDRESS_INDUSTRY", objSI.IndustryAddress);
                objParams[5] = new SqlParameter("@P_PROJECT_DETAILS", objSI.Project_Details);
                objParams[6] = new SqlParameter("@P_PROJECT_FROM", objSI.From_date);
                objParams[7] = new SqlParameter("@P_PROJECT_TO", objSI.To_date);
                objParams[8] = new SqlParameter("@P_DURATION", objSI.Duration);
                objParams[9] = new SqlParameter("@P_GRANT_RECEIVED", objSI.Grant_Received);
                objParams[10] = new SqlParameter("@P_SUPERVISOR_DETAILS", objSI.Supervisor_Details);
                objParams[11] = new SqlParameter("@P_MENTOR_COLLEGE", objSI.Mentor_College);
                objParams[12] = new SqlParameter("@P_PROJECT_TYPE", objSI.Project_Type);
                objParams[13] = new SqlParameter("@P_INDUSTRY_TYPE", objSI.Industry_Type);
                objParams[14] = new SqlParameter("@P_UA_NO", objSI.Ua_no);
                objParams[15] = new SqlParameter("@P_IP_ADDRESS", objSI.Ip_address);
                objParams[16] = new SqlParameter("@P_COLLEGE_CODE", objSI.College_code);
                objParams[17] = (objSI.ProjectFilename != "") ? new SqlParameter("@P_PROJECT_FILE_NAME", objSI.ProjectFilename) : new SqlParameter("@P_PROJECT_FILE_NAME", DBNull.Value);
                objParams[18] = (objSI.ProjectFilePath != "") ? new SqlParameter("@P_PROJECT_FILE_PATH", objSI.ProjectFilePath) : new SqlParameter("@P_PROJECT_FILE_PATH", DBNull.Value);

                objParams[19] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[19].Direction = ParameterDirection.Output;

                //object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_STUDENT_PROJECT_DETAILS", objParams, true);        BEFORE UPLOADING FILE
                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_STUDENT_PROJECT_DETAILS_03042020", objParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Academic.AddProjectDetails-> " + ex.ToString());
            }

            return retStatus;
        }

        public int AddPublicationsDetails(StudentInformation objSI)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_constr);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[16];
                objParams[0] = new SqlParameter("@P_IDNO", objSI.IDNO);
                objParams[1] = new SqlParameter("@P_SEMESTERNO", objSI.Semesterno);
                objParams[2] = new SqlParameter("@P_TITLE_NAME", objSI.Title_name);
                objParams[3] = new SqlParameter("@P_JOURNAL_NAME", objSI.Journal_name);
                objParams[4] = new SqlParameter("@P_VOLUME", objSI.Volume);
                objParams[5] = new SqlParameter("@P_PAGE_NO", objSI.Page_no);
                objParams[6] = new SqlParameter("@P_ISSUE_NO", objSI.Issue_no);
                objParams[7] = new SqlParameter("@P_AUTHORS", objSI.Authors);

                objParams[8] = new SqlParameter("@P_PUB_MONTH", objSI.Month);
                objParams[9] = new SqlParameter("@P_PUB_YEAR", objSI.Year);

                objParams[10] = new SqlParameter("@P_UA_NO", objSI.Ua_no);
                objParams[11] = new SqlParameter("@P_IP_ADDRESS", objSI.Ip_address);
                objParams[12] = new SqlParameter("@P_COLLEGE_CODE", objSI.College_code);

                objParams[13] = (objSI.ProjectFilename != "") ? new SqlParameter("@P_PUBLICATIONS_FILE_NAME", objSI.ProjectFilename) : new SqlParameter("@P_PUBLICATIONS_FILE_NAME", DBNull.Value);
                objParams[14] = (objSI.ProjectFilePath != "") ? new SqlParameter("@P_PUBLICATIONS_FILE_PATH", objSI.ProjectFilePath) : new SqlParameter("@P_PUBLICATIONS_FILE_PATH", DBNull.Value);

                objParams[15] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[15].Direction = ParameterDirection.Output;

                //object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_STUDENT_PUBLICATIONS_DETAILS", objParams, true);   BEFORE UPLOADING FILE
                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_STUDENT_PUBLICATIONS_DETAILS_03042020", objParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Academic.AddPublicationsDetails-> " + ex.ToString());
            }

            return retStatus;
        }

        public int AddPublicatonAuthorDetails(StudentInformation objSI)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_constr);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[8];
                objParams[0] = new SqlParameter("@P_IDNO", objSI.IDNO);
                objParams[1] = new SqlParameter("@P_SEMESTERNO", objSI.Semesterno);
                objParams[2] = new SqlParameter("@P_AUTHOR_ROLE", objSI.Author_role);
                objParams[3] = new SqlParameter("@P_AUTHOR_NAME", objSI.Author_name);
                objParams[4] = new SqlParameter("@P_UA_NO", objSI.Ua_no);
                objParams[5] = new SqlParameter("@P_IP_ADDRESS", objSI.Ip_address);
                objParams[6] = new SqlParameter("@P_COLLEGE_CODE", objSI.College_code);

                objParams[7] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[7].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_STUDENT_PUBLICATION_AUTHORS_DETAILS_03042020", objParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Academic.AddPublicatonAuthorDetails-> " + ex.ToString());
            }

            return retStatus;
        }

        public int AddBankDetails(StudentInformation objSI)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_constr);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[6];
                objParams[0] = new SqlParameter("@P_IDNO", objSI.IDNO);
                objParams[1] = new SqlParameter("@P_BANKNO", objSI.Bankno);
                objParams[2] = new SqlParameter("@P_ACC_NO", objSI.Acc_no);
                objParams[3] = new SqlParameter("@P_BANK_BRANCH", objSI.Bank_branch);
                objParams[4] = new SqlParameter("@P_IFSC_CODE", objSI.Ifsc_code);

                objParams[5] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[5].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_STUDENT_BANK_DETAILS", objParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Academic.AddBankDetails-> " + ex.ToString());
            }

            return retStatus;
        }

        public int AddScholarshipDetails(StudentInformation objSI)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_constr);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[16];
                objParams[0] = new SqlParameter("@P_IDNO", objSI.IDNO);
                objParams[1] = new SqlParameter("@P_SEMESTERNO", objSI.Semesterno);
                objParams[2] = new SqlParameter("@P_SCHOLARSHIP_NAME", objSI.Scholarship_name);
                objParams[3] = new SqlParameter("@P_ORGANIZATION_NAME", objSI.Organization_name);
                objParams[4] = new SqlParameter("@P_APPLIED_DATE", objSI.Apply_date);
                objParams[5] = new SqlParameter("@P_SANCTIONED_DATE", objSI.Sanction_date);
                objParams[6] = new SqlParameter("@P_ORGANIZATION_TYPE", objSI.Organization_type);
                objParams[7] = new SqlParameter("@P_AMT_APPLIED", objSI.Amt_applied);
                objParams[8] = new SqlParameter("@P_AMT_SANCTIONED", objSI.Amt_sanctioned);
                objParams[9] = new SqlParameter("@P_PAYMENT_DETAILS", objSI.Payment_details);
                objParams[10] = new SqlParameter("@P_STUDENT_BANK_DETAILS", objSI.Student_bank_details);
                objParams[11] = new SqlParameter("@P_UA_NO", objSI.Ua_no);
                objParams[12] = new SqlParameter("@P_IP_ADDRESS", objSI.Ip_address);
                objParams[13] = new SqlParameter("@P_COLLEGE_CODE", objSI.College_code);
                objParams[14] = new SqlParameter("@P_FEE_WAIVER", objSI.feewavier);

                objParams[15] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[15].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_STUDENT_SCHOLARSHIP_DETAILS", objParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Academic.AddScholarshipDetails-> " + ex.ToString());
            }

            return retStatus;
        }

        public int AddAchievementsDetails(StudentInformation objSI)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_constr);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[14];
                objParams[0] = new SqlParameter("@P_IDNO", objSI.IDNO);
                objParams[1] = new SqlParameter("@P_SEMESTERNO", objSI.Semesterno);
                objParams[2] = new SqlParameter("@P_CERTIFICATE_NAME", objSI.Certificate_name);
                objParams[3] = new SqlParameter("@P_ORGANIZATION_NAME", objSI.Organization_name);
                objParams[4] = new SqlParameter("@P_ORGANIZATION_ADDRESS", objSI.Organization_address);
                objParams[5] = new SqlParameter("@P_DATE_RECEIVED", objSI.Date_received);
                objParams[6] = new SqlParameter("@P_AWARD_DETAILS", objSI.Award_details);
                objParams[7] = new SqlParameter("@P_AMT_RECEIVED", objSI.Amt_received);

                objParams[8] = new SqlParameter("@P_UA_NO", objSI.Ua_no);
                objParams[9] = new SqlParameter("@P_IP_ADDRESS", objSI.Ip_address);
                objParams[10] = new SqlParameter("@P_COLLEGE_CODE", objSI.College_code);

                objParams[11] = (objSI.ProjectFilename != "") ? new SqlParameter("@P_ACHIEVEMENTS_FILE_NAME", objSI.ProjectFilename) : new SqlParameter("@P_ACHIEVEMENTS_FILE_NAME", DBNull.Value);
                objParams[12] = (objSI.ProjectFilePath != "") ? new SqlParameter("@P_ACHIEVEMENTS_FILE_PATH", objSI.ProjectFilePath) : new SqlParameter("@P_ACHIEVEMENTS_FILE_PATH", DBNull.Value);

                objParams[13] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[13].Direction = ParameterDirection.Output;

                //object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_STUDENT_ACHIEVEMENTS_CERTIFICATE_DETAILS", objParams, true);  BEFORE UPLOADING FILES
                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_STUDENT_ACHIEVEMENTS_CERTIFICATE_DETAILS_03042020", objParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Academic.AddAchievementsDetails-> " + ex.ToString());
            }

            return retStatus;
        }

        public DataSet GetStudentProjectDetails(int idno, string semesterno)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[2];
                objParams[0] = new SqlParameter("@P_IDNO", idno);
                objParams[1] = new SqlParameter("@P_SEMESTERNO", semesterno);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_STUDENT_GET_PROJECT_DETAILS_03042020", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentProjectDetails-> " + ex.ToString());
            }
            return ds;
        }

        public DataSet GetStudentProjectDocs(int project)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_PTNO", project);
                //  objParams[1] = new SqlParameter("@P_SEMESTERNO", semesterno);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_STUDENT_GET_PROJECT_BY_PTNO", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentProjectDetails-> " + ex.ToString());
            }
            return ds;
        }

        public DataSet GetStudentInternshipDetails(int idno, string semesterno)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[2];
                objParams[0] = new SqlParameter("@P_IDNO", idno);
                objParams[1] = new SqlParameter("@P_SEMESTERNO", semesterno);

                //ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_STUDENT_GET_INTERNSHIP_DETAILS", objParams);    BEFORE UPLOADING
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_STUDENT_GET_INTERNSHIP_DETAILS_03042020", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentInternshipDetails-> " + ex.ToString());
            }
            return ds;
        }

        public DataSet GetStudentInternshipDocs(int itno)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_ITNO", itno);
                //  objParams[1] = new SqlParameter("@P_SEMESTERNO", semesterno);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_STUDENT_GET_INTERNSHIP_BY_ITNO", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentProjectDetails-> " + ex.ToString());
            }
            return ds;
        }

        public DataSet GetStudentPublicationsAuthorDetails(int idno, string semesterno)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[2];
                objParams[0] = new SqlParameter("@P_IDNO", idno);
                objParams[1] = new SqlParameter("@P_SEMESTERNO", semesterno);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_STUDENT_GET_PUBLICATION_AUTHORS_DETAILS", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentPublicationsAuthorDetails-> " + ex.ToString());
            }
            return ds;
        }

        public DataSet GetStudentPublicationDocs(int pubno)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_PUBNO", pubno);
                //  objParams[1] = new SqlParameter("@P_SEMESTERNO", semesterno);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_STUDENT_GET_PUBLICATIONS_BY_PUBNO", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentProjectDetails-> " + ex.ToString());
            }
            return ds;
        }

        public DataSet GetStudentPublicationsDetails(int idno, string semesterno)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[2];
                objParams[0] = new SqlParameter("@P_IDNO", idno);
                objParams[1] = new SqlParameter("@P_SEMESTERNO", semesterno);

                //ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_STUDENT_GET_PUBLICATIONS_DETAILS", objParams);     BEFORE UPLOADING FILE
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_STUDENT_GET_PUBLICATIONS_DETAILS_03042020", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentInternshipDetails-> " + ex.ToString());
            }
            return ds;
        }

        public DataSet GetStudentScholarshipDetails(int idno, string semesterno)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[2];
                objParams[0] = new SqlParameter("@P_IDNO", idno);
                objParams[1] = new SqlParameter("@P_SEMESTERNO", semesterno);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_STUDENT_GET_SCHOLARSHIP_DETAILS", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentScholarshipDetails-> " + ex.ToString());
            }
            return ds;
        }

        public DataSet GetStudentAchievementDetails(int idno, string semesterno)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[2];
                objParams[0] = new SqlParameter("@P_IDNO", idno);
                objParams[1] = new SqlParameter("@P_SEMESTERNO", semesterno);

                //ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_STUDENT_GET_ACHIEVEMENTS_CERTIFICATE_DETAILS", objParams);      BEFORE UPOADING FILE
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_STUDENT_GET_ACHIEVEMENTS_CERTIFICATE_DETAILS_03042020", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentAchievementDetails-> " + ex.ToString());
            }
            return ds;
        }

        public DataSet GetStudentAchievementsDocs(int CERT_NO)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_CERT_NO", CERT_NO);
                //  objParams[1] = new SqlParameter("@P_SEMESTERNO", semesterno);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_STUDENT_GET_ACHIEVEMENTS_CERTIFICATE_BY_CERT_NO", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentProjectDetails-> " + ex.ToString());
            }
            return ds;
        }

        #endregion Student_deatils

        public int DeleteProjectDocs(int projectdocsno)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_constr);
                SqlParameter[] objParams = new SqlParameter[]
                        {
                        new SqlParameter("@P_PROJECTS_DOCS_NO", projectdocsno),
                        new SqlParameter("@P_OUT", SqlDbType.Int)
                        };

                objParams[objParams.Length - 1].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_STUDENT_DELETE_PROJECT_DOCS_BY_NO", objParams, true);

                if (ret != null && ret.ToString() != "-99")

                    retStatus = Convert.ToInt32(CustomStatus.RecordDeleted);
                else
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.AllotStudentBranch-> " + ex.ToString());
            }
            return retStatus;
        }

        public int DeleteInternshipDocs(int interndocsno)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_constr);
                SqlParameter[] objParams = new SqlParameter[]
                        {
                        new SqlParameter("@P_ITERN_DOCS_NO", interndocsno),
                        new SqlParameter("@P_OUT", SqlDbType.Int)
                        };

                objParams[objParams.Length - 1].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_STUDENT_DELETE_INTERNSHIP_DOCS_BY_NO", objParams, true);

                if (ret != null && ret.ToString() != "-99")

                    retStatus = Convert.ToInt32(CustomStatus.RecordDeleted);
                else
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistrationController.DeleteInternshipDocs-> " + ex.ToString());
            }
            return retStatus;
        }

        public int DeleteAchievementsCertificateDocs(int certdocsno)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_constr);
                SqlParameter[] objParams = new SqlParameter[]
                        {
                        new SqlParameter("@P_CERT_DOCS_NO", certdocsno),
                        new SqlParameter("@P_OUT", SqlDbType.Int)
                        };

                objParams[objParams.Length - 1].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_STUDENT_DELETE_ACHIEVEMENTS_CERTIFICATE_DOCS_BY_NO", objParams, true);

                if (ret != null && ret.ToString() != "-99")

                    retStatus = Convert.ToInt32(CustomStatus.RecordDeleted);
                else
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistrationController.DeleteAchievementsCertificateDocs-> " + ex.ToString());
            }
            return retStatus;
        }

        public int DeletePublicationsDocs(int pubdocsno)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_constr);
                SqlParameter[] objParams = new SqlParameter[]
                        {
                        new SqlParameter("@P_PUB_DOCS_NO", pubdocsno),
                        new SqlParameter("@P_OUT", SqlDbType.Int)
                        };

                objParams[objParams.Length - 1].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_STUDENT_DELETE_PUBLICATIONS_DOCS_BY_NO", objParams, true);

                if (ret != null && ret.ToString() != "-99")

                    retStatus = Convert.ToInt32(CustomStatus.RecordDeleted);
                else
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistrationController.DeletePublicationsDocs-> " + ex.ToString());
            }
            return retStatus;
        }

        #region Industrial_Related Insert

        public int AddIndustrialVisitDetails(StudentInformation objSI)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_constr);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[17];
                objParams[0] = new SqlParameter("@P_INDUSTRIAL_TBL", objSI.Student_data);
                objParams[1] = new SqlParameter("@P_INDUSTRY_NAME", objSI.Industry_name);
                objParams[2] = new SqlParameter("@P_ADDRESS_INDUSTRY", objSI.Address_industry);
                objParams[3] = new SqlParameter("@P_VISIT_DETAILS", objSI.Visit_purpose);
                objParams[4] = new SqlParameter("@P_VISIT_DATE", objSI.Visit_date);
                objParams[5] = new SqlParameter("@P_STUDENTSNO_VISIT", objSI.no_Students);
                objParams[6] = new SqlParameter("@P_FACULTIESNO_VISIT", objSI.no_Faculities);
                objParams[7] = new SqlParameter("@P_BRANCHNO", objSI.branchno);
                objParams[8] = new SqlParameter("@P_SEMESTERNO", objSI.semesterno);
                objParams[9] = new SqlParameter("@P_SECTIONNO", objSI.sectiono);
                objParams[10] = new SqlParameter("@P_ADM_YEAR", objSI.admyear);

                objParams[11] = new SqlParameter("@P_UA_NO", objSI.Ua_no);
                objParams[12] = new SqlParameter("@P_IP_ADDRESS", objSI.Ip_address);
                objParams[13] = new SqlParameter("@P_COLLEGE_CODE", objSI.College_code);

                objParams[14] = (objSI.Indus_file_name != "") ? new SqlParameter("@P_INDUSTRIAL_VISIT_FILE_NAME", objSI.Indus_file_name) : new SqlParameter("@P_INDUSTRIAL_VISIT_FILE_NAME", DBNull.Value);
                objParams[15] = (objSI.Indus_file_path != "") ? new SqlParameter("@P_INDUSTRIAL_VISIT_FILE_PATH", objSI.Indus_file_path) : new SqlParameter("@P_INDUSTRIAL_VISIT_FILE_PATH", DBNull.Value);

                objParams[16] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[16].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_STUDENT_INDUSTRIAL_VISIT_DETAILS", objParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Academic.AddIndustrialVisitDetails-> " + ex.ToString());
            }

            return retStatus;
        }

        public DataSet GetIndustrialVisitDetails(int branchno, int semesterno, int admbatchno)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[3];
                objParams[0] = new SqlParameter("@P_BRANCHNO", branchno);
                objParams[1] = new SqlParameter("@P_SEMESTERNO", semesterno);
                objParams[2] = new SqlParameter("@P_ADMBATCHNO", admbatchno);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_STUDENT_GET_INDUSTRIAL_VISIT_DETAILS", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentProjectDetails-> " + ex.ToString());
            }
            return ds;
        }

        public DataSet GetStudentIndustrialVisitDocs(int visit)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_VISITNO", visit);
                //  objParams[1] = new SqlParameter("@P_SEMESTERNO", semesterno);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_STUDENT_GET_INDUSTRIAL_VISIT_BY_VISITNO", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentProjectDetails-> " + ex.ToString());
            }
            return ds;
        }

        public int DeleteIndustrialVisitDocs(int IndustrialVisitdocsno)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_constr);
                SqlParameter[] objParams = new SqlParameter[]
                        {
                        new SqlParameter("@P_INDUS_DOCS_NO", IndustrialVisitdocsno),
                        new SqlParameter("@P_OUT", SqlDbType.Int)
                        };

                objParams[objParams.Length - 1].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_STUDENT_DELETE_INDUSTRIAL_VISIT_DOCS_BY_NO", objParams, true);

                if (ret != null && ret.ToString() != "-99")

                    retStatus = Convert.ToInt32(CustomStatus.RecordDeleted);
                else
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.AllotStudentBranch-> " + ex.ToString());
            }
            return retStatus;
        }

        //public int AddIndustrialLinkDetails(StudentInformation objSI)
        //{
        //    int retStatus = Convert.ToInt32(CustomStatus.Others);
        //    try
        //    {
        //        SQLHelper objSQLHelper = new SQLHelper(_constr);
        //        SqlParameter[] objParams = null;

        //        objParams = new SqlParameter[15];

        //        objParams[0] = new SqlParameter("@P_COMPANY_NAME", objSI.Company_name);
        //        objParams[1] = new SqlParameter("@P_COMPANY_ADDRESS", objSI.Company_address);
        //        objParams[2] = new SqlParameter("@P_MOU_FROM", objSI.Mou_from);
        //        objParams[3] = new SqlParameter("@P_MOU_TO", objSI.Mou_to);
        //        objParams[4] = new SqlParameter("@P_MOU_TYPE", objSI.Mou_type);
        //        objParams[5] = new SqlParameter("@P_ACTIVITIES", objSI.Activities);
        //        objParams[6] = new SqlParameter("@P_REMARKS", objSI.Remarks);
        //        objParams[7] = new SqlParameter("@P_LIVE_STATUS", objSI.Live_status);

        //        //    objParams[12] = new SqlParameter("@P_UA_NO			, objSI.Amt_received);
        //        //    objParams[13] = new SqlParameter("@P_IP_ADDRESS		, objSI.Month);
        //        //    objParams[14] = new SqlParameter("@P_ENTRY_DATE		, objSI.Year);
        //        //    objParams[15] = new SqlParameter("@P_COLLEGE_CODE		, objSI.Author_role);
        //        // objParams[11] = new SqlParameter(", objSI.Author_name);

        //        objParams[8] = new SqlParameter("@P_UA_NO", objSI.Ua_no);
        //        objParams[9] = new SqlParameter("@P_IP_ADDRESS", objSI.Ip_address);
        //        objParams[10] = new SqlParameter("@P_COLLEGE_CODE", objSI.College_code);

        //        objParams[11] = (objSI.Indus_file_name != "") ? new SqlParameter("@P_INDUSTRIAL_LINK_FILE_NAME", objSI.Indus_file_name) : new SqlParameter("@P_INDUSTRIAL_LINK_FILE_NAME", DBNull.Value);
        //        objParams[12] = (objSI.Indus_file_path != "") ? new SqlParameter("@P_INDUSTRIAL_LINK_FILE_PATH", objSI.Indus_file_path) : new SqlParameter("@P_INDUSTRIAL_LINK_FILE_PATH", DBNull.Value);

        //        objParams[13] = new SqlParameter("@P_INDLINKNO", objSI.Indlinkno);

        //        objParams[14] = new SqlParameter("@P_OUT", SqlDbType.Int);
        //        objParams[14].Direction = ParameterDirection.Output;

        //        //object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_STUDENT_PUBLICATIONS_DETAILS", objParams, true);   BEFORE UPLOADING FILE
        //        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_STUDENT_INDUSTRIAL_LINK_DETAILS", objParams, true);
        //        if (Convert.ToInt32(ret) == -99)
        //            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
        //        else if (Convert.ToInt32(ret) == 1)
        //            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
        //        else
        //            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

        //    }
        //    catch (Exception ex)
        //    {
        //        retStatus = Convert.ToInt32(CustomStatus.Error);
        //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Academic.AddIndustrialLinkDetails-> " + ex.ToString());
        //    }

        //    return retStatus;
        //}
        public DataSet GetIndustrialLinkDetails(int branchno, int semesterno, int admbatchno)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[3];
                objParams[0] = new SqlParameter("@P_BRANCHNO", branchno);
                objParams[1] = new SqlParameter("@P_SEMESTERNO", semesterno);
                objParams[2] = new SqlParameter("@P_ADMBATCHNO", admbatchno);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_STUDENT_GET_INDUSTRIAL_VISIT_DETAILS", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentProjectDetails-> " + ex.ToString());
            }
            return ds;
        }

        public int DeleteIndustrialLinkDocs(int IndustrialLinkdocsno)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_constr);
                SqlParameter[] objParams = new SqlParameter[]
                        {
                        new SqlParameter("@P_INDLINK_DOCS_NO", IndustrialLinkdocsno),
                        new SqlParameter("@P_OUT", SqlDbType.Int)
                        };

                objParams[objParams.Length - 1].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_STUDENT_DELETE_INDUSTRIAL_LINK_DOCS_BY_NO", objParams, true);

                if (ret != null && ret.ToString() != "-99")

                    retStatus = Convert.ToInt32(CustomStatus.RecordDeleted);
                else
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.AllotStudentBranch-> " + ex.ToString());
            }
            return retStatus;
        }

        public DataSet GetStudentInternshipApprovedDetails(int degreeno, int branchno, int semesterno)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[3];
                objParams[0] = new SqlParameter("@P_DEGREENO", degreeno);
                objParams[1] = new SqlParameter("@P_BRANCHNO", branchno);
                objParams[2] = new SqlParameter("@P_SEMESTERNO", semesterno);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_INTERNSHIP_APPROVEL_DETAILS", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentInternshipDetails-> " + ex.ToString());
            }
            return ds;
        }

        public int ApprovedStudentInternshipDetails(int InterNo, int approveby, int app_uano)
        {
            int status = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_constr);
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_ITNO",InterNo),
                    new SqlParameter("@P_APPROVED_BY",approveby),
                    new SqlParameter("@P_UANO",app_uano),
                    new SqlParameter("@P_OUT", SqlDbType.Int)
                };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;
                status = (Int32)objDataAccess.ExecuteNonQuerySP("PKG_ACD_APPROVED_STUDENT_INTERNSHIP_DETAILS_BY_HOD", sqlParams, true);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.SessionActivityController.AddSessionActivity() --> " + ex.Message + " " + ex.StackTrace);
            }
            return status;
        }

        public int AddInternshipDetails(StudentInformation objSI)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_constr);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[19];
                objParams[0] = new SqlParameter("@P_IDNO", objSI.IDNO);
                objParams[1] = new SqlParameter("@P_SEMESTERNO", objSI.Semesterno);
                objParams[2] = new SqlParameter("@P_INDUSTRY_NAME", objSI.Industry_name);
                objParams[3] = new SqlParameter("@P_ADDRESS_INDUSTRY", objSI.Address_industry);
                objParams[4] = new SqlParameter("@P_INTERNSHIP_DETAILS", objSI.Internship_details);
                objParams[5] = new SqlParameter("@P_FROM_DATE", objSI.From_date);
                objParams[6] = new SqlParameter("@P_TO_DATE", objSI.To_date);
                objParams[7] = new SqlParameter("@P_DURATION", objSI.Duration);
                objParams[8] = new SqlParameter("@P_REMARKS", objSI.Remarks);
                objParams[9] = new SqlParameter("@P_STIPEND", objSI.Stipend);
                objParams[10] = new SqlParameter("@P_TECHNICAL_PERSON", objSI.Technical_person);
                objParams[11] = new SqlParameter("@P_MOBILE_NO", objSI.Mobile_no);
                objParams[12] = new SqlParameter("@P_EMAILID", objSI.Emailid);
                objParams[13] = new SqlParameter("@P_UA_NO", objSI.Ua_no);
                objParams[14] = new SqlParameter("@P_IP_ADDRESS", objSI.Ip_address);
                objParams[15] = new SqlParameter("@P_COLLEGE_CODE", objSI.College_code);
                objParams[16] = (objSI.ProjectFilename != "") ? new SqlParameter("@P_INTERNSHIP_FILE_NAME", objSI.ProjectFilename) : new SqlParameter("@P_INTERNSHIP_FILE_NAME", DBNull.Value);
                objParams[17] = (objSI.ProjectFilePath != "") ? new SqlParameter("@P_INTERNSHIP_FILE_PATH", objSI.ProjectFilePath) : new SqlParameter("@P_INTERNSHIP_FILE_PATH", DBNull.Value);

                objParams[18] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[18].Direction = ParameterDirection.Output;

                //object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_STUDENT_INTERNSHIP_DETAILS", objParams, true);     BEFORE UPLOADING FILE
                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_STUDENT_INTERNSHIP_DETAILS_03042020", objParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Academic.AddInternshipDetails-> " + ex.ToString());
            }

            return retStatus;
        }

        // Added by Naresh Beerla for Generating the Internship Details in Excel Format on 09072020
        public DataSet GetStudentInternshipDetails_Excel(int admbatch, string degreeno, string branchno, int semesterno)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_constr);
                SqlParameter[] objParams = new SqlParameter[4];
                objParams[0] = new SqlParameter("@P_ADMBATCH", admbatch);
                objParams[1] = new SqlParameter("@P_DEGREENO", degreeno);
                objParams[2] = new SqlParameter("@P_BRANCHNO", branchno);
                objParams[3] = new SqlParameter("@P_SEMESTERNO", semesterno);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_STUDENT_GET_INTERNSHIP_DETAILS_FOR_EXCEL", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.StudentAdmRegister-> " + ex.ToString());
            }

            return ds;
        }

        public int AddIndustrialLinkDetails(StudentInformation objSI)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_constr);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[18];

                objParams[0] = new SqlParameter("@P_COMPANY_NAME", objSI.Company_name);
                objParams[1] = new SqlParameter("@P_COMPANY_ADDRESS", objSI.Company_address);
                objParams[2] = new SqlParameter("@P_MOU_FROM", objSI.Mou_from);
                objParams[3] = new SqlParameter("@P_MOU_TO", objSI.Mou_to);
                objParams[4] = new SqlParameter("@P_MOU_TYPE", objSI.Mou_type);
                objParams[5] = new SqlParameter("@P_ACTIVITIES", objSI.Activities);
                objParams[6] = new SqlParameter("@P_REMARKS", objSI.Remarks);
                objParams[7] = new SqlParameter("@P_LIVE_STATUS", objSI.Live_status);

                //    objParams[12] = new SqlParameter("@P_UA_NO			, objSI.Amt_received);
                //    objParams[13] = new SqlParameter("@P_IP_ADDRESS		, objSI.Month);
                //    objParams[14] = new SqlParameter("@P_ENTRY_DATE		, objSI.Year);
                //    objParams[15] = new SqlParameter("@P_COLLEGE_CODE		, objSI.Author_role);
                // objParams[11] = new SqlParameter(", objSI.Author_name);

                objParams[8] = new SqlParameter("@P_UA_NO", objSI.Ua_no);
                objParams[9] = new SqlParameter("@P_IP_ADDRESS", objSI.Ip_address);
                objParams[10] = new SqlParameter("@P_COLLEGE_CODE", objSI.College_code);
                objParams[11] = new SqlParameter("@P_STUD_PART", objSI.Stud_Par);
                objParams[12] = new SqlParameter("@P_TEA_PART", objSI.Teacher_par);
                objParams[13] = new SqlParameter("@P_LINK", objSI.Link);

                objParams[14] = (objSI.Indus_file_name != "") ? new SqlParameter("@P_INDUSTRIAL_LINK_FILE_NAME", objSI.Indus_file_name) : new SqlParameter("@P_INDUSTRIAL_LINK_FILE_NAME", DBNull.Value);
                objParams[15] = (objSI.Indus_file_path != "") ? new SqlParameter("@P_INDUSTRIAL_LINK_FILE_PATH", objSI.Indus_file_path) : new SqlParameter("@P_INDUSTRIAL_LINK_FILE_PATH", DBNull.Value);

                objParams[16] = new SqlParameter("@P_INDLINKNO", objSI.Indlinkno);

                objParams[17] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[17].Direction = ParameterDirection.Output;

                //object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_STUDENT_PUBLICATIONS_DETAILS", objParams, true);   BEFORE UPLOADING FILE
                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_STUDENT_INDUSTRIAL_LINK_DETAILS", objParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else if (Convert.ToInt32(ret) == 1)
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Academic.AddIndustrialLinkDetails-> " + ex.ToString());
            }

            return retStatus;
        }

        #region Extension Activity

        public int AddExtensionActivityDetails(StudentInformation objSI, int Activity_no)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_constr);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[14];

                objParams[0] = new SqlParameter("@P_ACTIVITY_NAME", objSI.Activity_Name);
                objParams[1] = new SqlParameter("@P_AWARD_NAME", objSI.Award_Name);
                objParams[2] = new SqlParameter("@P_AWARD_GOVT_NAME", objSI.Award_GovtName);
                objParams[3] = new SqlParameter("@P_AWARD_DATE", objSI.Award_Date);
                objParams[4] = new SqlParameter("@P_ORGANIZING_NAME", objSI.Organization_unit);
                objParams[5] = new SqlParameter("@P_SCHEME_NAME", objSI.Scheme_Name);
                objParams[6] = new SqlParameter("@P_STUDENT_PARTICIPATED", objSI.Student_Participated);
                objParams[7] = new SqlParameter("@P_UA_NO", objSI.Ua_no);
                objParams[8] = new SqlParameter("@P_IP_ADDRESS", objSI.Ip_address);
                objParams[9] = new SqlParameter("@P_COLLEGE_CODE", objSI.College_code);
                objParams[10] = new SqlParameter("@P_ACTIVITY_NO", Activity_no);

                objParams[11] = (objSI.FileName != "") ? new SqlParameter("@P_FILE_NAME", objSI.FileName) : new SqlParameter("@P_FILE_NAME", DBNull.Value);
                objParams[12] = (objSI.FilePath != "") ? new SqlParameter("@P_FILE_PATH", objSI.FilePath) : new SqlParameter("@P_FILE_PATH", DBNull.Value);

                objParams[13] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[13].Direction = ParameterDirection.Output;

                //object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_STUDENT_PUBLICATIONS_DETAILS", objParams, true);   BEFORE UPLOADING FILE
                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_EXTENSION_ACTIVITY_DETAILS", objParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else if (Convert.ToInt32(ret) == 1)
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Academic.AddExtensionActivityDetails-> " + ex.ToString());
            }

            return retStatus;
        }

        public int DeleteExtensionActivityDocs(int Activitydocno)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_constr);
                SqlParameter[] objParams = new SqlParameter[]
                        {
                        new SqlParameter("@P_ACTIVITY_DOCS_NO", Activitydocno),
                        new SqlParameter("@P_OUT", SqlDbType.Int)
                        };

                objParams[objParams.Length - 1].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_DELETE_EXTENSION_ACTIVITIES_DOCS_BY_NO", objParams, true);

                if (ret != null && ret.ToString() != "-99")

                    retStatus = Convert.ToInt32(CustomStatus.RecordDeleted);
                else
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistrationController.DeleteAddExtensionActivityDocs-> " + ex.ToString());
            }
            return retStatus;
        }

        public DataSet GetExtensionActivityDetails()
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_constr);
                SqlParameter[] objParams = new SqlParameter[0];
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_EXTENSION_ACTIVITIES_BY_ACTIVITYNO", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetExtensionActivityDetails-> " + ex.ToString());
            }
            return ds;
        }

        public DataSet GetExtensionActivityDocs(int Activityno)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_ACTIVITYNO", Activityno);
                //  objParams[1] = new SqlParameter("@P_SEMESTERNO", semesterno);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_EXTENSION_ACTIVITIES_DOCS_BY_ACTIVITYNO", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetExtensionActivityDocs-> " + ex.ToString());
            }
            return ds;
        }

        public int DeleteExtensionActivity(int Activityno)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_constr);
                SqlParameter[] objParams = new SqlParameter[]
                        {
                        new SqlParameter("@P_ACTIVITY_NO", Activityno),
                        new SqlParameter("@P_OUT", SqlDbType.Int)
                        };

                objParams[objParams.Length - 1].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_DELETE_EXTENSION_ACTIVITIES_BY_NO", objParams, true);

                if (ret != null && ret.ToString() != "-99")

                    retStatus = Convert.ToInt32(CustomStatus.RecordDeleted);
                else
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistrationController.DeleteAddExtensionActivityDocs-> " + ex.ToString());
            }
            return retStatus;
        }

        // ADD IN THE Extension Activity REGION

        // Added by Naresh Beerla for Generating the Extension Details in Excel Format on 03122020
        public DataSet GetStudentExtensionDetails_Excel()
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_constr);
                SqlParameter[] objParams = new SqlParameter[0];

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_STUDENT_GET_EXTENSION_ACTIVITIES_DETAILS_FOR_EXCEL", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentMouDetails_Excel-> " + ex.ToString());
            }

            return ds;
        }

        // ADD IN THE INDUSTRIAL Related Insert REGION

        // Added by Naresh Beerla for Generating the Industrail Link (MOU DETAILS) in Excel Format on 03122020
        public DataSet GetStudentMouDetails_Excel()
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_constr);
                SqlParameter[] objParams = new SqlParameter[0];

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_STUDENT_GET_INDUSTRIAL_LINK_DETAILS_FOR_EXCEL", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentMouDetails_Excel-> " + ex.ToString());
            }

            return ds;
        }

        #endregion Extension Activity

        //Ends Here

        #endregion Industrial_Related Insert

        public int AddUpdateStudentEntranceDetail(StudentRegistrationModel objSR)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_constr);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[18];
                objParams[0] = new SqlParameter("@P_IDNO", objSR.IDNO);
                objParams[1] = (objSR.CutOffMarks > 0) ? new SqlParameter("@P_CUTOFF_MARKS", objSR.CutOffMarks) : new SqlParameter("@P_CUTOFF_MARKS", DBNull.Value);
                objParams[2] = (objSR.OverAllMarks > 0) ? new SqlParameter("@P_OVERALL_MARKS", objSR.OverAllMarks) : new SqlParameter("@P_OVERALL_MARKS", DBNull.Value);
                objParams[3] = (objSR.CommunityRank > 0) ? new SqlParameter("@P_COMMUNITY_RANK", objSR.CommunityRank) : new SqlParameter("@P_COMMUNITY_RANK", DBNull.Value);
                objParams[4] = (objSR.OverAllRank > 0) ? new SqlParameter("@P_OVERALL_RANK", objSR.OverAllRank) : new SqlParameter("@P_OVERALL_RANK", DBNull.Value);
                objParams[5] = new SqlParameter("@P_TNEA_APPLICATION_NO", objSR.TNEAApplicationNo);
                objParams[6] = new SqlParameter("@P_ACK_REC_NO", objSR.AcknowledgeRecNo);
                objParams[7] = new SqlParameter("@P_ADM_ORDER_NO", objSR.AdmOrderNo);
                objParams[8] = (objSR.AdvPaymentAmt > 0) ? new SqlParameter("@P_ADV_PAYMENT_AMOUNT", objSR.AdvPaymentAmt) : new SqlParameter("@P_ADV_PAYMENT_AMOUNT", DBNull.Value);
                if (objSR.AdmOrderDate == DateTime.MinValue)
                {
                    objParams[9] = new SqlParameter("@P_ADM_ORDER_DATE", DBNull.Value);
                }
                else
                {
                    objParams[9] = new SqlParameter("@P_ADM_ORDER_DATE", objSR.AdmOrderDate);
                }
                if (objSR.AcknowledgeRecDate == DateTime.MinValue)
                {
                    objParams[10] = new SqlParameter("@P_ACK_RECEIPT_DATE", DBNull.Value);
                }
                else
                {
                    objParams[10] = new SqlParameter("@P_ACK_RECEIPT_DATE", objSR.AcknowledgeRecDate);
                }

                objParams[11] = new SqlParameter("@P_DOTE_APPLICATION_NO", objSR.DoteApplicationNo);
                objParams[12] = new SqlParameter("@P_DOTE_ALLOTMENT_ORDER_NO", objSR.DoteAllotmentOrderNo);

                if (objSR.DoteAllotmentOrderDate == DateTime.MinValue)
                {
                    objParams[13] = new SqlParameter("@P_DOTE_ALLOTMENT_DATE", DBNull.Value);
                }
                else
                {
                    objParams[13] = new SqlParameter("@P_DOTE_ALLOTMENT_DATE", objSR.DoteAllotmentOrderDate);
                }

                objParams[14] = new SqlParameter("@P_ENTRANCE_NAME", objSR.EntranceName);
                objParams[15] = new SqlParameter("@P_ENTRANCE_ROLLNO", objSR.EntranceRollno);
                objParams[16] = new SqlParameter("@P_PERCENTILE", objSR.Percentile);

                objParams[17] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[17].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_SP_STUDENT_ENTRANCE_DETAILS_UPDATE_STUDINFO", objParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Academic.AddUpdateStudentEntranceDetail-> " + ex.ToString());
            }

            return retStatus;
        }

        public int UpdateStudSign(Student objstud)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_constr);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[2];
                objParams[0] = new SqlParameter("@P_IDNO", objstud.IdNo);
                objParams[1] = new SqlParameter("@P_STUD_SIGN", objstud.StudSign);

                if (objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_SP_UPD_STUDENT_SIGN", objParams, false) != null)
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.UpdateStudentSign-> " + ex.ToString());
            }
            return retStatus;
        }

        public int AddUpdateStudRemark(int idno, int year, int remarktype, string remark)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[5];
                objParams[0] = new SqlParameter("@P_IDNO", idno);
                objParams[1] = new SqlParameter("@P_YEAR", year);
                objParams[2] = new SqlParameter("@P_REMARKTYPE", remarktype);
                objParams[3] = new SqlParameter("@P_REMARK", remark);
                objParams[4] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[4].Direction = ParameterDirection.Output;
                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_SUBMIT_STUD_REMARK", objParams, true);

                if (Convert.ToInt32(ret) == 1)
                {
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                }
                else if (Convert.ToInt32(ret) == 2)
                {
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                }
                else
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.AddUpdateStudRemark-> " + ex.ToString());
            }
            return retStatus;
        }

        public int UpdateStudPhoto(Student objstud)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_constr);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[2];

                objParams[0] = new SqlParameter("@P_IDNO", objstud.IdNo);
                objParams[1] = new SqlParameter("@P_STUD_PHOTO", objstud.StudPhoto);

                // if (objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_UPDATE_STUDENT_PHOTO", objParams, false) != null)
                if (objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_SP_UPD_STUDENT_PHOTO", objParams, false) != null)
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.UpdateStudentPhoto-> " + ex.ToString());
            }
            return retStatus;
        }

        public int AddUpdateOnlineStudentReg(StudentRegistrationModel objSR)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_constr);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[37];
                objParams[0] = new SqlParameter("@P_IDNO", objSR.IDNO);
                objParams[1] = new SqlParameter("@P_STUDNAME", objSR.StudentName);
                objParams[2] = new SqlParameter("@P_SURNAME", objSR.Surname);
                objParams[3] = new SqlParameter("@P_DOB", objSR.DateOfBirth);
                objParams[4] = new SqlParameter("@P_GENDER", objSR.Gender);
                objParams[5] = new SqlParameter("@P_ADHAARNO", objSR.AdhaarcardNo);
                objParams[6] = new SqlParameter("@P_BLOODGROUPNO", objSR.BloogGroupNo);
                objParams[7] = new SqlParameter("@P_WILLINGTOBLOODONATE", objSR.IsBloodDonate);
                objParams[8] = new SqlParameter("@P_ISPHYSICALHANDICAP", objSR.IsPhysicalHandicap);
                objParams[9] = new SqlParameter("@P_TYPEOFHANDICAP", objSR.TypeOfHandicap);
                objParams[10] = new SqlParameter("@P_ALLERGY_DETAILS", objSR.AlergyDetails);
                objParams[11] = new SqlParameter("@P_MOTHER_TONGUE", objSR.MotherTongue);
                objParams[12] = new SqlParameter("@P_CITIZENSHIP", objSR.Citizenship);
                objParams[13] = new SqlParameter("@P_COMMUNITYCODE", objSR.CommunityCode);
                objParams[14] = new SqlParameter("@P_COMMUNITYNO", objSR.CommunityNo);
                objParams[15] = new SqlParameter("@P_RELIGIONNO", objSR.ReligionNo);
                objParams[16] = new SqlParameter("@P_CASTENO", objSR.CasteNo);
                objParams[17] = new SqlParameter("@P_ISHOSTELLER", objSR.IsHosteller);
                objParams[18] = (objSR.Distance > 0) ? new SqlParameter("@P_DISTANCE", objSR.Distance) : new SqlParameter("@P_DISTANCE", DBNull.Value);
                objParams[19] = new SqlParameter("@P_LANGUAGEKNOWN", objSR.LanguageKnown);
                objParams[20] = new SqlParameter("@P_FLANGUAGEKNOWN", objSR.ForeignLanguageKnown);
                objParams[21] = new SqlParameter("@P_NEEDTRANSPORT", objSR.NeedTransport);
                objParams[22] = new SqlParameter("@P_ACADEMIC_YEARNO", objSR.AcademicYearNo);
                objParams[23] = new SqlParameter("@P_BRANCHNO", objSR.BranchNo);
                objParams[24] = new SqlParameter("@P_ADMISSION_TYPE", objSR.AdmissionType);
                objParams[25] = new SqlParameter("@P_DEGREENO", objSR.DegreeNo);
                objParams[26] = new SqlParameter("@P_UA_NO", objSR.UA_NO);
                objParams[27] = new SqlParameter("@P_SEMESTERNO", objSR.Semesterno);
                objParams[28] = new SqlParameter("@P_NATIVE_PLACE", objSR.NativePlace);
                objParams[29] = new SqlParameter("@P_MARITAL_STATUS", objSR.Marital_Status);
                objParams[30] = new SqlParameter("@P_FIRSTNAME", objSR.FirstName);
                objParams[31] = new SqlParameter("@P_MIDDLENAME", objSR.MiddleName);
                objParams[32] = new SqlParameter("@P_IDENTITY_MARK", objSR.IdentityMark);
                objParams[33] = new SqlParameter("@P_SUBCASTE", objSR.Subcaste);
                objParams[34] = new SqlParameter("@P_IDENTITY_MARK2", objSR.IdentityMark2);
                objParams[35] = new SqlParameter("@P_COLLEGE_ID", objSR.CollegeId);

                objParams[36] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[36].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_INSERT_STUDENT_ONLINE_ADMISSION_DETAILS_STUDINFO", objParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Academic.AddUpdateOnlineStudentReg-> " + ex.ToString());
            }

            return retStatus;
        }

        public int AddUpdateStudentLastExamDetail(StudentRegistrationModel objSR)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_constr);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[172];
                objParams[0] = new SqlParameter("@P_IDNO", objSR.IDNO);
                objParams[1] = new SqlParameter("@P_SSCBOARD_CATEGORY", objSR.SSCBoardCategory);
                objParams[2] = new SqlParameter("@P_SSCLANGUAGE", objSR.SSCLanguage);

                objParams[3] = (objSR.SSCEnglishObtMark > 0) ? new SqlParameter("@P_SSCENGLISH_OBTMARK", objSR.SSCEnglishObtMark) : new SqlParameter("@P_SSCENGLISH_OBTMARK", DBNull.Value);
                objParams[4] = (objSR.SSCEnglishMaxMark > 0) ? new SqlParameter("@P_SSCENGLISH_MAXMARK", objSR.SSCEnglishMaxMark) : new SqlParameter("@P_SSCENGLISH_MAXMARK", DBNull.Value);
                objParams[5] = (objSR.SSCEnglishPer > 0) ? new SqlParameter("@P_SSCENGLISH_PER", objSR.SSCEnglishPer) : new SqlParameter("@P_SSCENGLISH_PER", DBNull.Value);
                objParams[6] = (objSR.SSCMathsObtMark > 0) ? new SqlParameter("@P_SSCMATHS_OBTMARK", objSR.SSCMathsObtMark) : new SqlParameter("@P_SSCMATHS_OBTMARK", DBNull.Value);
                objParams[7] = (objSR.SSCMathsMaxMark > 0) ? new SqlParameter("@P_SSCMATHS_MAXMARK", objSR.SSCMathsMaxMark) : new SqlParameter("@P_SSCMATHS_MAXMARK", DBNull.Value);
                objParams[8] = (objSR.SSCMathsPer > 0) ? new SqlParameter("@P_SSCMATHS_PER", objSR.SSCMathsPer) : new SqlParameter("@P_SSCMATHS_PER", DBNull.Value);
                objParams[9] = (objSR.SSCScienceObtMark > 0) ? new SqlParameter("@P_SSCSCIENCE_OBTMARK", objSR.SSCScienceObtMark) : new SqlParameter("@P_SSCSCIENCE_OBTMARK", DBNull.Value);
                objParams[10] = (objSR.SSCScienceMaxMark > 0) ? new SqlParameter("@P_SSCSCIENCE_MAXMARK", objSR.SSCScienceMaxMark) : new SqlParameter("@P_SSCSCIENCE_MAXMARK", DBNull.Value);
                objParams[11] = (objSR.SSCSciencePer > 0) ? new SqlParameter("@P_SSCSCIENCE_PER", objSR.SSCSciencePer) : new SqlParameter("@P_SSCSCIENCE_PER", DBNull.Value);
                objParams[12] = (objSR.SSCSocialScienceObtMark > 0) ? new SqlParameter("@P_SSCSOCIALSCIENCE_OBTMARK", objSR.SSCSocialScienceObtMark) : new SqlParameter("@P_SSCSOCIALSCIENCE_OBTMARK", DBNull.Value);
                objParams[13] = (objSR.SSCSocialScienceMaxMark > 0) ? new SqlParameter("@P_SSCSOCIALSCIENCE_MAXMARK", objSR.SSCSocialScienceMaxMark) : new SqlParameter("@P_SSCSOCIALSCIENCE_MAXMARK", DBNull.Value);
                objParams[14] = (objSR.SSCSocialSciencePer > 0) ? new SqlParameter("@P_SSCSOCIALSCIENCE_PER", objSR.SSCSocialSciencePer) : new SqlParameter("@P_SSCSOCIALSCIENCE_PER", DBNull.Value);
                objParams[15] = (objSR.SSCTotalMarkScore > 0) ? new SqlParameter("@P_SSCTOTALMARK_SCORE", objSR.SSCTotalMarkScore) : new SqlParameter("@P_SSCTOTALMARK_SCORE", DBNull.Value);
                objParams[16] = new SqlParameter("@P_SSCYEAROF_PASSING", objSR.SSCYearofPassing);
                objParams[17] = new SqlParameter("@P_SSCMEDIUM_OF_INSTRUCTION", objSR.SSCMediumOfInstruction);
                objParams[18] = new SqlParameter("@P_SSCMARK_CERTNO", objSR.SSCMarkCertNo);
                objParams[19] = new SqlParameter("@P_SSCPASS_CERTNO", objSR.SSCPassCertNo);
                objParams[20] = new SqlParameter("@P_SSCTMR_NO", objSR.SSCTMRNo);
                objParams[21] = new SqlParameter("@P_SSCREGISTER_NO", objSR.SSCRegisterNo);
                objParams[22] = new SqlParameter("@P_SSCINSTITUTE_NAME", objSR.SSCInstituteName);
                objParams[23] = (objSR.SSCTotalPer > 0) ? new SqlParameter("@P_SSCTOTAL_PER", objSR.SSCTotalPer) : new SqlParameter("@P_SSCTOTAL_PER", DBNull.Value);
                objParams[24] = new SqlParameter("@P_SSCINSTITUTE_ADDRESS", objSR.SSCInstituteAddress);
                objParams[25] = (objSR.ISEComputerApplicationObtMark > 0) ? new SqlParameter("@P_ISECOMPUTERAPPLICATION_OBTMARK", objSR.ISEComputerApplicationObtMark) : new SqlParameter("@P_ISECOMPUTERAPPLICATION_OBTMARK", DBNull.Value);
                objParams[26] = (objSR.ISEComputerApplicationMaxMark > 0) ? new SqlParameter("@P_ISECOMPUTERAPPLICATION_MAXMARK", objSR.ISEComputerApplicationMaxMark) : new SqlParameter("@P_ISECOMPUTERAPPLICATION_MAXMARK", DBNull.Value);
                objParams[27] = (objSR.ISEComputerApplicationPer > 0) ? new SqlParameter("@P_ISECOMPUTERAPPLICATION_PER", objSR.ISEComputerApplicationPer) : new SqlParameter("@P_ISECOMPUTERAPPLICATION_PER", DBNull.Value);
                objParams[28] = (objSR.ISEHistoryObtMark > 0) ? new SqlParameter("@P_ISEHISTORY_OBTMARK", objSR.ISEHistoryObtMark) : new SqlParameter("@P_ISEHISTORY_OBTMARK", DBNull.Value);
                objParams[29] = (objSR.ISEHistoryMaxMark > 0) ? new SqlParameter("@P_ISEHISTORY_MAXMARK", objSR.ISEHistoryMaxMark) : new SqlParameter("@P_ISEHISTORY_MAXMARK", DBNull.Value);
                objParams[30] = (objSR.ISEHistoryPer > 0) ? new SqlParameter("@P_ISEHISTORY_PER", objSR.ISEHistoryPer) : new SqlParameter("@P_ISEHISTORY_PER", DBNull.Value);
                objParams[31] = new SqlParameter("@P_HSCBOARD_CATEGORY", objSR.HSCBoardCategory);
                objParams[32] = new SqlParameter("@P_HSC_LANGUAGE", objSR.HSCLanguage);
                objParams[33] = (objSR.HSCEnglishObtMark > 0) ? new SqlParameter("@P_HSCENGLISH_OBTMARK", objSR.HSCEnglishObtMark) : new SqlParameter("@P_HSCENGLISH_OBTMARK", DBNull.Value);
                objParams[34] = (objSR.HSCEnglishMaxMark > 0) ? new SqlParameter("@P_HSCENGLISH_MAXMARK", objSR.HSCEnglishMaxMark) : new SqlParameter("@P_HSCENGLISH_MAXMARK", DBNull.Value);
                objParams[35] = (objSR.HSCEnglishPer > 0) ? new SqlParameter("@P_HSCENGLISH_PER", objSR.HSCEnglishPer) : new SqlParameter("@P_HSCENGLISH_PER", DBNull.Value);
                objParams[36] = (objSR.HSCMathsObtMark > 0) ? new SqlParameter("@P_HSCMATHS_OBTMARK", objSR.HSCMathsObtMark) : new SqlParameter("@P_HSCMATHS_OBTMARK", DBNull.Value);
                objParams[37] = (objSR.HSCMathsMaxMark > 0) ? new SqlParameter("@P_HSCMATHS_MAXMARK", objSR.HSCMathsMaxMark) : new SqlParameter("@P_HSCMATHS_MAXMARK", DBNull.Value);
                objParams[38] = (objSR.HSCMathsPer > 0) ? new SqlParameter("@P_HSCMATHS_PER", objSR.HSCMathsPer) : new SqlParameter("@P_HSCMATHS_PER", DBNull.Value);
                objParams[39] = (objSR.HSCPhysicsMaxMark > 0) ? new SqlParameter("@P_HSCPHYSICS_MAXMARK", objSR.HSCPhysicsMaxMark) : new SqlParameter("@P_HSCPHYSICS_MAXMARK", DBNull.Value);
                objParams[40] = (objSR.HSCPhysicsObtMark > 0) ? new SqlParameter("@P_HSCPHYSICS_OBTMARK", objSR.HSCPhysicsObtMark) : new SqlParameter("@P_HSCPHYSICS_OBTMARK", DBNull.Value);
                objParams[41] = (objSR.HSCPhysicsPer > 0) ? new SqlParameter("@P_HSCPHYSICS_PER", objSR.HSCPhysicsPer) : new SqlParameter("@P_HSCPHYSICS_PER", DBNull.Value);
                objParams[42] = (objSR.HSCChemMaxMark > 0) ? new SqlParameter("@P_HSCCHEM_MAXMARK", objSR.HSCChemMaxMark) : new SqlParameter("@P_HSCCHEM_MAXMARK", DBNull.Value);
                objParams[43] = (objSR.HSCChemObtMark > 0) ? new SqlParameter("@P_HSCCHEM_OBTMARK", objSR.HSCChemObtMark) : new SqlParameter("@P_HSCCHEM_OBTMARK", DBNull.Value);
                objParams[44] = (objSR.HSCChemPer > 0) ? new SqlParameter("@P_HSCCHEM_PER", objSR.HSCChemPer) : new SqlParameter("@P_HSCCHEM_PER", DBNull.Value);
                objParams[45] = (objSR.HSCOptionalSub > 0) ? new SqlParameter("@P_HSCOPTIONAL_SUB", objSR.HSCOptionalSub) : new SqlParameter("@P_HSCOPTIONAL_SUB", DBNull.Value);
                objParams[46] = (objSR.HSCTotalMarkScore > 0) ? new SqlParameter("@P_HSCTOTALMARK_SCORE", objSR.HSCTotalMarkScore) : new SqlParameter("@P_HSCTOTALMARK_SCORE", DBNull.Value);
                objParams[47] = new SqlParameter("@P_HSCYEAROF_PASSING", objSR.HSCYearofPassing);
                objParams[48] = new SqlParameter("@P_HSCMEDIUMOF_INSTRUCTION", objSR.HSCMediumOfInstruction);
                objParams[49] = new SqlParameter("@P_HSCMARK_CERTIFICATENO", objSR.HSCMarkCertificateNo);
                objParams[50] = new SqlParameter("@P_HSCPASS_CERTIFICATENO", objSR.HSCPassCertificateNo);
                objParams[51] = new SqlParameter("@P_HSCTMR_NO", objSR.HSCTMRNo);
                objParams[52] = new SqlParameter("@P_HSCREGISTER_NO", objSR.HSCRegisterNo);
                objParams[53] = new SqlParameter("@P_HSCINSTITUTE_NAME", objSR.HSCInstituteName);
                objParams[54] = (objSR.HSCTotalPer > 0) ? new SqlParameter("@P_HSCTOTAL_PER", objSR.HSCTotalPer) : new SqlParameter("@P_HSCTOTAL_PER", DBNull.Value);
                objParams[55] = new SqlParameter("@P_HSCINSTITUTE_ADDRESS", objSR.HSCInstituteAddress);
                objParams[56] = (objSR.VocationalTHObtMark > 0) ? new SqlParameter("@P_VOCATIONAL_TH_OBTMARK", objSR.VocationalTHObtMark) : new SqlParameter("@P_VOCATIONAL_TH_OBTMARK", DBNull.Value);
                objParams[57] = (objSR.VocationalTHMaxMark > 0) ? new SqlParameter("@P_VOCATIONAL_TH_MAXMARK", objSR.VocationalTHMaxMark) : new SqlParameter("@P_VOCATIONAL_TH_MAXMARK", DBNull.Value);
                objParams[58] = (objSR.VocationalTHPer > 0) ? new SqlParameter("@P_VOCATIONAL_TH_PER", objSR.VocationalTHPer) : new SqlParameter("@P_VOCATIONAL_TH_PER", DBNull.Value);
                objParams[59] = (objSR.VocationalPR1ObtMark > 0) ? new SqlParameter("@P_VOCATIONAL_PR1_OBTMARK", objSR.VocationalPR1ObtMark) : new SqlParameter("@P_VOCATIONAL_PR1_OBTMARK", DBNull.Value);
                objParams[60] = (objSR.VocationalPR1MaxMark > 0) ? new SqlParameter("@P_VOCATIONAL_PR1_MAXMARK", objSR.VocationalPR1MaxMark) : new SqlParameter("@P_VOCATIONAL_PR1_MAXMARK", DBNull.Value);
                objParams[61] = (objSR.VocationalPR1Per > 0) ? new SqlParameter("@P_VOCATIONAL_PR1_PER", objSR.VocationalPR1Per) : new SqlParameter("@P_VOCATIONAL_PR1_PER", DBNull.Value);
                objParams[62] = (objSR.VocationalPR2ObtMark > 0) ? new SqlParameter("@P_VOCATIONAL_PR2_OBTMARK", objSR.VocationalPR2ObtMark) : new SqlParameter("@P_VOCATIONAL_PR2_OBTMARK", DBNull.Value);
                objParams[63] = (objSR.VocationalPR2MaxMark > 0) ? new SqlParameter("@P_VOCATIONAL_PR2_MAXMARK", objSR.VocationalPR2MaxMark) : new SqlParameter("@P_VOCATIONAL_PR2_MAXMARK", DBNull.Value);
                objParams[64] = (objSR.VocationalPR2Per > 0) ? new SqlParameter("@P_VOCATIONAL_PR2_PER", objSR.VocationalPR2Per) : new SqlParameter("@P_VOCATIONAL_PR2_PER", DBNull.Value);
                objParams[65] = (objSR.SSCLanguageObtMark > 0) ? new SqlParameter("@P_SSCLANG_OBTMARK", objSR.SSCLanguageObtMark) : new SqlParameter("@P_SSCLANG_OBTMARK", DBNull.Value);
                objParams[66] = (objSR.SSCLanguageMaxMark > 0) ? new SqlParameter("@P_SSCLANG_MAXMARK", objSR.SSCLanguageMaxMark) : new SqlParameter("@P_SSCLANG_MAXMARK", DBNull.Value);
                objParams[67] = (objSR.SSCLanguagePer > 0) ? new SqlParameter("@P_SSCLANG_PER", objSR.SSCLanguagePer) : new SqlParameter("@P_SSCLANG_PER", DBNull.Value);
                objParams[68] = (objSR.HSCLanguageObtMark > 0) ? new SqlParameter("@P_HSCLANG_OBTMARK", objSR.HSCLanguageObtMark) : new SqlParameter("@P_HSCLANG_OBTMARK", DBNull.Value);
                objParams[69] = (objSR.HSCLanguageMaxMark > 0) ? new SqlParameter("@P_HSCLANG_MAXMARK", objSR.HSCLanguageMaxMark) : new SqlParameter("@P_HSCLANG_MAXMARK", DBNull.Value);
                objParams[70] = (objSR.HSCLanguagePer > 0) ? new SqlParameter("@P_HSCLANG_PER", objSR.HSCLanguagePer) : new SqlParameter("@P_HSCLANG_PER", DBNull.Value);

                objParams[71] = (objSR.SSCLanguageGdPoint > 0) ? new SqlParameter("@P_SSC_LANGUAGE_GDPOINT", objSR.SSCLanguageGdPoint) : new SqlParameter("@P_SSC_LANGUAGE_GDPOINT", DBNull.Value);
                objParams[72] = new SqlParameter("@P_SSC_LANGUAGE_GRADE", objSR.SSCLanguageGrade);
                objParams[73] = (objSR.SSCEnglishGdPoint > 0) ? new SqlParameter("@P_SSC_ENGLISH_GDPOINT", objSR.SSCEnglishGdPoint) : new SqlParameter("@P_SSC_ENGLISH_GDPOINT", DBNull.Value);
                objParams[74] = new SqlParameter("@P_SSC_ENGLISH_GRADE", objSR.SSCEnglishGrade);
                objParams[75] = (objSR.SSCMathGdPoint > 0) ? new SqlParameter("@P_SSC_MATHS_GDPOINT", objSR.SSCMathGdPoint) : new SqlParameter("@P_SSC_MATHS_GDPOINT", DBNull.Value);
                objParams[76] = new SqlParameter("@P_SSC_MATHS_GRADE", objSR.SSCMathGrade);
                objParams[77] = (objSR.SSCScienceGdPoint > 0) ? new SqlParameter("@P_SSC_SCIENCE_GDPOINT", objSR.SSCScienceGdPoint) : new SqlParameter("@P_SSC_SCIENCE_GDPOINT", DBNull.Value);
                objParams[78] = new SqlParameter("@P_SSC_SCIENCE_GRADE", objSR.SSCScienceGrade);
                objParams[79] = (objSR.SSCSocSciGdPoint > 0) ? new SqlParameter("@P_SSC_SOC_SCIENCE_GDPOINT", objSR.SSCSocSciGdPoint) : new SqlParameter("@P_SSC_SOC_SCIENCE_GDPOINT", DBNull.Value);
                objParams[80] = new SqlParameter("@P_SSC_SOC_SCIENCE_GRADE", objSR.SSCSocSciGrade);
                objParams[81] = (objSR.ISEComputerAppGdPoint > 0) ? new SqlParameter("@P_SSC_COMP_APPLICATION_GDPOINT", objSR.ISEComputerAppGdPoint) : new SqlParameter("@P_SSC_COMP_APPLICATION_GDPOINT", DBNull.Value);
                objParams[82] = new SqlParameter("@P_SSC_COMP_APPLICATION_GRADE", objSR.ISEComputerAppGrade);
                objParams[83] = (objSR.ISEHistoryGdPoint > 0) ? new SqlParameter("@P_SSC_HISTORY_GDPOINT", objSR.ISEHistoryGdPoint) : new SqlParameter("@P_SSC_HISTORY_GDPOINT", DBNull.Value);
                objParams[84] = new SqlParameter("@P_SSC_HISTORY_GRADE", objSR.ISEHistoryGrade);
                objParams[85] = (objSR.HSCLangGdPoint > 0) ? new SqlParameter("@P_HSC_LANGUAGE_GDPOINT", objSR.HSCLangGdPoint) : new SqlParameter("@P_HSC_LANGUAGE_GDPOINT", DBNull.Value);
                objParams[86] = new SqlParameter("@P_HSC_LANGUAGE_GRADE", objSR.HSCLangGrade);
                objParams[87] = (objSR.HSCEnglishGdPoint > 0) ? new SqlParameter("@P_HSC_ENGLISH_GDPOINT", objSR.HSCEnglishGdPoint) : new SqlParameter("@P_HSC_ENGLISH_GDPOINT", DBNull.Value);
                objParams[88] = new SqlParameter("@P_HSC_ENGLISH_GRADE", objSR.HSCEnglishGrade);
                objParams[89] = (objSR.HSCMathGdPoint > 0) ? new SqlParameter("@P_HSC_MATHS_GDPOINT", objSR.HSCMathGdPoint) : new SqlParameter("@P_HSC_MATHS_GDPOINT", DBNull.Value);
                objParams[90] = new SqlParameter("@P_HSC_MATHS_GRADE", objSR.HSCMathGrade);
                objParams[91] = (objSR.HSCPhyGdPoint > 0) ? new SqlParameter("@P_HSC_PHYSICS_GDPOINT", objSR.HSCPhyGdPoint) : new SqlParameter("@P_HSC_PHYSICS_GDPOINT", DBNull.Value);
                objParams[92] = new SqlParameter("@P_HSC_PHYSICS_GRADE", objSR.HSCPhyGrade);
                objParams[93] = (objSR.HSCChemGdPoint > 0) ? new SqlParameter("@P_HSC_CHEMISTRY_GDPOINT", objSR.HSCChemGdPoint) : new SqlParameter("@P_HSC_CHEMISTRY_GDPOINT", DBNull.Value);
                objParams[94] = new SqlParameter("@P_HSC_CHEMISTRY_GRADE", objSR.HSCChemGrade);
                objParams[95] = (objSR.HSCOptionalSubObtMark > 0) ? new SqlParameter("@P_HSC_OPTIONAL_OBT_MARK", objSR.HSCOptionalSubObtMark) : new SqlParameter("@P_HSC_OPTIONAL_OBT_MARK", DBNull.Value);
                objParams[96] = (objSR.HSCOptionalSubMaxMark > 0) ? new SqlParameter("@P_HSC_OPTIONAL_MAX_MARK", objSR.HSCOptionalSubMaxMark) : new SqlParameter("@P_HSC_OPTIONAL_MAX_MARK", DBNull.Value);
                objParams[97] = (objSR.HSCOptionalSubPer > 0) ? new SqlParameter("@P_HSC_OPTIONAL_PER", objSR.HSCOptionalSubPer) : new SqlParameter("@P_HSC_OPTIONAL_PER", DBNull.Value);
                objParams[98] = (objSR.HSCOptionalSubGdPoint > 0) ? new SqlParameter("@P_HSC_OPTIONAL_GDPOINT", objSR.HSCOptionalSubGdPoint) : new SqlParameter("@P_HSC_OPTIONAL_GDPOINT", DBNull.Value);
                objParams[99] = new SqlParameter("@P_HSC_OPTIONAL_GRADE", objSR.HSCOptionalSubGrade);
                objParams[100] = (objSR.VocationalTHGdPoint > 0) ? new SqlParameter("@P_HSC_THEORY_GDPOINT", objSR.VocationalTHGdPoint) : new SqlParameter("@P_HSC_THEORY_GDPOINT", DBNull.Value);
                objParams[101] = new SqlParameter("@P_HSC_THEORY_GRADE", objSR.VocationalTHGrade);
                objParams[102] = (objSR.VocationalPR1GdPoint > 0) ? new SqlParameter("@P_HSC_PR1_GDPOINT", objSR.VocationalPR1GdPoint) : new SqlParameter("@P_HSC_PR1_GDPOINT", DBNull.Value);
                objParams[103] = new SqlParameter("@P_HSC_PR1_GRADE", objSR.VocationalPR1Grade);
                objParams[104] = (objSR.VocationalPR2GdPoint > 0) ? new SqlParameter("@P_HSC_PR2_GDPOINT", objSR.VocationalPR2GdPoint) : new SqlParameter("@P_HSC_PR2_GDPOINT", DBNull.Value);
                objParams[105] = new SqlParameter("@P_HSC_PR2_GRADE", objSR.VocationalPR2Grade);

                objParams[106] = new SqlParameter("@P_DIPLOMA_DEGREE_NAME", objSR.NameofDiploma);
                objParams[107] = new SqlParameter("@P_DIPLOMA_COLLEGE", objSR.DiplomaCollegeName);
                objParams[108] = new SqlParameter("@P_DIPLOMA_BOARD", objSR.DiplomaBoard);
                objParams[109] = new SqlParameter("@P_DIPLOMA_REGNUMBER", objSR.DiplomaRegNumber);
                objParams[110] = new SqlParameter("@P_DIPLOMA_YEAROF_PASSING", objSR.DiplomaYearOfPassing);
                objParams[111] = (objSR.SemIObtainedMark > 0) ? new SqlParameter("@P_SEM_I_OBTMARK", objSR.SemIObtainedMark) : new SqlParameter("@P_SEM_I_OBTMARK", DBNull.Value);
                objParams[112] = (objSR.SemIMaxMark > 0) ? new SqlParameter("@P_SEM_I_MAXMARK", objSR.SemIMaxMark) : new SqlParameter("@P_SEM_I_MAXMARK", DBNull.Value);
                objParams[113] = (objSR.SemIPer > 0) ? new SqlParameter("@P_SEM_I_PER", objSR.SemIPer) : new SqlParameter("@P_SEM_I_PER", DBNull.Value);
                objParams[114] = (objSR.SemIIObtainedMark > 0) ? new SqlParameter("@P_SEM_II_OBTMARK", objSR.SemIIObtainedMark) : new SqlParameter("@P_SEM_II_OBTMARK", DBNull.Value);
                objParams[115] = (objSR.SemIIMaxMark > 0) ? new SqlParameter("@P_SEM_II_MAXMARK", objSR.SemIIMaxMark) : new SqlParameter("@P_SEM_II_MAXMARK", DBNull.Value);
                objParams[116] = (objSR.SemIIPer > 0) ? new SqlParameter("@P_SEM_II_PER", objSR.SemIIPer) : new SqlParameter("@P_SEM_II_PER", DBNull.Value);
                objParams[117] = (objSR.SemIIIObtainedMark > 0) ? new SqlParameter("@P_SEM_III_OBTMARK", objSR.SemIIIObtainedMark) : new SqlParameter("@P_SEM_III_OBTMARK", DBNull.Value);
                objParams[118] = (objSR.SemIIIMaxMark > 0) ? new SqlParameter("@P_SEM_III_MAXMARK", objSR.SemIIIMaxMark) : new SqlParameter("@P_SEM_III_MAXMARK", DBNull.Value);
                objParams[119] = (objSR.SemIIIPer > 0) ? new SqlParameter("@P_SEM_III_PER", objSR.SemIIIPer) : new SqlParameter("@P_SEM_III_PER", DBNull.Value);
                objParams[120] = (objSR.SemIVObtainedMark > 0) ? new SqlParameter("@P_SEM_IV_OBTMARK", objSR.SemIVObtainedMark) : new SqlParameter("@P_SEM_IV_OBTMARK", DBNull.Value);
                objParams[121] = (objSR.SemIVMaxMark > 0) ? new SqlParameter("@P_SEM_IV_MAXMARK", objSR.SemIVMaxMark) : new SqlParameter("@P_SEM_IV_MAXMARK", DBNull.Value);
                objParams[122] = (objSR.SemIVPer > 0) ? new SqlParameter("@P_SEM_IV_PER", objSR.SemIVPer) : new SqlParameter("@P_SEM_IV_PER", DBNull.Value);
                objParams[123] = (objSR.SemVObtainedMark > 0) ? new SqlParameter("@P_SEM_V_OBTMARK", objSR.SemVObtainedMark) : new SqlParameter("@P_SEM_V_OBTMARK", DBNull.Value);
                objParams[124] = (objSR.SemVMaxMark > 0) ? new SqlParameter("@P_SEM_V_MAXMARK", objSR.SemVMaxMark) : new SqlParameter("@P_SEM_V_MAXMARK", DBNull.Value);
                objParams[125] = (objSR.SemVPer > 0) ? new SqlParameter("@P_SEM_V_PER", objSR.SemVPer) : new SqlParameter("@P_SEM_V_PER", DBNull.Value);
                objParams[126] = (objSR.SemVIObtainedMark > 0) ? new SqlParameter("@P_SEM_VI_OBTMARK", objSR.SemVIObtainedMark) : new SqlParameter("@P_SEM_VI_OBTMARK", DBNull.Value);
                objParams[127] = (objSR.SemVIMaxMark > 0) ? new SqlParameter("@P_SEM_VI_MAXMARK", objSR.SemVIMaxMark) : new SqlParameter("@P_SEM_VI_MAXMARK", DBNull.Value);
                objParams[128] = (objSR.SemVIPer > 0) ? new SqlParameter("@P_SEM_VI_PER", objSR.SemVIPer) : new SqlParameter("@P_SEM_VI_PER", DBNull.Value);
                objParams[129] = (objSR.SemVIIObtainedMark > 0) ? new SqlParameter("@P_SEM_VII_OBTMARK", objSR.SemVIIObtainedMark) : new SqlParameter("@P_SEM_VII_OBTMARK", DBNull.Value);
                objParams[130] = (objSR.SemVIIMaxMark > 0) ? new SqlParameter("@P_SEM_VII_MAXMARK", objSR.SemVIIMaxMark) : new SqlParameter("@P_SEM_VII_MAXMARK", DBNull.Value);
                objParams[131] = (objSR.SemVIIPer > 0) ? new SqlParameter("@P_SEM_VII_PER", objSR.SemVIIPer) : new SqlParameter("@P_SEM_VII_PER", DBNull.Value);
                objParams[132] = new SqlParameter("@P_DIP_DEGREE", objSR.DipDegree);
                objParams[133] = new SqlParameter("@P_SPECIALIZATION", objSR.Specialization);
                objParams[134] = (objSR.TotalMarkScoredDip > 0) ? new SqlParameter("@P_TOTALMARK_SCORED_DIP", objSR.TotalMarkScoredDip) : new SqlParameter("@P_TOTALMARK_SCORED_DIP", DBNull.Value);
                objParams[135] = (objSR.TotalofMaxMarkDip > 0) ? new SqlParameter("@P_TOTALMARK_MAX_DIP", objSR.TotalofMaxMarkDip) : new SqlParameter("@P_TOTALMARK_MAX_DIP", DBNull.Value);
                objParams[136] = (objSR.TotalPercentageDip > 0) ? new SqlParameter("@P_TOTAL_PERCENTAGE_DIP", objSR.TotalPercentageDip) : new SqlParameter("@P_TOTAL_PERCENTAGE_DIP", DBNull.Value);
                objParams[137] = (objSR.SSCTotalofMaxMark > 0) ? new SqlParameter("@P_SSC_TOTAL_MAX_MARK", objSR.SSCTotalofMaxMark) : new SqlParameter("@P_SSC_TOTAL_MAX_MARK", DBNull.Value);
                objParams[138] = (objSR.HSCTotalofMaxMark > 0) ? new SqlParameter("@P_HSC_TOTAL_MAX_MARK", objSR.HSCTotalofMaxMark) : new SqlParameter("@P_HSC_TOTAL_MAX_MARK", DBNull.Value);
                objParams[139] = (objSR.SSCTotalGradePoint > 0) ? new SqlParameter("@P_SSC_TOTAL_GRADEPOINT", objSR.SSCTotalGradePoint) : new SqlParameter("@P_SSC_TOTAL_GRADEPOINT", DBNull.Value);
                objParams[140] = (objSR.SSCCGPA > 0) ? new SqlParameter("@P_SSC_CGPA", objSR.SSCCGPA) : new SqlParameter("@P_SSC_CGPA", DBNull.Value);
                objParams[141] = (objSR.HSCTotalGradePoint > 0) ? new SqlParameter("@P_HSC_TOTAL_GRADEPOINT", objSR.HSCTotalGradePoint) : new SqlParameter("@P_HSC_TOTAL_GRADEPOINT", DBNull.Value);
                objParams[142] = (objSR.HSCCGPA > 0) ? new SqlParameter("@P_HSC_CGPA", objSR.HSCCGPA) : new SqlParameter("@P_HSC_CGPA", DBNull.Value);
                objParams[143] = new SqlParameter("@P_SSC_OTHER_BOARD", objSR.SSCotherBoard);
                objParams[144] = new SqlParameter("@P_HSC_OTHER_BOARD", objSR.HSCotherBoard);
                objParams[145] = new SqlParameter("@P_SSC_INPUT_SYSTEM", objSR.SSCInputSystem);
                objParams[146] = new SqlParameter("@P_HSC_INPUT_SYSTEM", objSR.HSCInputSystem);
                objParams[147] = new SqlParameter("@P_SSC_TRANSFER_CERT_NO", objSR.SSCTransferCertNo);
                objParams[148] = new SqlParameter("@P_HSC_TRANSFER_CERT_NO", objSR.HSCTransferCertNo);
                objParams[149] = new SqlParameter("@P_DIP_TRANSFER_CERT_NO", objSR.DIPTransferCertNo);

                objParams[150] = new SqlParameter("@P_SSCLANGUAGE2", objSR.SSCLanguage2);
                objParams[151] = new SqlParameter("@P_SSCLANG2_PER", objSR.SSCLanguage2Per);
                objParams[152] = (objSR.SSCLanguage2ObtMark > 0) ? new SqlParameter("@P_SSCLANG2_OBTMARK", objSR.SSCLanguage2ObtMark) : new SqlParameter("@P_SSCLANG2_OBTMARK", DBNull.Value);
                objParams[153] = (objSR.SSCLanguage2MaxMark > 0) ? new SqlParameter("@P_SSCLANG2_MAXMARK", objSR.SSCLanguage2MaxMark) : new SqlParameter("@P_SSCLANG2_MAXMARK", DBNull.Value);
                objParams[154] = new SqlParameter("@P_SSC_LANGUAGE2_GDPOINT", objSR.SSCLanguage2GdPoint);
                objParams[155] = new SqlParameter("@P_SSC_LANGUAGE2_GRADE", objSR.SSCLanguage2Grade);

                objParams[156] = new SqlParameter("@P_BOTANY_OBT_MARK", objSR.BotanyObtMark);
                objParams[157] = new SqlParameter("@P_BOTANY_MAX_MARK", objSR.BotanyMaxMark);
                objParams[158] = new SqlParameter("@P_BOTANY_PER", objSR.BotanyPer);
                objParams[159] = new SqlParameter("@P_BOTANY_GDPOINT", objSR.BotanyGdPoint);
                objParams[160] = new SqlParameter("@P_BOTANY_GRADE", objSR.BotanyGrade);
                objParams[161] = new SqlParameter("@P_ZOOLOGY_OBT_MARK", objSR.ZooObtMark);
                objParams[162] = new SqlParameter("@P_ZOOLOGY_MAX_MARK", objSR.ZooMaxMark);
                objParams[163] = new SqlParameter("@P_ZOOLOGY_PER", objSR.ZooPer);
                objParams[164] = new SqlParameter("@P_ZOOLOGY_GDPOINT", objSR.ZooGdPoint);
                objParams[165] = new SqlParameter("@P_ZOOLOGY_GRADE", objSR.ZooGrade);

                objParams[166] = new SqlParameter("@P_CS_OBT_MARK", objSR.CSObtMark);
                objParams[167] = new SqlParameter("@P_CS_MAX_MARK", objSR.CSMaxMark);
                objParams[168] = new SqlParameter("@P_CS_PER", objSR.CSPer);
                objParams[169] = new SqlParameter("@P_CS_GDPOINT", objSR.CSGdPoint);
                objParams[170] = new SqlParameter("@P_CS_GRADE", objSR.CSGrade);

                objParams[171] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[171].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_SP_STUDENT_LASTEXAM_DETAILS_UPDATE_STUDINFO", objParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Academic.AddUpdateStudentLastExamDetail-> " + ex.ToString());
            }

            return retStatus;
        }

        public int UpdateDocumentVerification(int userno, int doc)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_constr);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[3];
                objParams[0] = new SqlParameter("@P_USERNO", userno);
                objParams[1] = new SqlParameter("@P_DOCNO", doc);
                //  objParams[2] = new SqlParameter("@P_DOCNO", doc);

                objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[2].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_UPDATE_DOC_VERIFICATION", objParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Academic.AddUpdateOnlineStudentReg-> " + ex.ToString());
            }

            return retStatus;
        }

        public int AddStudentDetails(int userno, string collegeid, string degreeno, string branchno, string remark, int status, int boardstudied, int uano, string ipaddress, DateTime transdate)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_constr);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[11];
                objParams[0] = new SqlParameter("@P_USERNO", userno);
                objParams[1] = new SqlParameter("@P_COLLEGE_ID", collegeid);
                objParams[2] = new SqlParameter("@P_DEGREE_NO", degreeno);
                objParams[3] = new SqlParameter("@P_BRANCH_NO", branchno);
                objParams[4] = new SqlParameter("@P_REMARK", remark);
                objParams[5] = new SqlParameter("@P_STATUS", status);
                objParams[6] = new SqlParameter("@P_BOARD_STUDIED", boardstudied);
                objParams[7] = new SqlParameter("@P_UANO", uano);
                objParams[8] = new SqlParameter("@P_IPADDRESS", ipaddress);
                objParams[9] = new SqlParameter("@P_TRANSDATE", transdate);
                objParams[10] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[10].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_TRANSFER_STUD_DATA_NEW_ByDirectAdmission", objParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Academic.AddUpdateOnlineStudentReg-> " + ex.ToString());
            }

            return retStatus;
        }
    }
}