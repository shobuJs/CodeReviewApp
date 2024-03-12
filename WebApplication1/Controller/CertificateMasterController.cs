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
            /// <summary>
            /// This CertificateMasterController is used to control certificate detail.
            /// </summary>
            public class CertificateMasterController
            {
                /// <summary>
                /// ConnectionString
                /// </summary>
                private string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                public DataSet GetStudentListForBC(int admbatchNo, int sessionNo, int collegeNo, int degreeNo, int branchNo, int semesterNo)
                {
                    DataSet ds = null;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_ADMBATCHNO", admbatchNo);
                        objParams[1] = new SqlParameter("@P_SESSIONNO", sessionNo);
                        objParams[2] = new SqlParameter("@P_COLLEGE_ID", collegeNo);
                        objParams[3] = new SqlParameter("@P_DEGREENO", degreeNo);
                        objParams[4] = new SqlParameter("@P_BRANCHNO", branchNo);
                        objParams[5] = new SqlParameter("@P_SEMESTERNO", semesterNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STUDENT_LIST_FOR_BC", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CertificateMasterController.GetStudentListForBC-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetStudentListForIssueCertBona(int idNo)
                {
                    DataSet ds = null;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STUDENT_LIST_FOR_ISSUE_CERT_BONAFIDE", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CertificateMasterController.GetStudentListForIssueCert-> " + ex.ToString());
                    }
                    return ds;
                }

                public int AddBonafideCertificate(CertificateMaster objcertMaster, decimal tuitionFee, decimal examFee, decimal otherFee, decimal hostelFee)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[16];
                        objParams[0] = new SqlParameter("@P_CERT_NO", objcertMaster.CertNo);
                        objParams[1] = new SqlParameter("@P_IDNO", objcertMaster.IdNo);
                        objParams[2] = new SqlParameter("@P_ATTENDANCE", objcertMaster.Attendance);
                        objParams[3] = new SqlParameter("@P_CONDUCT", objcertMaster.Conduct);

                        if (objcertMaster.LeavingDate == DateTime.MinValue)
                            objParams[4] = new SqlParameter("@P_LEAVING_DATE", DBNull.Value);
                        else
                            objParams[4] = new SqlParameter("@P_LEAVING_DATE", objcertMaster.LeavingDate);

                        objParams[5] = new SqlParameter("@P_IP_ADDRESS", objcertMaster.IpAddress);
                        objParams[6] = new SqlParameter("@P_UA_NO", objcertMaster.UaNO);
                        objParams[7] = new SqlParameter("@P_ISSUE_STATUS", objcertMaster.IssueStatus);
                        objParams[8] = new SqlParameter("@P_COLLEGE_CODE", objcertMaster.CollegeCode);
                        //objParams[9] = new SqlParameter("@P_SESSIONNO", objcertMaster.SessionNo);
                        //objParams[10] = new SqlParameter("@P_SEMESTERNO", objcertMaster.SemesterNo);
                        objParams[9] = new SqlParameter("@P_REASON", objcertMaster.Reason);
                        objParams[10] = new SqlParameter("@P_REMARK", objcertMaster.Remark);
                        objParams[11] = new SqlParameter("@P_TUITION_FEE", tuitionFee);
                        objParams[12] = new SqlParameter("@P_EXAM_FEE", examFee);
                        objParams[13] = new SqlParameter("@P_OTHER_FEE", otherFee);
                        objParams[14] = new SqlParameter("@P_HOSTEL_FEE", hostelFee);

                        objParams[15] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[15].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_SP_CERTIFICATE_INSERT", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CertificateMasterController.AddBonafideCertificate-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetStudentListForBC_BYIDNO(int idNo)
                {
                    DataSet ds = null;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STUDENT_LIST_FOR_BC_BYIDNO", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CertificateMasterController.GetStudentListForBC-> " + ex.ToString());
                    }
                    return ds;
                }

                //when certificate is issue
                public DataSet GetStudentListForIssueCert(int idNo)
                {
                    DataSet ds = null;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STUDENT_LIST_FOR_ISSUE_CERT", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CertificateMasterController.GetStudentListForIssueCert-> " + ex.ToString());
                    }
                    return ds;
                }

                /// <summary>
                /// This method is used to delete record from news table.
                /// </summary>
                /// <param name="newsid">Delete record as per this newsid.</param>
                /// <returns>Integer CustomStatus - Record Deleted or Error</returns>
                public int CanelCertificate(int CERT_TR_NO)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_CERT_TR_NO", CERT_TR_NO);

                        objSQLHelper.ExecuteNonQuerySP("PKG_SP_DEL_ISSUE_CERTIFICATE", objParams, false);
                        retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.certificatemastercontroller.CanelCertificate-> " + ex.ToString());
                    }

                    return Convert.ToInt32(retStatus);
                }

                public DataSet GetStudentListForTC(int branchNo, int semesterNo, int admbatchNo)
                {
                    DataSet ds = null;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_BRANCHNO", branchNo);
                        objParams[1] = new SqlParameter("@P_SEMESTERNO", semesterNo);
                        objParams[2] = new SqlParameter("@P_ADMBATCHNO", admbatchNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STUDENT_LIST_FOR_TC", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CertificateMasterController.GetStudentListForBC-> " + ex.ToString());
                    }
                    return ds;
                }

                public int UpdateIssueStatusCertificate(int idno)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);

                        objSQLHelper.ExecuteNonQuerySP("PKG_SP_UPD_ISSUE_CERTIFICATE", objParams, false);
                        retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.certificatemastercontroller.UpdateIssueStatusCertificate-> " + ex.ToString());
                    }

                    return Convert.ToInt32(retStatus);
                }

                public DataSet GetStudentListForRegistrationCard(int admbatchNo, int collegeNo, int degreeNo, int branchNo)
                {
                    DataSet ds = null;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_ADMBATCHNO", admbatchNo);
                        objParams[1] = new SqlParameter("@P_COLLEGE_ID", collegeNo);
                        objParams[2] = new SqlParameter("@P_DEGREENO", degreeNo);
                        objParams[3] = new SqlParameter("@P_BRANCHNO", branchNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_SHOW_STUDENTS_FOR_REGISTRATION_CARD ", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CertificateMasterController.GetStudentListForBC-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetStudentListForMigrationCard(int admbatchNo, int collegeNo, int degreeNo, int branchNo)
                {
                    DataSet ds = null;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_ADMBATCHNO", admbatchNo);
                        objParams[1] = new SqlParameter("@P_COLLEGE_ID", collegeNo);
                        objParams[2] = new SqlParameter("@P_DEGREENO", degreeNo);
                        objParams[3] = new SqlParameter("@P_BRANCHNO", branchNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_SHOW_STUDENTS_FOR_MIGRATION_CARD ", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CertificateMasterController.GetStudentListForBC-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetStudentDetailsByRegistrationNo(int idNo)
                {
                    DataSet ds = null;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STUDENT_LIST_BY_IDNO", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CertificateMasterController.GetStudentListForBC-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet getstudentinfo(int admbtach, int regstatus)
                {
                    DataSet ds = null;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_ADMBATCHNO", admbtach);
                        objParams[1] = new SqlParameter("@P_REGSTATUS", regstatus);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_YEARWISE_REGISTRATION", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CertificateMasterController.GetStudentListForIssueCert-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GET_STUDENT_TC_Conduct(int admbatch, int degreeno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objsqlHlper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_ADMBATCHNO", admbatch);
                        //objParams[1] = new SqlParameter("@P_SEMESTERNO", sessionno);
                        objParams[1] = new SqlParameter("@P_DEGREENO", degreeno);
                        ds = objsqlHlper.ExecuteDataSetSP("PKG_STUDENT_LIST_FOR_TC_CONDUCT", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CertificateMasterController.GET_TC_EXCEL-> " + ex.ToString());
                    }
                    return ds;
                }

                public int AddTransferCertificate(CertificateMaster objcertMaster, decimal tuitionFee, decimal examFee, decimal otherFee, decimal hostelFee, int branch, int admbatch)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[18];
                        objParams[0] = new SqlParameter("@P_CERT_NO", objcertMaster.CertNo);
                        objParams[1] = new SqlParameter("@P_IDNO", objcertMaster.IdNo);
                        objParams[2] = new SqlParameter("@P_ATTENDANCE", objcertMaster.Attendance);
                        objParams[3] = new SqlParameter("@P_CONDUCT", objcertMaster.Conduct);

                        if (objcertMaster.LeavingDate == DateTime.MinValue)
                            objParams[4] = new SqlParameter("@P_LEAVING_DATE", DBNull.Value);
                        else
                            objParams[4] = new SqlParameter("@P_LEAVING_DATE", objcertMaster.LeavingDate);

                        objParams[5] = new SqlParameter("@P_IP_ADDRESS", objcertMaster.IpAddress);
                        objParams[6] = new SqlParameter("@P_UA_NO", objcertMaster.UaNO);
                        objParams[7] = new SqlParameter("@P_ISSUE_STATUS", objcertMaster.IssueStatus);
                        objParams[8] = new SqlParameter("@P_COLLEGE_CODE", objcertMaster.CollegeCode);
                        //objParams[9] = new SqlParameter("@P_SESSIONNO", objcertMaster.SessionNo);
                        //objParams[10] = new SqlParameter("@P_SEMESTERNO", objcertMaster.SemesterNo);
                        objParams[9] = new SqlParameter("@P_REASON", objcertMaster.Reason);
                        objParams[10] = new SqlParameter("@P_REMARK", objcertMaster.Remark);
                        objParams[11] = new SqlParameter("@P_TUITION_FEE", tuitionFee);
                        objParams[12] = new SqlParameter("@P_EXAM_FEE", examFee);
                        objParams[13] = new SqlParameter("@P_OTHER_FEE", otherFee);
                        objParams[14] = new SqlParameter("@P_HOSTEL_FEE", hostelFee);
                        objParams[15] = new SqlParameter("@P_BRANCHNO", branch);
                        objParams[16] = new SqlParameter("@P_ADMBATCH", admbatch);

                        objParams[17] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[17].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_SP_CERTIFICATE_INSERT_SVCE", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CertificateMasterController.AddTransferCertificate-> " + ex.ToString());
                    }
                    return retStatus;
                }

                //  Modified and Added by Naresh Beerla 25/04/2020

                public int AddBonafideCertificate(CertificateMaster objcertMaster, decimal tuitionFee, decimal examFee, decimal otherFee, decimal hostelFee, int branch, int admbatch)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[18];
                        objParams[0] = new SqlParameter("@P_CERT_NO", objcertMaster.CertNo);
                        objParams[1] = new SqlParameter("@P_IDNO", objcertMaster.IdNo);
                        objParams[2] = new SqlParameter("@P_ATTENDANCE", objcertMaster.Attendance);
                        objParams[3] = new SqlParameter("@P_CONDUCT", objcertMaster.Conduct);

                        if (objcertMaster.LeavingDate == DateTime.MinValue)
                            objParams[4] = new SqlParameter("@P_LEAVING_DATE", DBNull.Value);
                        else
                            objParams[4] = new SqlParameter("@P_LEAVING_DATE", objcertMaster.LeavingDate);

                        objParams[5] = new SqlParameter("@P_IP_ADDRESS", objcertMaster.IpAddress);
                        objParams[6] = new SqlParameter("@P_UA_NO", objcertMaster.UaNO);
                        objParams[7] = new SqlParameter("@P_ISSUE_STATUS", objcertMaster.IssueStatus);
                        objParams[8] = new SqlParameter("@P_COLLEGE_CODE", objcertMaster.CollegeCode);
                        //objParams[9] = new SqlParameter("@P_SESSIONNO", objcertMaster.SessionNo);
                        //objParams[10] = new SqlParameter("@P_SEMESTERNO", objcertMaster.SemesterNo);
                        objParams[9] = new SqlParameter("@P_REASON", objcertMaster.Reason);
                        objParams[10] = new SqlParameter("@P_REMARK", objcertMaster.Remark);
                        objParams[11] = new SqlParameter("@P_TUITION_FEE", tuitionFee);
                        objParams[12] = new SqlParameter("@P_EXAM_FEE", examFee);
                        objParams[13] = new SqlParameter("@P_OTHER_FEE", otherFee);
                        objParams[14] = new SqlParameter("@P_HOSTEL_FEE", hostelFee);
                        objParams[15] = new SqlParameter("@P_BRANCHNO", branch);
                        objParams[16] = new SqlParameter("@P_ADMBATCH", admbatch);

                        objParams[17] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[17].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_SP_CERTIFICATE_INSERT_SVCE", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CertificateMasterController.AddBonafideCertificate-> " + ex.ToString());
                    }
                    return retStatus;
                }

                // Ends Here by Naresh beerla 25/04/2020

                //Modified and Added by Naresh Beerla on 25/04/2020

                public DataSet GetStudentListForBC(int admbatchNo, int sessionNo, int collegeNo, int degreeNo, int branchNo, int semesterNo, int cert_no)
                {
                    DataSet ds = null;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[7];
                        objParams[0] = new SqlParameter("@P_ADMBATCHNO", admbatchNo);
                        objParams[1] = new SqlParameter("@P_SESSIONNO", sessionNo);
                        objParams[2] = new SqlParameter("@P_COLLEGE_ID", collegeNo);
                        objParams[3] = new SqlParameter("@P_DEGREENO", degreeNo);
                        objParams[4] = new SqlParameter("@P_BRANCHNO", branchNo);
                        objParams[5] = new SqlParameter("@P_SEMESTERNO", semesterNo);
                        objParams[6] = new SqlParameter("@P_CERT_NO", cert_no);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STUDENT_LIST_FOR_BC", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CertificateMasterController.GetStudentListForBC-> " + ex.ToString());
                    }
                    return ds;
                }

                //Added By Naresh Beerla for Single Transfer Certificate on 29/04/2020
                public DataSet GetStudentListForTC_BYIDNO(int idNo)
                {
                    DataSet ds = null;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STUDENT_LIST_FOR_TC_BYIDNO", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CertificateMasterController.GetStudentListForTC-> " + ex.ToString());
                    }
                    return ds;
                }

                //Ends Here By Naresh Beerla for Single Transfer Certificate on 29/04/2020

                //MODIDFIED AND ADDED CERT_NO PARAMETER By Naresh Beerla for Single Transfer Certificate on 29/04/2020
                public DataSet GetStudentListForBC_BYIDNO(int idNo, int Cert_no)
                {
                    DataSet ds = null;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_IDNO", idNo);
                        objParams[1] = new SqlParameter("@P_CERT_NO", Cert_no);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STUDENT_LIST_FOR_BC_BYIDNO", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CertificateMasterController.GetStudentListForBC-> " + ex.ToString());
                    }
                    return ds;
                }

                //Ends Here MODIDFIED AND ADDED CERT_NO PARAMETER By Naresh Beerla for Single Transfer Certificate on 29/04/2020

                //Ends here by Naresh Beerla on 25/04/2020
                // ADDED BY NARESH BEERLA FOR GETTING THE DATA OF ADMISSION CANCEL STUDENTS ON 21072020

                public DataSet GetStudentListForAdmcanTC(int branchNo, int semesterNo, int admbatchNo)
                {
                    DataSet ds = null;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_BRANCHNO", branchNo);
                        objParams[1] = new SqlParameter("@P_SEMESTERNO", semesterNo);
                        objParams[2] = new SqlParameter("@P_ADMBATCHNO", admbatchNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STUDENT_LIST_FOR_ADMCAN_TC", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CertificateMasterController.GetStudentListForAdmcanTC-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetStudentBasicDetails(int Idno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", Idno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_BASIC_STUD_DETAILS_FOR_CERT_APPLICATION", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CertificateMasterController.GetStudentBasicDetails() -->" + ex.ToString());
                    }
                    return ds;
                }

                public int AddMulCertificateDetailsofStudent(CertificateMaster objcertMaster)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[12];
                        objParams[0] = new SqlParameter("@P_IDNO", objcertMaster.IdNo);
                        objParams[1] = new SqlParameter("@P_ROLLNO", objcertMaster.Rollno);
                        objParams[2] = new SqlParameter("@P_DEGREENO", objcertMaster.Degreeno);
                        objParams[3] = new SqlParameter("@P_BRANCHNO", objcertMaster.Branchno);
                        objParams[4] = new SqlParameter("@P_ADMBATCH", objcertMaster.Admbatch);
                        objParams[5] = new SqlParameter("@P_CERT_NOS", objcertMaster.Certificate_No);
                        objParams[6] = new SqlParameter("@P_TOTAL_AMT", objcertMaster.Cert_Amt);
                        objParams[7] = new SqlParameter("@P_COLLEGE_ID", objcertMaster.CollegeCode);
                        objParams[8] = new SqlParameter("@P_APP_TYPE", objcertMaster.App_Type);
                        objParams[9] = new SqlParameter("@P_SESSIONNO", objcertMaster.SessionNo);
                        objParams[10] = new SqlParameter("@P_APP_ID", objcertMaster.App_ID);
                        objParams[11] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[11].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INSERT_MUL_CERTIFICATE_APPLICATION_DETAILS", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CertificateMasterController.AddMulCertificateDetailsofStudent-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet Certificatelist(int idno, int App_Type, int App_id)
                {
                    DataSet dsPD = null;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        objParams[1] = new SqlParameter("@P_APP_TYPE", App_Type);
                        objParams[2] = new SqlParameter("@P_APP_ID", App_id);

                        dsPD = objSQLHelper.ExecuteDataSetSP("PKG_STUDENT_GET_CERTIFICATE_LIST", objParams);
                    }
                    catch (Exception ex)
                    {
                        return dsPD;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CertificateMasterController.Certificatelist()-> " + ex.ToString());
                    }

                    return dsPD;
                }

                public DataSet BindStudentCertDetails(int App_Type, int IDNO)
                {
                    DataSet ds = null;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_APP_TYPE", App_Type);
                        objParams[1] = new SqlParameter("@P_IDNO", IDNO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_STUDENT_CERT_DETAILS", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CertificateMasterController.BindStudentCertDetails()-> " + ex.ToString());
                    }

                    return ds;
                }

                //Admin Approval

                public DataSet GetMulApplicationStuDatainBulk(int App_Type, int Deptno)
                {
                    DataSet ds = null;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_APP_TYPE", App_Type);
                        objParams[1] = new SqlParameter("@P_DEPTNO ", Deptno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_CERT_DETAILS_DEPTWISE", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CertificateMasterController.GetMulApplicationStuDatainBulk()-> " + ex.ToString());
                    }

                    return ds;
                }

                public DataSet GetMulApplicationStuDataofAdminApproval(int idno, int App_Type, int App_id)
                {
                    DataSet ds = null;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        objParams[1] = new SqlParameter("@P_APP_TYPE", App_Type);
                        objParams[2] = new SqlParameter("@P_APP_ID", App_id);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_STUDENT_CERT_APP_DATA", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CertificateMasterController.GetMulApplicationStuDataofAdminApproval()-> " + ex.ToString());
                    }

                    return ds;
                }

                public DataSet CertificatelistofAdminApproval(int idno, int App_Type, int App_id)
                {
                    DataSet ds = null;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        objParams[1] = new SqlParameter("@P_APP_TYPE", App_Type);
                        objParams[2] = new SqlParameter("@P_APP_ID", App_id);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_STUDENT_CERT_LIST", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CertificateMasterController.CertificatelistofAdminApproval()-> " + ex.ToString());
                    }

                    return ds;
                }

                public int UpdateAdminApprovalonCertAplication(CertificateMaster objcertMaster)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[8];
                        objParams[0] = new SqlParameter("@P_IDNO", objcertMaster.IdNo);
                        objParams[1] = new SqlParameter("@P_CERT_NOS", objcertMaster.Certificate_No);
                        objParams[2] = new SqlParameter("@P_TOTAL_AMT", objcertMaster.Cert_Amt);
                        objParams[3] = new SqlParameter("@P_UANO", objcertMaster.UaNO);
                        objParams[4] = new SqlParameter("@P_IP_ADDRESS", objcertMaster.IpAddress);
                        objParams[5] = new SqlParameter("@P_APP_TYPE", objcertMaster.App_Type);
                        objParams[6] = new SqlParameter("@P_APP_ID", objcertMaster.App_ID);
                        objParams[7] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[7].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_UPDATE_ADMIN_APPROVAL_OF_STUD_CERT_APPLICATION", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CertificateMasterController.UpdateAdminApprovalonCertAplication-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetStudentDocDetailsafterapproval(int idno, int App_Type, int App_id)
                {
                    DataSet ds = null;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        objParams[1] = new SqlParameter("@P_APP_TYPE", App_Type);
                        objParams[2] = new SqlParameter("@P_APP_ID", App_id);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_STUDENT_CERT_LIST_REPORT", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CertificateMasterController.GetStudentDocDetailsafterapproval()-> " + ex.ToString());
                    }

                    return ds;
                }

                //End Sneha Doble
            }
        }
    }
}