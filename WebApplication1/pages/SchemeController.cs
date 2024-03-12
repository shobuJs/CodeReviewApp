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
            public class SchemeController
            {
                private string _UAIMS_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                public int AddScheme(Scheme objScheme, int YEARNO, int BRIDGING, int intake, int IsLatest)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        //Add
                        objParams = new SqlParameter[20];
                        objParams[0] = new SqlParameter("@P_SCHEMENAME", objScheme.SchemeName);
                        objParams[1] = new SqlParameter("@P_BRANCHNO", objScheme.BranchNo);
                        objParams[2] = new SqlParameter("@P_DEGREENO", objScheme.DegreeNo);
                        objParams[3] = new SqlParameter("@P_DEPTNO", objScheme.Dept_No);
                        objParams[4] = new SqlParameter("@P_SEMESTERNO", objScheme.SemesterNo);
                        objParams[5] = new SqlParameter("@P_YEAR", objScheme.Year);
                        objParams[6] = new SqlParameter("@P_NEWSCHEME", objScheme.NewScheme);
                        objParams[7] = new SqlParameter("@P_COLLEGE_CODE", objScheme.CollegeCode);
                        objParams[8] = new SqlParameter("@P_SCHEMETYPENO", objScheme.SchemeTypeNo);
                        objParams[9] = new SqlParameter("@P_PATH_NO", objScheme.Path_no);
                        objParams[10] = new SqlParameter("@P_PATTERNNO", objScheme.PatternNo);
                        objParams[11] = new SqlParameter("@P_MINIMUM_CREDITS", objScheme.MinimumCredits);
                        objParams[12] = new SqlParameter("@P_GRADEMARKS", objScheme.gradeMarks);
                        objParams[13] = new SqlParameter("@P_COLLEGE_ID", objScheme.College_id);
                        objParams[14] = new SqlParameter("@P_GRADING_SCHEME_NO", objScheme.GradingSchemeNo);
                        objParams[15] = new SqlParameter("@P_YEARNO", YEARNO);
                        objParams[16] = new SqlParameter("@P_BRIDGING", BRIDGING);
                        objParams[17] = new SqlParameter("@P_INTAKE", intake);
                        objParams[18] = new SqlParameter("@P_IS_LATEST", IsLatest);
                        objParams[19] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[19].Direction = ParameterDirection.Output;

                        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_SCHEME_SP_INS_SCHEME", objParams, true);
                        if (obj != null)
                        {
                            int ret = Convert.ToInt32(obj);
                            if (ret == 1)
                            {
                                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                            }
                            else
                            {
                                retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
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
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ShemeController.AddScheme-> " + ex.ToString());
                    }

                    return retStatus;
                }

                public int UpdateScheme(Scheme objScheme, int YEARNO, int BRIDGING, int intake, int IsLatest)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[19];
                        objParams[0] = new SqlParameter("@P_SCHEMENO", objScheme.SchemeNo);
                        objParams[1] = new SqlParameter("@P_SCHEMENAME", objScheme.SchemeName);
                        objParams[2] = new SqlParameter("@P_BRANCHNO", objScheme.BranchNo);
                        objParams[3] = new SqlParameter("@P_DEGREENO", objScheme.DegreeNo);
                        objParams[4] = new SqlParameter("@P_DEPTNO", objScheme.Dept_No);
                        objParams[5] = new SqlParameter("@P_SEMESTERNO", objScheme.SemesterNo);
                        objParams[6] = new SqlParameter("@P_YEAR", objScheme.Year);
                        objParams[7] = new SqlParameter("@P_SCHEMETYPENO", objScheme.SchemeTypeNo);
                        objParams[8] = new SqlParameter("@P_PATH_NO", objScheme.Path_no);
                        objParams[9] = new SqlParameter("@P_GRADE_TYPE", objScheme.Path_no);
                        objParams[10] = new SqlParameter("@P_PATTERNNO", objScheme.PatternNo);
                        objParams[11] = new SqlParameter("@P_MINIMUM_CREDITS", objScheme.MinimumCredits);
                        objParams[12] = new SqlParameter("@P_GRADEMARKS", objScheme.gradeMarks);
                        objParams[13] = new SqlParameter("@P_COLLEGE_ID", objScheme.College_id);
                        objParams[14] = new SqlParameter("@P_GRADING_SCHEME_NO", objScheme.GradingSchemeNo);
                        objParams[15] = new SqlParameter("@P_YEARNO", YEARNO);
                        objParams[16] = new SqlParameter("@P_BRIDGING", BRIDGING);
                        objParams[17] = new SqlParameter("@P_INTAKE", intake);
                        objParams[18] = new SqlParameter("@P_IS_LATEST", IsLatest);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_SCHEME_SP_UPD_SCHEME", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.SchemeController.UpdateScheme-> " + ex.ToString());
                    }

                    return retStatus;
                }

                /// <summary>
                /// This method is used to get all scheme.
                /// </summary>
                /// <param name="deptno">Get scheme as per this deptno.</param>
                /// <param name="semno">Get scheme as per this semno.</param>
                /// <returns>DataSet</returns>

                public DataSet GetScheme(int deptno, int degreeno)
                {
                    DataSet ds = null;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_DEPT_NO", deptno);
                        objParams[1] = new SqlParameter("@P_DEGREE_NO", degreeno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_SCHEME_SP_ALL_SCHEME", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.SchemeController.GetScheme-> " + ex.ToString());
                    }
                    return ds;
                }

                public SqlDataReader GetSingleScheme(int schemeno)
                {
                    SqlDataReader dr = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_SCHEMENO", schemeno);

                        dr = objSQLHelper.ExecuteReaderSP("PKG_SCHEME_SINGLE_SCHEME", objParams);
                    }
                    catch (Exception ex)
                    {
                        return dr;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.SchemeController.GetSingleScheme-> " + ex.ToString());
                    }
                    return dr;
                }

                /// <summary>
                /// This method is used to retrieve scheme.
                /// </summary>
                /// <param name="sem_no">Retrieve scheme as per this sem_no.</param>
                /// <param name="stype">Retrieve scheme as per this stype.</param>
                /// <param name="degree">Retrieve scheme as per degree.</param>
                /// <returns>DataSet</returns>
                public DataSet CopySchemeBySem(int batchNo, int semesterNo, int degreeNo)
                {
                    DataSet ds = null;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_BATCHNO", batchNo);
                        objParams[1] = new SqlParameter("@P_SEMESTERNO", semesterNo);
                        objParams[2] = new SqlParameter("@P_DEGREENO", degreeNo);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_SCH_SP_RET_ALL_COPYSCHEME_BYSEM", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.SchemeController.CopySchemeBySem-> " + ex.ToString());
                    }

                    return ds;
                }

                public int AddCopyScheme(Scheme objScheme)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        //Add
                        objParams = new SqlParameter[10];
                        objParams[0] = new SqlParameter("@P_SCHEMENAME", objScheme.SchemeName);
                        objParams[1] = new SqlParameter("@P_SEMESTERNO", objScheme.SemesterNo);
                        objParams[2] = new SqlParameter("@P_BRANCHNO", objScheme.BranchNo);
                        objParams[3] = new SqlParameter("@P_DEGREENO", objScheme.DegreeNo);
                        objParams[4] = new SqlParameter("@P_DEPTNO", objScheme.Dept_No);
                        objParams[5] = new SqlParameter("@P_BATCHNO", objScheme.BatchNo);
                        objParams[6] = new SqlParameter("@P_NEWSCHEME", objScheme.NewScheme);
                        objParams[7] = new SqlParameter("@P_SCHNO", objScheme.SchemeNo);
                        objParams[8] = new SqlParameter("@P_COLLEGE_CODE", objScheme.CollegeCode);
                        objParams[9] = new SqlParameter("@P_SCHEMENO", SqlDbType.Int);
                        objParams[9].Direction = ParameterDirection.Output;

                        //if (objSQLHelper.ExecuteNonQuerySP("PKG_SCHEME_SP_INS_SCHEME_COPY", objParams, true) != null)
                        //  retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_SCHEME_SP_INS_SCHEME_COPY", objParams, true);

                        if (ret != null)
                        {
                            int schemeno = Convert.ToInt32(ret);
                            if (schemeno == -99)
                                retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                            else
                                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                        {
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        }
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ShemeController.AddScheme-> " + ex.ToString());
                    }

                    return retStatus;
                }

                /// <summary>
                /// This method is used to Get All New Schemes
                /// </summary>
                /// <returns>DataSet</returns>
                public DataSet GetAllNewScheme()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[0];

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_SCHEME_SP_ALL_NEWSCHEME", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.SchemeController.GetAllNewScheme-> " + ex.ToString());
                    }

                    return ds;
                }
            }
        }
    }
}