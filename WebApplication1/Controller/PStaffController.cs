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
            /// This CourseController is used to control Course table.
            /// </summary>
            public partial class PStaffController
            {
                /// <summary>
                /// ConnectionStrings
                /// </summary>
                private string _UAIMS_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                //modified by reena on 21_10_16

                public DataSet GetStaffList(string Internal)   //modified by reena on 21_10_16
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objDataAccess = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] sqlParams = new SqlParameter[]
                        {
                            new SqlParameter("@P_INTERNAL_EXTERNAL", Internal)
                        };

                        ds = objDataAccess.ExecuteDataSetSP("PKG_ACAD_GET_STAFF", sqlParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessEntities.PStaffController.GetStaffList() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }

                public DataSet GetStaffDetails(int staffNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objDataAccess = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_STAFF_NO", staffNo)
                };
                        ds = objDataAccess.ExecuteDataSetSP("PKG_ACAD_RET_STAFF_BY_STAFFNO", sqlParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessEntities.PStaffController.GetStaffDetails() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }

                public int AddNewInternalStaff(int ua_no)
                {
                    int status = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objDataAccess = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] sqlParams = new SqlParameter[]
                            {
                                new SqlParameter("@P_UA_NO",ua_no)
                            };
                        object ret = (Int32)objDataAccess.ExecuteNonQuerySP("PKG_ACAD_INS_INTERNAL_NEW_STAFF", sqlParams, true);
                        status = (int)ret;
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessEntities.PStaffController.AddNewInternalStaff() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return status;
                }

                //added by reena on 15_10_16
                public int AddStaffPreference(int staffno, string ccode, int ps_mod, int sessionno)
                {
                    int status = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objDataAccess = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_STAFFNO", staffno),

                    new SqlParameter("@P_CCODE", ccode),
                    new SqlParameter("@P_PS_MOD", ps_mod),
                    new SqlParameter("@P_SESSIONNO",sessionno)
                };

                        object ret = (Int32)objDataAccess.ExecuteNonQuerySP("PKG_ACAD_INS_PAPERSET_PREFERENCE", sqlParams, true);

                        status = (int)ret;
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessEntities.PStaffController.AddStaffPreference() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return status;
                }

                public int DeleteStaffPreference(int staffno, string ccode, int ps_mod)
                {
                    int status = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objDataAccess = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_STAFFNO", staffno),

                    new SqlParameter("@P_CCODE", ccode),
                    new SqlParameter("@P_PS_MOD", ps_mod)
                };

                        object ret = (Int32)objDataAccess.ExecuteNonQuerySP("PKG_ACAD_DEL_PAPERSET_PREFERENCE", sqlParams, true);

                        status = (int)ret;
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessEntities.PStaffController.AddStaffPreference() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return status;
                }

                public int UpdateStaff(PStaff objPStaff)
                {
                    int status = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objDataAccess = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_STAFFNO", objPStaff.PStaffNo),
                    new SqlParameter("@P_STAFF_NAME", objPStaff.PStaffName),
                    new SqlParameter("@P_STAFF_ADDRESS", objPStaff.PStaffAddress),
                    new SqlParameter("@P_CONTACTNO", objPStaff.Contactno),
                    new SqlParameter("@P_EMAIL_ID", objPStaff.Emailid),
                    new SqlParameter("@P_INTERNAL_EXTERNAL", objPStaff.Internal_External),
                    new SqlParameter("@P_COLLEGE_CODE", objPStaff.CollegeCode),
                };

                        object ret = (Int32)objDataAccess.ExecuteNonQuerySP("PKG_ACAD_UPD_STAFF", sqlParams, false);

                        if (ret != null)
                            status = Convert.ToInt32(CustomStatus.RecordUpdated);
                        else
                            status = Convert.ToInt32(CustomStatus.TransactionFailed);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessEntities.PStaffController.UpdateStaff() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return status;
                }

                //INSTITUTE
                //=============
                public int AddInstitute(string code, string name, string add1, string add2, string add3, string contact_no, string col_code)
                {
                    int status = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objDataAccess = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] sqlParams = new SqlParameter[]
                    {
                        new SqlParameter("@P_INSTITUTE_CODE", code),
                        new SqlParameter("@P_INSTITUTE_NAME", name),
                        new SqlParameter("@P_INSTITUTE_ADDRESS1", add1),
                        new SqlParameter("@P_INSTITUTE_ADDRESS2", add2),
                        new SqlParameter("@P_INSTITUTE_ADDRESS3", add3),
                        new SqlParameter("@P_CONTACTNO", contact_no),
                        new SqlParameter("@P_COLLEGE_CODE", col_code),
                        new SqlParameter("@P_OUT", SqlDbType.Int),
                    };
                        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;

                        object ret = (Int32)objDataAccess.ExecuteNonQuerySP("PKG_ACAD_INS_INSTITUTE", sqlParams, true);

                        if (Convert.ToInt32(ret) == -99)
                            status = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            status = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessEntities.PStaffController.AddStaff() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return status;
                }

                public int UpdateInstitute(int inst_no, string code, string name, string add1, string add2, string add3, string contact_no)
                {
                    int status = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objDataAccess = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] sqlParams = new SqlParameter[]
                    {
                        new SqlParameter("@P_INSTITUTE_NO", inst_no),
                        new SqlParameter("@P_INSTITUTE_CODE", code),
                        new SqlParameter("@P_INSTITUTE_NAME", name),
                        new SqlParameter("@P_INSTITUTE_ADDRESS1", add1),
                        new SqlParameter("@P_INSTITUTE_ADDRESS2", add2),
                        new SqlParameter("@P_INSTITUTE_ADDRESS3", add3),
                        new SqlParameter("@P_CONTACTNO", contact_no),
                        new SqlParameter("@P_OUT", SqlDbType.Int),
                    };
                        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;

                        object ret = (Int32)objDataAccess.ExecuteNonQuerySP("PKG_ACAD_UPD_INSTITUTE", sqlParams, true);

                        if (Convert.ToInt32(ret) == -99)
                            status = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            status = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessEntities.PStaffController.AddStaff() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return status;
                }

                public int DeleteStaff(int staffNo)
                {
                    int status = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objDataAccess = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_STAFFNO", staffNo)
                };
                        object ret = (Int32)objDataAccess.ExecuteNonQuerySP("PKG_ACAD_DEL_STAFF", sqlParams, true);

                        if (Convert.ToInt32(ret) == -99)
                            status = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            status = Convert.ToInt32(CustomStatus.RecordDeleted);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessEntities.PStaffController.DeleteStaff() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return status;
                }

                public int AddSupervisorName(PStaff objSupr)
                {
                    int status = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_SUPERVISORNAME",objSupr.SupervisorName),
                    new SqlParameter("@P_DEPTNO", objSupr.DeptNo),
                    new SqlParameter("@P_TYPE", objSupr.Type),
                    new SqlParameter("@P_TYPENAME", objSupr.TypeName),
                    new SqlParameter("@P_COLLEGE_CODE", objSupr.CollegeCode),
                    new SqlParameter("@P_SUPERVISORNO", status)
                };
                        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

                        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_SUPERVISORNAME_INSERT", sqlParams, true);

                        if (obj != null && obj.ToString() != "-99")
                            status = Convert.ToInt32(CustomStatus.RecordSaved);
                        else
                            status = Convert.ToInt32(CustomStatus.Error);
                    }
                    catch (Exception ex)
                    {
                        status = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.PStaffController.AddSupervisorName() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return status;
                }

                public SqlDataReader GetSupervisorNo(int SupervisorNo)
                {
                    SqlDataReader dr = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[] { new SqlParameter("@P_SUPERVISORNO", SupervisorNo) };

                        dr = objSQLHelper.ExecuteReaderSP("PKG_ACAD_SUPERVISOR_GET_BY_NO", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.PStaffController.GetSupervisorNo() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return dr;
                }

                public int UpdateSupervisorName(PStaff objSupr)
                {
                    int status = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_SUPERVISORNAME",objSupr.SupervisorName),
                    new SqlParameter("@P_DEPTNO", objSupr.DeptNo),
                    new SqlParameter("@P_TYPE", objSupr.Type),
                    new SqlParameter("@P_TYPENAME", objSupr.TypeName),
                    new SqlParameter("@P_COLLEGE_CODE", objSupr.CollegeCode),
                    new SqlParameter("@P_SUPERVISORNO", objSupr.SupervisorNo)
                };
                        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

                        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_SUPERVISORNAME_UPDATE", sqlParams, true);

                        if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001")
                            status = Convert.ToInt32(CustomStatus.RecordUpdated);
                        else
                            status = Convert.ToInt32(CustomStatus.Error);
                    }
                    catch (Exception ex)
                    {
                        status = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.PStaffController.UpdateSupervisorName() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return status;
                }

                public DataSet GetAllSupervisorName()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[0];

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_SUPERVISORNAME_GET_ALL", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.PStaffController.GetAllSupervisorName() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }

                public int AddDRCDetail(PStaff objSupr)
                {
                    int status = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] sqlParams = new SqlParameter[]
                        {
                            new SqlParameter("@P_DRCNAME",objSupr.DRCName),
                            new SqlParameter("@P_DEPTNO", objSupr.DeptNo),
                            new SqlParameter("@P_COLLEGE_CODE", objSupr.CollegeCode),
                            new SqlParameter("@P_DRCNO", status)
                        };
                        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

                        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_DRCNAME_INSERT", sqlParams, true);

                        if (obj != null && obj.ToString() != "-99")
                            status = Convert.ToInt32(CustomStatus.RecordSaved);
                        else
                            status = Convert.ToInt32(CustomStatus.Error);
                    }
                    catch (Exception ex)
                    {
                        status = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.PStaffController.AddSupervisorName() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return status;
                }

                public int UpdateDRCName(PStaff objSupr)
                {
                    int status = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_DRCNAME",objSupr.DRCName),
                    new SqlParameter("@P_DEPTNO", objSupr.DeptNo),
                    new SqlParameter("@P_COLLEGE_CODE", objSupr.CollegeCode),
                    new SqlParameter("@P_DRCNO", objSupr.DRCNo)
                };
                        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

                        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_DRCNAME_UPDATE", sqlParams, true);

                        if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001")
                            status = Convert.ToInt32(CustomStatus.RecordUpdated);
                        else
                            status = Convert.ToInt32(CustomStatus.Error);
                    }
                    catch (Exception ex)
                    {
                        status = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.PStaffController.UpdateDRCName() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return status;
                }

                public SqlDataReader GetDRCNo(int DrcNo)
                {
                    SqlDataReader dr = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[] { new SqlParameter("@P_DRCNO", DrcNo) };

                        dr = objSQLHelper.ExecuteReaderSP("PKG_ACAD_DRC_GET_BY_NO", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.PStaffController.GetSupervisorNo() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return dr;
                }

                public DataSet GetAllDRCName()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[0];

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_DRCNAME_GET_ALL", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.PStaffController.GetAllSupervisorName() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }

                //added by reena on 11_7_16

                public DataSet GetPaperSetterStudCount1(int sessionno, int deptno, int semesterno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objDataAccess = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] sqlParams = new SqlParameter[]
                        {
                            new SqlParameter("@P_SESSIONNO", sessionno),
                            new SqlParameter("@P_DEPTNO", deptno),
                            new SqlParameter("@P_SEMSTERNO", semesterno)
                        };
                        ds = objDataAccess.ExecuteDataSetSP("ACAD_PAPER_SETTER_NAME_BOS_STUDCOUNT", sqlParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessEntities.PStaffController.GetPaperSetterStudCount() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }

                public DataSet GetPaperSetterNotDone(int sessionno, int deptno, int semesterno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objDataAccess = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] sqlParams = new SqlParameter[]
                        {
                            new SqlParameter("@P_SESSIONNO", sessionno),
                            new SqlParameter("@P_DEPTNO", deptno),
                            new SqlParameter("@P_SEMSTERNO", semesterno)
                        };
                        ds = objDataAccess.ExecuteDataSetSP("ACAD_PAPER_SETTER_NOT_ALLOT_BOS", sqlParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessEntities.PStaffController.GetPaperSetterStudCount() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }

                // ---  added by dipali for internal faculty enter into staff entry
                public DataSet GetPaperSetterInternalStaff(int deptno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objDataAccess = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] sqlParams = new SqlParameter[]
                        {
                            new SqlParameter("@P_DEPTNO", deptno)
                        };
                        ds = objDataAccess.ExecuteDataSetSP("ACD_PAPER_SET_INTERNAL_STAFF_ENTRY", sqlParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessEntities.PStaffController.GetPaperSetterStudCount() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }

                // -- added for all staff details
                public int AddAllStaff(PStaff objPStaff)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    int status;
                    try
                    {
                        SQLHelper objDataAccess = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_STAFF_NO", objPStaff.staffno ),
                    new SqlParameter("@P_STAFF_NAME", objPStaff.PStaffName),
                    new SqlParameter("@P_STAFF_ADDRESS", objPStaff.PStaffAddress),
                    new SqlParameter("@P_CONTACTNO", objPStaff.Contactno),
                    new SqlParameter("@P_EMAIL_ID", objPStaff.Emailid),
                    new SqlParameter("@P_QUALIFICATION", objPStaff.Qualification),
                    new SqlParameter("@P_UA_NO ", objPStaff.Uno),
                    new SqlParameter("@P_INTERNAL_EXTERNAL", objPStaff.Internal_External),
                    new SqlParameter("@P_TEACH_EXP", objPStaff.Teach_exp),
                    new SqlParameter("@P_SPECIALIZATION", objPStaff.Specialization),
                    new SqlParameter("@P_DEPTNO", objPStaff.DeptNo),
                    new SqlParameter("@P_COLLEGE_CODE", objPStaff.CollegeCode),
                    new SqlParameter("@P_IDNOS", objPStaff.Idnos),
                    new SqlParameter("@P_OUT", SqlDbType.Int),
                };
                        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;

                        object ret = (Int32)objDataAccess.ExecuteNonQuerySP("PKG_ACAD_INS_STAFF_ALL", sqlParams, true);
                        status = Convert.ToInt16(ret);

                        if (ret != null)
                        {
                            retStatus = status;
                        }
                        else
                        {
                            retStatus = -99;
                        }

                        return retStatus;
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessEntities.PStaffController.AddStaff() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    // return ret;
                }

                //  -- add for paper set details
                public DataSet GetPaperSetCourseCollection(int sessionno, int deptno, int semesterno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objDataAccess = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] sqlParams = new SqlParameter[]
                {
                      new SqlParameter("@P_SESSIONNO", sessionno),
                      new SqlParameter("@P_DEPTNO", deptno),
                      new SqlParameter("@P_SEMESTERNO", semesterno)
                };
                        ds = objDataAccess.ExecuteDataSetSP("PKG_ACD_PAPERSET_COURSE_DETAILS", sqlParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessEntities.PStaffController.GetStaffDetails() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }

                public DataSet GetPaperSetStaffList(string Internal)   //modified by reena on 21_10_16
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objDataAccess = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] sqlParams = new SqlParameter[]
                        {
                            new SqlParameter("@P_INTERNAL_EXTERNAL", Internal)
                        };

                        ds = objDataAccess.ExecuteDataSetSP("PKG_ACD_GET_PAPERSET_STAFF", sqlParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessEntities.PStaffController.GetStaffList() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }

                public int AddPaperSetStaffPreference(string staffno, string ccode, int ps_mod, int sessionno, int courseno)
                {
                    int status = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objDataAccess = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_STAFFNO", staffno),
                    new SqlParameter("@P_COURSENO", courseno),
                    new SqlParameter("@P_CCODE", ccode),
                    new SqlParameter("@P_PS_MOD", ps_mod),
                    new SqlParameter("@P_SESSIONNO",sessionno),
                    new SqlParameter("@P_OUT",DbType.Int16)
                };

                        object ret = (Int32)objDataAccess.ExecuteNonQuerySP("PKG_ACAD_INS_PAPERSET_STAFF_PREFERENCE", sqlParams, true);

                        status = (int)ret;
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessEntities.PStaffController.AddStaffPreference() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return status;
                }

                public DataSet GetPaperSetPrefrenceStaffDetails(string Internal)   //modified by reena on 21_10_16
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objDataAccess = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] sqlParams = new SqlParameter[]
                        {
                            new SqlParameter("@P_INTERNAL_EXTERNAL", Internal)
                        };

                        ds = objDataAccess.ExecuteDataSetSP("PKG_ACD_PAPERSET_SATFF_PREFRECE_DETAILS", sqlParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessEntities.PStaffController.GetStaffList() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }

                // -- added by dipali for paper set
                public DataSet GetFacultyPaperStock(int deptno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objDataAccess = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] sqlParams = new SqlParameter[]
                        {
                            new SqlParameter("@P_DEPT", deptno)
                        };

                        ds = objDataAccess.ExecuteDataSetSP("PKG_ACD_PAPERSET_FACULTY_DETAILS", sqlParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessEntities.PStaffController.GetStaffList() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }

                public int UpdateFacultyQuanntity(int staffno, int bos_deptno, int qty, int balance)
                {
                    int retun_status = Convert.ToInt16(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[4];

                        objParams[0] = new SqlParameter("@P_STAFFNO", staffno);
                        objParams[1] = new SqlParameter("@P_BOS_DEPTNO", bos_deptno);
                        objParams[2] = new SqlParameter("@P_QTY", qty);
                        objParams[3] = new SqlParameter("@P_BALANCE", balance);

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_UPD_FACULTY_STOCK", objParams, true);
                        retun_status = Convert.ToInt16(ret);
                    }
                    catch (Exception ex)
                    {
                        retun_status = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.UpdateCourseQTY-> " + ex.ToString());
                    }

                    return retun_status;
                }

                public int AddStaff(PStaff objPStaff)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    int status;
                    try
                    {
                        SQLHelper objDataAccess = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_STAFF_NO", objPStaff.staffno ),
                    new SqlParameter("@P_STAFF_NAME", objPStaff.PStaffName),
                    new SqlParameter("@P_STAFF_ADDRESS", objPStaff.PStaffAddress),
                    new SqlParameter("@P_CONTACTNO", objPStaff.Contactno),
                    new SqlParameter("@P_EMAIL_ID", objPStaff.Emailid),
                    new SqlParameter("@P_QUALIFICATION", objPStaff.Qualification),
                    new SqlParameter("@P_UA_NO ", objPStaff.Uno),
                    new SqlParameter("@P_INTERNAL_EXTERNAL", objPStaff.Internal_External),
                    new SqlParameter("@P_TEACH_EXP", objPStaff.Teach_exp),
                    new SqlParameter("@P_SPECIALIZATION", objPStaff.Specialization),
                    new SqlParameter("@P_DEPTNO", objPStaff.DeptNo),
                    new SqlParameter("@P_COLLEGE_CODE", objPStaff.CollegeCode),
                    new SqlParameter("@P_INSTITUTION_NAME", objPStaff.InstitutionName),
                    new SqlParameter("@P_OUT", SqlDbType.Int),
                };
                        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;

                        object ret = (Int32)objDataAccess.ExecuteNonQuerySP("PKG_ACAD_INS_STAFF", sqlParams, true);
                        status = Convert.ToInt16(ret);

                        if (ret != null)
                        {
                            retStatus = status;
                        }
                        else
                        {
                            retStatus = -99;
                        }

                        return retStatus;
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessEntities.PStaffController.AddStaff() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    // return ret;
                }

                public DataSet GetPaperSetStaffList(int Sessionno, int Semesterno, int DeptNo, string Internal)   //modified by reena on 21_10_16
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objDataAccess = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] sqlParams = new SqlParameter[]
                        {
                            new SqlParameter("@P_SESSIONNO", Sessionno),
                            new SqlParameter("@P_SEMESTERNO", Semesterno),
                            new SqlParameter("@P_DEPTNO", DeptNo),
                            new SqlParameter("@P_INTERNAL_EXTERNAL", Internal)
                        };

                        ds = objDataAccess.ExecuteDataSetSP("PKG_ACD_GET_PAPERSET_STAFF", sqlParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessEntities.PStaffController.GetStaffList() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }
            }
        }//END: BusinessLayer.BusinessLogic
    }//END: UAIMS
}//END: IITMS