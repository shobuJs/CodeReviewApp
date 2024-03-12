//sing IITMS.SQLServer.SQLDAL;
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
            public class BranchController
            {
                private string _UAIMS_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                public int AddBranchType(Branch objBranchType)
                {
                    int status = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] sqlParams = new SqlParameter[]

                        //Add New Branch Type
                        {
                         new SqlParameter("@P_SHORTNAME", objBranchType.ShortName),
                         new SqlParameter("@P_LONGNAME", objBranchType.LongName),
                         new SqlParameter("@P_BRANCHNM_HINDI", objBranchType.BranchNameInHindi),
                         new SqlParameter("@P_INTAKE1", objBranchType.Intake1),
                         new SqlParameter("@P_INTAKE2", objBranchType.Intake2),
                         new SqlParameter("@P_INTAKE3", objBranchType.Intake3),
                         new SqlParameter("@P_INTAKE4", objBranchType.Intake4),
                         new SqlParameter("@P_INTAKE5", objBranchType.Intake5),
                         new SqlParameter("@P_DURATION", objBranchType.Duration),
                         new SqlParameter("@P_CODE", objBranchType.Code),
                         new SqlParameter("@P_UGPGPF", objBranchType.Ugpgpf),
                         new SqlParameter("@P_DEGREENO", objBranchType.DegreeNo),
                         new SqlParameter("@P_DEPTNO", objBranchType.DeptNo),
                         new SqlParameter("@P_COLLEGE_CODE", objBranchType.CollegeCode),
                         new SqlParameter("@P_BRANCHNO", status)
                    };
                        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;
                        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_INS_BRANCH", sqlParams, true);

                        if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001")
                            status = Convert.ToInt32(CustomStatus.RecordSaved);
                        else
                            status = Convert.ToInt32(CustomStatus.Error);
                    }
                    catch (Exception ex)
                    {
                        status = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.BranchController.AddBranchType() --> " + ex.Message + " " + ex.StackTrace);
                    }

                    return status;
                }

                public SqlDataReader GetBranchType(int branchno)
                {
                    SqlDataReader dr = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_BRANCHNO", branchno);

                        dr = objSQLHelper.ExecuteReaderSP("PKG_ACAD_SINGLE_BRANCH", objParams);
                    }
                    catch (Exception ex)
                    {
                        return dr;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.BranchController.GetBranchType-> " + ex.ToString());
                    }
                    return dr;
                }

                public int UpdateBranchType(Branch objBranch)
                {
                    int status;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] sqlParams = new SqlParameter[]

                        {
                         new SqlParameter("@P_SHORTNAME", objBranch.ShortName),
                         new SqlParameter("@P_LONGNAME ", objBranch.LongName),
                         new SqlParameter("@P_BRANCHNM_HINDI", objBranch.BranchNameInHindi),
                         new SqlParameter("@P_INTAKE1", objBranch.Intake1),
                         new SqlParameter("@P_INTAKE2", objBranch.Intake2),
                         new SqlParameter("@P_INTAKE3", objBranch.Intake3),
                         new SqlParameter("@P_INTAKE4", objBranch.Intake4),
                         new SqlParameter("@P_INTAKE5", objBranch.Intake5),
                         new SqlParameter("@P_DURATION", objBranch.Duration),
                         new SqlParameter("@P_CODE", objBranch.Code),
                         new SqlParameter("@P_UGPGPF", objBranch.Ugpgpf),
                         new SqlParameter("@P_DEGREENO", objBranch.DegreeNo),
                         new SqlParameter("@P_BRANCHNO", objBranch.BranchNo),
                         new SqlParameter("@P_DEPTNO", objBranch.DeptNo)
                    };

                        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

                        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_UPD_BRANCH", sqlParams, true);

                        if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001")
                            status = Convert.ToInt32(CustomStatus.RecordUpdated);
                        else
                            status = Convert.ToInt32(CustomStatus.Error);
                    }
                    catch (Exception ex)
                    {
                        status = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.BranchController.UpdateBranch() --> " + ex.Message + " " + ex.StackTrace);
                    }

                    return status;
                }

                public DataSet GetAllBranchType()
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
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.BranchController.GetAllBranchTypes-> " + ex.ToString());
                    }
                    return ds;
                }

                // BRANCH CHANGE METHOD BELOW

                public int AddChagneBranchData(Student objStudent)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        //Add Branch change data
                        objParams = new SqlParameter[8];
                        objParams[0] = new SqlParameter("@P_IDNO", objStudent.IdNo);
                        objParams[1] = new SqlParameter("@P_ENROLLMENTNO", objStudent.RollNo);
                        objParams[2] = new SqlParameter("@P_OLDBRANCHNO", objStudent.BranchNo);
                        objParams[3] = new SqlParameter("@P_NEWBRANCHNO", objStudent.NewBranchNo);
                        objParams[4] = new SqlParameter("@P_NEWENROLLMENTNO", objStudent.RegNo);
                        objParams[5] = new SqlParameter("@P_REMARK", objStudent.Remark);
                        objParams[6] = new SqlParameter("@P_COLLEGE_CODE", objStudent.CollegeCode);

                        objParams[7] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[7].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_STUDENT_SP_INS_BRANCH_CHANGE", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.BranchController.AddChagneBranchData-> " + ex.ToString());
                    }

                    return retStatus;
                }

                public int ChangeBranch_NewStudent(Student objStudent, int userno, string ipaddress, string NewEnrollNo)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        //Add Branch change data
                        objParams = new SqlParameter[11];
                        objParams[0] = new SqlParameter("@P_IDNO", objStudent.IdNo);

                        objParams[1] = new SqlParameter("@P_OLDBRANCHNO", objStudent.BranchNo);
                        objParams[2] = new SqlParameter("@P_NEWBRANCHNO", objStudent.NewBranchNo);
                        objParams[3] = new SqlParameter("@P_NEWENROLLMENTNO", NewEnrollNo);
                        objParams[4] = new SqlParameter("@P_REMARK", objStudent.Remark);
                        objParams[5] = new SqlParameter("@P_COLLEGE_CODE", objStudent.CollegeCode);
                        objParams[6] = new SqlParameter("@P_USERNO", userno);
                        objParams[7] = new SqlParameter("@P_IPADDRESS", ipaddress);
                        objParams[8] = new SqlParameter("@P_ENROLLNO", objStudent.EnrollNo);
                        objParams[9] = new SqlParameter("@P_DEGREENO", objStudent.DegreeNo);

                        objParams[10] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[10].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_STUDENT_SP_INS_BRANCH_CHANGE_NEW_ADMISSION", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else if (Convert.ToInt32(ret) == 2627)
                            retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.BranchController.AddChagneBranchData-> " + ex.ToString());
                    }

                    return retStatus;
                }

                //ADDED BY AASHNA -09-02-2022
                public int AddModuletransfer(int idno, int oldscheme, int newschme, string oldcode, string newcode, int oldcourseno, int newcourseno, int sessionno,
                    int uano, string ip, string collegecode, int status, string oldcredit, string newcredit)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        //Add Branch change data
                        objParams = new SqlParameter[15];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        objParams[1] = new SqlParameter("@P_OLD_SCHEMENO", oldscheme);
                        objParams[2] = new SqlParameter("@P_NEW_SCHEMENO", newschme);
                        objParams[3] = new SqlParameter("@P_OLD_CCODE", oldcode);
                        objParams[4] = new SqlParameter("@P_NEW_CCODE", newcode);
                        objParams[5] = new SqlParameter("@P_OLD_COURSENO", oldcourseno);
                        objParams[6] = new SqlParameter("@P_NEW_COURSENO", newcourseno);
                        objParams[7] = new SqlParameter("@P_OLD_CREDITS", oldcredit);
                        objParams[8] = new SqlParameter("@P_NEW_CREDITS", newcredit);
                        objParams[9] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[10] = new SqlParameter("@P_UA_NO", uano);
                        objParams[11] = new SqlParameter("@P_IPADDRESS", ip);
                        objParams[12] = new SqlParameter("@P_COLLEGE_CODE", collegecode);
                        objParams[13] = new SqlParameter("@P_STATUS", status);
                        objParams[14] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[14].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_STUDENT_SP_INS_MODULE_MAPPING_DATA", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.BranchController.AddChagneBranchData-> " + ex.ToString());
                    }

                    return retStatus;
                }

                //added by aashna 09-02-2022
                public DataSet GetprogramChange(string REGNO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_REGNO", REGNO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_GET_PROGRAM_TRANSFER_DATA", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.BranchController.GetAllBranchTypes-> " + ex.ToString());
                    }
                    return ds;
                }

                //added by aashna 09-02-2022
                public DataSet Getprogramdetails(string strSearch, int commandType)
                {
                    DataSet ds = null;
                    try
                    {
                        //@P_CommandType @P_SearchString
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_SearchString", strSearch);
                        objParams[1] = new SqlParameter("@P_CommandType", commandType);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_USER_DETAILS_PROGRAM_DATA", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.BranchController.GetAllBranchTypes-> " + ex.ToString());
                    }
                    return ds;
                }

                public int AddChagneBranch(Student objStudent, FeeDemand feeDemand, int olddegree, int oldbranch, int userno, int institute,
                      string IP, int oldclg, int oldcampus, int newcampus, string oldenroll, int sessionno, int schemeno, int EnrollChangeStatus)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        //Add Branch change data
                        objParams = new SqlParameter[21];
                        objParams[0] = new SqlParameter("@P_IDNO", objStudent.IdNo);
                        objParams[1] = new SqlParameter("@P_ENROLLMENTNO", objStudent.RollNo);
                        objParams[2] = new SqlParameter("@P_OLDBRANCHNO", oldbranch);
                        objParams[3] = new SqlParameter("@P_NEWBRANCHNO", feeDemand.BranchNo);
                        objParams[4] = new SqlParameter("@P_NEWENROLLMENTNO", objStudent.RegNo);
                        objParams[5] = new SqlParameter("@P_REMARK", objStudent.Remark);
                        objParams[6] = new SqlParameter("@P_COLLEGE_CODE", objStudent.CollegeCode);
                        objParams[7] = new SqlParameter("@P_USERNO", userno);
                        objParams[8] = new SqlParameter("@P_AFFILIATED_NO", institute);
                        objParams[9] = new SqlParameter("@P_IPADDRESS", IP);
                        objParams[10] = new SqlParameter("@P_OLD_COLLEGEID", oldclg);
                        objParams[11] = new SqlParameter("@P_NEW_COLLEGEID", feeDemand.College_ID);
                        objParams[12] = new SqlParameter("@P_OLD_DEGREENO", olddegree);
                        objParams[13] = new SqlParameter("@P_NEW_DEGREENO", feeDemand.DegreeNo);
                        objParams[14] = new SqlParameter("@P_OLDCAMPUS", oldcampus);
                        objParams[15] = new SqlParameter("@P_NEWCAMPUS", newcampus);
                        objParams[16] = new SqlParameter("@P_OLDREGNO", oldenroll);

                        objParams[17] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[18] = new SqlParameter("@P_SCHEMENO", schemeno);
                        objParams[19] = new SqlParameter("@P_REGNOCHANGE_STATUS", EnrollChangeStatus);


                        objParams[20] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[20].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_STUDENT_SP_INS_BRANCH_CHANGE_NEW_ADMISSION", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.BranchController.AddChagneBranchData-> " + ex.ToString());
                    }

                    return retStatus;
                }


                public DataSet getnewenoll(int idno, int degreeno, int branchno, int college_id)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_DEGREENO", degreeno);
                        objParams[1] = new SqlParameter("@P_BRANCHNO", branchno);
                        objParams[2] = new SqlParameter("@P_COLLEGE_ID", college_id);
                        objParams[3] = new SqlParameter("@P_IDNO", idno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_NEW_ENROLLNO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.BranchController.getnewenoll-> " + ex.ToString());
                    }
                    return ds;
                }

                //added by aashna 05-12-2022
                public int ImportDataForBranch(DataTable dtBulkData, int sessionno, int college_id, int
                    ugpg, int uano, int semesterno, string college_code, string apadd, string random, string filename)
                {
                    int retv = 0;
                    try
                    {
                        SQLHelper objSQL = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objPar = null;
                        {
                            objPar = new SqlParameter[11];
                            objPar[0] = new SqlParameter("@TBLEMPLOYEE", dtBulkData);
                            objPar[1] = new SqlParameter("@P_SESSIONNO", sessionno);
                            objPar[2] = new SqlParameter("@P_COLLEGE_ID", college_id);
                            objPar[3] = new SqlParameter("@P_UG_PG", ugpg);
                            objPar[4] = new SqlParameter("@P_SEMESTERNO", semesterno);
                            objPar[5] = new SqlParameter("@P_UANO ", uano);
                            objPar[6] = new SqlParameter("@P_COLLEGE_CODE", college_code);
                            objPar[7] = new SqlParameter("@P_RANDOMNUMBER", random);
                            objPar[8] = new SqlParameter("@P_FILENAME", filename);
                            objPar[9] = new SqlParameter("@P_IPADDRESS", apadd);
                            objPar[10] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                            objPar[10].Direction = ParameterDirection.Output;
                        };

                        object val = objSQL.ExecuteNonQuerySP("PKG_ACD_INS_BULK_PROGRAM_APPLY", objPar, true);

                        if (val != null)
                        {
                            if (val.ToString().Equals("-99"))
                                retv = -99;
                            else
                                retv = Convert.ToInt16(val.ToString());
                        }
                        else
                            retv = -99;
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLogicLayer.BusinessLogic.ImportDataForStudentREgistration.InsertToSqlDB-> " + ex.ToString());
                    }
                    return retv;
                }

                //ADDED BY AASHNA 05-12-2022
                public DataSet GetBulkProgramApply(string filename, string random)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_FILENAME", filename);
                        objParams[1] = new SqlParameter("@P_RANDOMNUMBER", random);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_BULK_PROGRAM_APPLY", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.BranchController.GetAllBranchTypes-> " + ex.ToString());
                    }
                    return ds;
                }

                //ADDED BY AASHNA 05-12-2022
                public int VerifyandMovedPgmApply(string RandomNo, string REGNO, string degreeno, string branchno)
                {
                    int ret = 0;
                    try
                    {
                        SQLHelper objSQL = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objPar = new SqlParameter[]
                        {
                           new SqlParameter("@P_RANDOMNO",RandomNo),
                           new SqlParameter("@P_REGNO",REGNO),
                           new SqlParameter("@P_NEW_DEGREENO",degreeno),
                           new SqlParameter("@P_NEWBRANCHNO",branchno),
                           new SqlParameter("@P_OUTPUT",SqlDbType.Int)
                        };
                        objPar[objPar.Length - 1].Direction = ParameterDirection.Output;
                        object val = objSQL.ExecuteNonQuerySP("PKG_ACD_UPD_BULK_PROGRAM_APPLY", objPar, true);
                        if (val != null)
                        {
                            if (val.ToString().Equals("-99"))
                                ret = -99;
                            else
                                ret = Convert.ToInt16(val.ToString());
                        }
                        else
                            ret = -99;
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLogicLayer.BusinessLogic.ImportDataController.VerifyandRegisterImportedCourseRegDataUG_Bulk-> " + ex.ToString());
                    }
                    return ret;
                }

                //ADDED BY AASHNA 06-12-2022
                public DataSet ConfirmprogramApplyData(int faculty, int ugpg, int semesterno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objPar = null;
                        {
                            objPar = new SqlParameter[3];
                            objPar[0] = new SqlParameter("@P_FACULTY", faculty);
                            objPar[1] = new SqlParameter("@P_UA_SECTION", ugpg);
                            objPar[2] = new SqlParameter("@P_SEMESTERNO", semesterno);
                        };
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_EXCEL_CONFIRMED_PROGRAM_APPLY_DATA", objPar);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLogicLayer.BusinessLogic.ImportCourseRegDataUG_Bulk.InsertToSqlDB-> " + ex.ToString());
                    }
                    return ds;
                }

                public int AddExamCenterandaptitutetest(User objUR, int MODE, string Exam_Date, string Time_Slot, int Venue_no, string Venue_name, int Special_need,
                                  string Reason, int scheduleno, int exam_dateno)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        //Add Branch change data
                        objParams = new SqlParameter[15];
                        objParams[0] = new SqlParameter("@P_USERNO", objUR.USERNO);
                        objParams[1] = new SqlParameter("@P_CAMPUSNO", objUR.Campus);
                        objParams[2] = new SqlParameter("@P_WEEKDAYS", objUR.WeekDays);
                        objParams[3] = new SqlParameter("@P_APPTITUTE_CENTER", objUR.AptitudeCenter);
                        objParams[4] = new SqlParameter("@P_APPTITUTE_MODE", MODE);
                        objParams[5] = new SqlParameter("@P_APPTITUTE_MEDIUM", objUR.AptitudeMedium);
                        objParams[6] = new SqlParameter("@P_EXAM_DATE", Exam_Date);
                        objParams[7] = new SqlParameter("@P_TIME_SLOT", Time_Slot);
                        objParams[8] = new SqlParameter("@P_VENUE_NO", Venue_no);
                        objParams[9] = new SqlParameter("@P_VENUE_NAME", Venue_name);
                        objParams[10] = new SqlParameter("@P_SPECIAL_NEED", Special_need);
                        objParams[11] = new SqlParameter("@P_REASON", Reason);
                        objParams[12] = new SqlParameter("@P_SCHEDULING_NO", scheduleno);
                        objParams[13] = new SqlParameter("@P_EXAMDATE_NO", exam_dateno);
                        objParams[14] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                        objParams[14].Direction = ParameterDirection.Output;

                        object ret = (objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INSERT_UPDATE_EXAM_CENTER", objParams, true));

                        if (ret.ToString() == "1" && ret != null)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else if (ret.ToString() == "2")
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        }
                        else if (ret.ToString() == "3")
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordDeleted);
                        }
                        else if (ret.ToString() == "-99")
                        {
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        }
                        else if (ret.ToString() == "2627")
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                        }
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.BranchController.AddBranchPreference-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int AddBranchPreference(User objUR, string CollegeId, String DegreeNo, string Branchno, string Area, string campusNos, string Admbatchs, string Branch_Pref, int uano)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        //Add Branch change data
                        objParams = new SqlParameter[13];
                        objParams[0] = new SqlParameter("@P_USERNO", objUR.USERNO);
                        objParams[1] = new SqlParameter("@P_PROGRAM_TYPE", objUR.UGPG);
                        objParams[2] = new SqlParameter("@P_AREAOFINTEREST", Area);
                        objParams[3] = new SqlParameter("@P_COLLEGE_ID", CollegeId);
                        objParams[4] = new SqlParameter("@P_DEGREENO", DegreeNo);
                        objParams[5] = new SqlParameter("@P_BRANCHNO", Branchno);
                        objParams[6] = new SqlParameter("@P_CAMPUSNO", objUR.Campus);
                        objParams[7] = new SqlParameter("@P_WEEKDAYS", objUR.WeekDays);
                        objParams[8] = new SqlParameter("@P_CAMPUSNOS", campusNos);
                        objParams[9] = new SqlParameter("@P_ADMBATCHS", Admbatchs);
                        objParams[10] = new SqlParameter("@P_BRANCH_PREF", Branch_Pref);
                        objParams[11] = new SqlParameter("@P_UANO  ", uano);
                        objParams[12] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                        objParams[12].Direction = ParameterDirection.Output;

                        object ret = (objSQLHelper.ExecuteNonQuerySP("PKG_USER_ADD_BRANCH_PREF_USA_ERP", objParams, true));

                        if (ret.ToString() == "1" && ret != null)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        if (ret.ToString() == "4" && ret != null)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordFound);
                        }
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.BranchController.AddBranchPreference-> " + ex.ToString());
                    }
                    return retStatus;
                }

                //added by aashna 08-05-2023
                public DataSet Getdetailsofbranch(int ugpg, int USERNO, int Campusno, int Discipline, int AFFILIATED_NO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_UGPG", ugpg);
                        objParams[1] = new SqlParameter("@P_USERNO", USERNO);
                        objParams[2] = new SqlParameter("@P_CAMPUSNO", Campusno);
                        objParams[3] = new SqlParameter("@P_DISCIPLINE", Discipline);
                        objParams[4] = new SqlParameter("@P_AFFILIATED", AFFILIATED_NO);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_UGPG_DATA_FORPROGRAM", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.BranchController.GetAllBranchDetails-> " + ex.ToString());
                    }
                    return ds;
                }
            }
        }
    }
}