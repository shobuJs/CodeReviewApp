//======================================================================================
// PROJECT NAME  : UAIMS
// MODULE NAME   : BUSINESS LOGIC FILE [SESSIONCONTROLLER]
// CREATION DATE : 18-MAY-2009
// CREATED BY    : SANJAY RATNAPARKHI
// MODIFIED DATE :
// MODIFIED DESC :
//======================================================================================

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
            public class SessionController
            {
                private string _UAIMS_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                public int AddSession(IITMS.UAIMS.BusinessLayer.BusinessEntities.Session objSession, int AcademicNo, int Academic_Year)
                {
                    //int retStatus = Convert.ToInt32(CustomStatus.Others);
                    int status = 0;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        //Add
                        objParams = new SqlParameter[12];
                        objParams[0] = new SqlParameter("@P_SESSION_PNAME", objSession.Session_PName);
                        objParams[1] = new SqlParameter("@P_SESSION_STDATE", objSession.Session_SDate);
                        objParams[2] = new SqlParameter("@P_SESSION_ENDDATE", objSession.Session_EDate);
                        objParams[3] = new SqlParameter("@P_SESSION_NAME", objSession.Session_Name);
                        objParams[4] = new SqlParameter("@P_ODD_EVEN", objSession.Odd_Even);
                        objParams[5] = new SqlParameter("@P_SESSNAME_HINDI", objSession.Sessname_hindi);
                        objParams[6] = new SqlParameter("@P_COLLEGE_CODE", objSession.College_code);
                        objParams[7] = new SqlParameter("@P_FLOCK", objSession.Flock);
                        objParams[8] = new SqlParameter("@P_EXAMTYPE", objSession.ExamType);
                        // objParams[9] = new SqlParameter("@P_COLLEGE_ID", objSession.College_ID);
                        objParams[9] = new SqlParameter("@P_ACADEMIC", AcademicNo);
                        objParams[10] = new SqlParameter("@P_ACADEMIC_YEAR", Academic_Year);
                        objParams[11] = new SqlParameter("@P_SESSIONNO", SqlDbType.Int);
                        objParams[11].Direction = ParameterDirection.Output;

                        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_SESSION_SP_INS_SESSIONMASTER", objParams, true);

                        if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001" && obj.ToString() != "0")
                            status = Convert.ToInt32(CustomStatus.RecordSaved);
                        else
                            status = Convert.ToInt32(CustomStatus.Error);
                    }
                    catch (Exception ex)
                    {
                        status = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.SessionController.AddSession-> " + ex.ToString());
                    }
                    return status;
                }

                public int UpdateSession(IITMS.UAIMS.BusinessLayer.BusinessEntities.Session objSession, int AcademicNo, int Academic_Year)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        //update
                        objParams = new SqlParameter[11];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", objSession.SessionNo);
                        objParams[1] = new SqlParameter("@P_SESSION_PNAME", objSession.Session_PName);
                        objParams[2] = new SqlParameter("@P_SESSION_STDATE", objSession.Session_SDate);
                        objParams[3] = new SqlParameter("@P_SESSION_ENDDATE", objSession.Session_EDate);
                        objParams[4] = new SqlParameter("@P_SESSION_NAME", objSession.Session_Name);
                        objParams[5] = new SqlParameter("@P_ODD_EVEN", objSession.Odd_Even);
                        objParams[6] = new SqlParameter("@P_SESSNAME_HINDI", objSession.Sessname_hindi);
                        objParams[7] = new SqlParameter("@P_EXAMTYPE", objSession.ExamType);
                        //   objParams[8] = new SqlParameter("@P_COLLEGE_ID", objSession.College_ID);
                        objParams[8] = new SqlParameter("@P_FLOCK", objSession.Flock);
                        objParams[9] = new SqlParameter("@P_ACADEMIC_YEAR", Academic_Year);
                        objParams[10] = new SqlParameter("@P_ACADEMIC", AcademicNo);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_SESSION_SP_UPD_SESSIONMASTER", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.SessionController.UpdateCT-> " + ex.ToString());
                    }

                    return retStatus;
                }

                public DataSet GetCurrentSession()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_ALL_BRANCH", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.SessionController.GetCurrentSession-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetAllSession()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_SESSION_SP_ALL_SESSIONMASTER", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.SessionController.GetAllSession-> " + ex.ToString());
                    }
                    return ds;
                }

                public SqlDataReader GetSingleSession(int sessionno)
                {
                    SqlDataReader dr = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("P_SESSIONNO", sessionno);
                        dr = objSQLHelper.ExecuteReaderSP("PKG_SESSION_SP_RET_SESSIONMASTER", objParams);
                    }
                    catch (Exception ex)
                    {
                        return dr;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.SessionController.GetSingleSession-> " + ex.ToString());
                    }
                    return dr;
                }

                public DataSet Getstudentsforeligibility(IITMS.UAIMS.BusinessLayer.BusinessEntities.Session objSession, int ua_no)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_ADMBATCH", objSession.Admbatch);
                        objParams[1] = new SqlParameter("@P_COLLEGEID", objSession.College_ID);
                        objParams[2] = new SqlParameter("@P_DEGREENO", objSession.Degree_No);
                        objParams[3] = new SqlParameter("@P_BRANCHNO", objSession.Branch_No);
                        objParams[4] = new SqlParameter("@P_SEMESTERNO", objSession.Semester_No);
                        objParams[5] = new SqlParameter("@P_UANO", ua_no);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_AMOUNT_SP_ALL_SCHOLARSHIP_ELIGIBILITY", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.SessionController.Getstudentsforeligibility-> " + ex.ToString());
                    }
                    return ds;
                }

                public int AddEligibleStudents(IITMS.UAIMS.BusinessLayer.BusinessEntities.Session objSession)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        //Add
                        objParams = new SqlParameter[9];
                        objParams[0] = new SqlParameter("@P_ADMBATCH", objSession.Admbatch);
                        objParams[1] = new SqlParameter("@P_IDNO", objSession.StudId);
                        objParams[2] = new SqlParameter("@P_COLLEGEID", objSession.College_ID);
                        objParams[3] = new SqlParameter("@P_DEGREENO", objSession.Degree_No);
                        objParams[4] = new SqlParameter("@P_BRANCHNO", objSession.Branch_No);
                        objParams[5] = new SqlParameter("@P_SEMESTERNO", objSession.Semester_No);
                        objParams[6] = new SqlParameter("@P_COLLEGE_CODE", objSession.College_code);
                        objParams[7] = new SqlParameter("@P_STATUS", objSession.Status);
                        objParams[8] = new SqlParameter("@P_CERTNO", SqlDbType.Int);
                        objParams[8].Direction = ParameterDirection.Output;

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_SCHO_ELIGIBLESTUDENTS_SP_INS", objParams, true) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.SessionController.AddSession-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int AddAmount(Session objSession)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        //Add
                        objParams = new SqlParameter[9];
                        objParams[0] = new SqlParameter("@P_ADMBATCH", objSession.Admbatch);
                        objParams[1] = new SqlParameter("@P_COLLEGE_ID", objSession.College_ID);
                        objParams[2] = new SqlParameter("@P_IDNO", objSession.StudId);
                        objParams[3] = new SqlParameter("@P_DEGREENO", objSession.Degree_No);
                        objParams[4] = new SqlParameter("@P_BRANCHNO", objSession.Branch_No);
                        objParams[5] = new SqlParameter("@P_SEMESTERNO", objSession.Semester_No);
                        objParams[6] = new SqlParameter("@P_AMOUNT", objSession.TotAmount);
                        objParams[7] = new SqlParameter("@P_COLLEGE_CODE", objSession.College_code);
                        objParams[8] = new SqlParameter("@P_CERTNO", SqlDbType.Int);
                        objParams[8].Direction = ParameterDirection.Output;

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_SCHO_AMOUNT_SP_INS", objParams, true) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.SessionController.AddSession-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int LockScholarship(Scholarship objScholarship)
                {
                    int status = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] sqlParams = new SqlParameter[]
                        {
                            new SqlParameter("@P_SESSIONNO",objScholarship.SESSIONNO),
                            new SqlParameter("@P_DEGREENO",objScholarship.DEGREENO),
                            new SqlParameter("@P_BRANCHNO",objScholarship.BRANCHNO),
                            new SqlParameter("@P_SEMESTERNO",objScholarship.SEMESTERNO),
                            new SqlParameter("@P_LOCK_UANO",objScholarship.UANO),
                            new SqlParameter("@P_LOCK_IP",objScholarship.IPADDRESS),
                            new SqlParameter("@P_MONTHNO",objScholarship.MONTHNO),
                            new SqlParameter("@P_YEARNO",objScholarship.YEARNO),
                            new SqlParameter("@P_IDNO",objScholarship.IDNO),
                        };
                        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

                        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_STUDENT_SCHOLARSHIP_LOCK", sqlParams, true);//PKG_ACAD_BATCH_INSERT

                        if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001" && obj.ToString() == "1")
                            status = Convert.ToInt32(CustomStatus.RecordSaved);
                        else if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001" && obj.ToString() == "2")
                            status = Convert.ToInt32(CustomStatus.RecordUpdated);
                        else
                            status = Convert.ToInt32(CustomStatus.Error);
                    }
                    catch (Exception ex)
                    {
                        status = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ScholarshipController.AddScholarship() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return status;
                }

                public int AddScholarship(Scholarship objScholarship)
                {
                    int status = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] sqlParams = new SqlParameter[]
                        {
                            new SqlParameter("@P_SESSIONNO",objScholarship.SESSIONNO),
                            new SqlParameter("@P_COLLEGE_ID",objScholarship.College_ID),
                            new SqlParameter("@P_DEGREENO",objScholarship.DEGREENO),
                            new SqlParameter("@P_BRANCHNO",objScholarship.BRANCHNO),
                            new SqlParameter("@P_SEMESTERNO",objScholarship.SEMESTERNO),
                            new SqlParameter("@P_AMOUNT",objScholarship.AMOUNT),
                            new SqlParameter("@P_ACTAMT",objScholarship.ACTAMT),
                            new SqlParameter("@P_HRA",objScholarship.HRA),
                            new SqlParameter("@P_NETAMT",objScholarship.NETAMT),
                            new SqlParameter("@P_ACC_NO",objScholarship.ACC_NO),
                            new SqlParameter("@P_TOTDAYS",objScholarship.TOTDAYS),
                            new SqlParameter("@P_MONTHNO",objScholarship.MONTHNO),
                            new SqlParameter("@P_YEARNO",objScholarship.YEARNO),
                            new SqlParameter("@P_IDNO",objScholarship.IDNO),
                        };
                        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

                        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_STUDENT_SCHOLARSHIP", sqlParams, true);//PKG_ACAD_BATCH_INSERT

                        if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001" && obj.ToString() == "1")
                            status = Convert.ToInt32(CustomStatus.RecordSaved);
                        else if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001" && obj.ToString() == "2")
                            status = Convert.ToInt32(CustomStatus.RecordUpdated);
                        else
                            status = Convert.ToInt32(CustomStatus.Error);
                    }
                    catch (Exception ex)
                    {
                        status = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ScholarshipController.AddScholarship() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return status;
                }

                public int UnLockScholarship(Scholarship objScholarship)
                {
                    int status = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] sqlParams = new SqlParameter[]
                        {
                            new SqlParameter("@P_SESSIONNO",objScholarship.SESSIONNO),
                            new SqlParameter("@P_DEGREENO",objScholarship.DEGREENO),
                            new SqlParameter("@P_BRANCHNO",objScholarship.BRANCHNO),
                            new SqlParameter("@P_SEMESTERNO",objScholarship.SEMESTERNO),
                            new SqlParameter("@P_UNLOCK_UANO",objScholarship.UANO),
                            new SqlParameter("@P_UNLOCK_IP",objScholarship.IPADDRESS),
                            new SqlParameter("@P_MONTHNO",objScholarship.MONTHNO),
                            new SqlParameter("@P_YEARNO",objScholarship.YEARNO),
                            new SqlParameter("@P_IDNO",objScholarship.IDNO),
                        };
                        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

                        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_STUDENT_SCHOLARSHIP_UNLOCK", sqlParams, true);//PKG_ACAD_BATCH_INSERT

                        if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001" && obj.ToString() == "1")
                            status = Convert.ToInt32(CustomStatus.RecordSaved);
                        else if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001" && obj.ToString() == "2")
                            status = Convert.ToInt32(CustomStatus.RecordUpdated);
                        else
                            status = Convert.ToInt32(CustomStatus.Error);
                    }
                    catch (Exception ex)
                    {
                        status = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ScholarshipController.AddScholarship() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return status;
                }

                public DataSet GetChekers(int sessionno, int collegeid, int degreeno, int branchno, int semesterno, int monthno, int yearno, string date, int ua_no)
                {
                    DataSet ds = null;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[8];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_COLLEGE_ID", collegeid);
                        objParams[2] = new SqlParameter("@P_DEGREENO", degreeno);
                        objParams[3] = new SqlParameter("@P_BRANCHNO", branchno);
                        objParams[4] = new SqlParameter("@P_SEMESTERNO", semesterno);
                        objParams[5] = new SqlParameter("@P_MONTHNO", monthno);
                        objParams[6] = new SqlParameter("@P_YEARNO", yearno);
                        objParams[7] = new SqlParameter("@P_UA_NO", ua_no);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_GET_CHECKER_DETAILS_NEW", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetChekers-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetBranchBasedsemester(int sessionno, int branchno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_DEGREENO", branchno);
                        ds = objSQLHelper.ExecuteDataSetSP("ACD_SEMESTER_DURATIONWISE", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TeachingPlanController.GetSingleTeachingPlanEntry -> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetAllSession(int College_id)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_COLLEGE_ID", College_id);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_SESSION_SP_ALL_SESSIONMASTER", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.SessionController.GetAllSession-> " + ex.ToString());
                    }
                    return ds;
                }

                public int AddEligibleStudents(IITMS.UAIMS.BusinessLayer.BusinessEntities.Session objSession, int uano, DateTime transdate, string ipaddress, string bankname, string accno, string ifsc)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        //Add
                        objParams = new SqlParameter[16];
                        objParams[0] = new SqlParameter("@P_ADMBATCH", objSession.Admbatch);
                        objParams[1] = new SqlParameter("@P_IDNO", objSession.StudId);
                        objParams[2] = new SqlParameter("@P_COLLEGEID", objSession.College_ID);
                        objParams[3] = new SqlParameter("@P_DEGREENO", objSession.Degree_No);
                        objParams[4] = new SqlParameter("@P_BRANCHNO", objSession.Branch_No);
                        objParams[5] = new SqlParameter("@P_SEMESTERNO", objSession.Semester_No);
                        objParams[6] = new SqlParameter("@P_COLLEGE_CODE", objSession.College_code);
                        objParams[7] = new SqlParameter("@P_STATUS", objSession.Status);
                        objParams[8] = new SqlParameter("@P_AMOUNT", objSession.TotAmount);
                        objParams[9] = new SqlParameter("@P_UANO", uano);
                        objParams[10] = new SqlParameter("@P_TRANSDATE", transdate);
                        objParams[11] = new SqlParameter("@P_IPADDRESS", ipaddress);
                        objParams[12] = new SqlParameter("@P_BANKNAME", bankname);
                        objParams[13] = new SqlParameter("@P_ACCNO", accno);
                        objParams[14] = new SqlParameter("@P_IFSC", ifsc);
                        objParams[15] = new SqlParameter("@P_CERTNO", SqlDbType.Int);
                        objParams[15].Direction = ParameterDirection.Output;

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_SCHO_ELIGIBLESTUDENTS_SP_INS", objParams, true) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.SessionController.AddSession-> " + ex.ToString());
                    }
                    return retStatus;
                }

                // ADDED BY PRITISH ON 04/06/2021
                public DataSet GetScholarshipDetails(IITMS.UAIMS.BusinessLayer.BusinessEntities.Session objSession)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_ADMBATCH", objSession.Admbatch);
                        objParams[1] = new SqlParameter("@P_COLLEGE_ID", objSession.College_ID);
                        objParams[2] = new SqlParameter("@P_DEGREENO", objSession.Degree_No);
                        objParams[3] = new SqlParameter("@P_BRANCHNO", objSession.Branch_No);
                        objParams[4] = new SqlParameter("@P_SEMESTERNO", objSession.Semester_No);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_SCHOLARSHIP_DETAILS", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.SessionController.GetAllAmount-> " + ex.ToString());
                    }
                    return ds;
                }

                public int AddScholarshipInfo(Session objSession, int payMode, string scholarshipNo)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        //Add
                        objParams = new SqlParameter[10];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", objSession.SessionNo);
                        objParams[1] = new SqlParameter("@P_ADMBATCH", objSession.Admbatch);
                        objParams[2] = new SqlParameter("@P_COLLEGE_ID", objSession.College_ID);
                        objParams[3] = new SqlParameter("@P_DEGREENO", objSession.Degree_No);
                        objParams[4] = new SqlParameter("@P_BRANCHNO", objSession.Branch_No);
                        objParams[5] = new SqlParameter("@P_SEMESTERNO", objSession.Semester_No);
                        objParams[6] = new SqlParameter("@P_IDNO", objSession.StudId);
                        objParams[7] = new SqlParameter("@P_PMODE", payMode);
                        objParams[8] = new SqlParameter("@P_SCHOLARSHIPNO", scholarshipNo);
                        objParams[9] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[9].Direction = ParameterDirection.Output;

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ADD_SCHOLARSHIP_INFO", objParams, true) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.SessionController.AddSession-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetAllAmount(IITMS.UAIMS.BusinessLayer.BusinessEntities.Session objSession, int ua_no)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_ADMBATCH", objSession.Admbatch);
                        objParams[1] = new SqlParameter("@P_COLLEGE_ID", objSession.College_ID);
                        objParams[2] = new SqlParameter("@P_DEGREENO", objSession.Degree_No);
                        objParams[3] = new SqlParameter("@P_BRANCHNO", objSession.Branch_No);
                        objParams[4] = new SqlParameter("@P_SEMESTERNO", objSession.Semester_No);
                        objParams[5] = new SqlParameter("@P_UA_NO", ua_no);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_AMOUNT_SP_ALL_SCHOLARSHIPAMOUNT_MASTER", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.SessionController.GetAllAmount-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetAllSession_Modified()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_SESSION_SP_ALL_SESSION_MODIFIED", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.SessionController.GetAllSession_Modified-> " + ex.ToString());
                    }
                    return ds;
                }

                public int AddSession_Modified(IITMS.UAIMS.BusinessLayer.BusinessEntities.Session objSession, int AcademicNo, int Academic_Year)
                {
                    int status = 0;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        //Add
                        objParams = new SqlParameter[14];
                        objParams[0] = new SqlParameter("@P_SESSION_PNAME", objSession.Session_PName);
                        objParams[1] = new SqlParameter("@P_SESSION_STDATE", objSession.Session_SDate);
                        objParams[2] = new SqlParameter("@P_SESSION_ENDDATE", objSession.Session_EDate);
                        objParams[3] = new SqlParameter("@P_SESSION_NAME", objSession.Session_Name);
                        objParams[4] = new SqlParameter("@P_ODD_EVEN", objSession.Odd_Even);
                        objParams[5] = new SqlParameter("@P_SESSNAME_HINDI", objSession.Sessname_hindi);
                        objParams[6] = new SqlParameter("@P_COLLEGE_CODE", objSession.College_code);
                        objParams[7] = new SqlParameter("@P_FLOCK", objSession.Flock);
                        objParams[8] = new SqlParameter("@P_EXAMTYPE", objSession.ExamType);
                        objParams[9] = new SqlParameter("@P_ACADEMIC_YEAR", Academic_Year);
                        objParams[10] = new SqlParameter("@P_ACADEMIC", AcademicNo);
                        objParams[11] = new SqlParameter("@P_IS_ACTIVE", objSession.IsActive);
                        objParams[12] = new SqlParameter("@P_ORGANIZATION_ID", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]));
                        objParams[13] = new SqlParameter("@P_SESSIONID", SqlDbType.Int);
                        objParams[13].Direction = ParameterDirection.Output;

                        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_SESSION_SP_INS_SESSION_MODIFIED", objParams, true);

                        if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001" && obj.ToString() != "-2")
                            status = Convert.ToInt32(CustomStatus.RecordSaved);
                        else if (obj.ToString().Equals("-2"))
                        {
                            status = Convert.ToInt32(CustomStatus.RecordExist);
                        }
                        else
                            status = Convert.ToInt32(CustomStatus.Error);
                    }
                    catch (Exception ex)
                    {
                        status = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.SessionController.AddSession_Modified-> " + ex.ToString());
                    }
                    return status;
                }

                public int UpdateSession_Modified(IITMS.UAIMS.BusinessLayer.BusinessEntities.Session objSession, int AcademicNo, int Academic_Year)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        //update
                        objParams = new SqlParameter[13];
                        objParams[0] = new SqlParameter("@P_SESSIONID", objSession.SessionNo);
                        objParams[1] = new SqlParameter("@P_SESSION_PNAME", objSession.Session_PName);
                        objParams[2] = new SqlParameter("@P_SESSION_STDATE", objSession.Session_SDate);
                        objParams[3] = new SqlParameter("@P_SESSION_ENDDATE", objSession.Session_EDate);
                        objParams[4] = new SqlParameter("@P_SESSION_NAME", objSession.Session_Name);
                        objParams[5] = new SqlParameter("@P_ODD_EVEN", objSession.Odd_Even);
                        objParams[6] = new SqlParameter("@P_SESSNAME_HINDI", objSession.Sessname_hindi);
                        objParams[7] = new SqlParameter("@P_EXAMTYPE", objSession.ExamType);
                        objParams[8] = new SqlParameter("@P_ACADEMIC_YEAR", Academic_Year);
                        objParams[9] = new SqlParameter("@P_ACADEMIC", AcademicNo);
                        objParams[10] = new SqlParameter("@P_FLOCK", objSession.Flock);
                        objParams[11] = new SqlParameter("@P_IS_ACTIVE", objSession.IsActive);
                        objParams[12] = new SqlParameter("@P_ORGANIZATION_ID", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]));

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_SESSION_SP_UPD_SESSION_MODIFIED", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.SessionController.UpdateSession_Modified-> " + ex.ToString());
                    }

                    return retStatus;
                }

                public int AddSessionMaster_Modified(string College_id, int Sessionid, int Sessionno, int Ua_no, int Active)
                {
                    int status = 0;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_SESSIONID", Sessionid);
                        objParams[1] = new SqlParameter("@P_COLLEGE_ID", College_id);
                        objParams[2] = new SqlParameter("@P_SESSIONNO", Sessionno);
                        objParams[3] = new SqlParameter("@P_UANO", Ua_no);
                        objParams[4] = new SqlParameter("@P_STATUS", Active);
                        objParams[5] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[5].Direction = ParameterDirection.Output;

                        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_SESSION_SP_INS_UPD_SESSION_MAPPING", objParams, true);

                        if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001" && obj.ToString() != "-2")
                            status = Convert.ToInt32(CustomStatus.RecordSaved);
                        else if (obj.ToString().Equals("-2"))
                        {
                            status = Convert.ToInt32(CustomStatus.RecordExist);
                        }
                        else if (obj.ToString().Equals("2"))
                        {
                            status = Convert.ToInt32(CustomStatus.RecordUpdated);
                        }
                        else
                            status = Convert.ToInt32(CustomStatus.Error);
                    }
                    catch (Exception ex)
                    {
                        status = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.SessionController.AddSessionMaster_Modified-> " + ex.ToString());
                    }
                    return status;
                }
            }
        }
    }
}