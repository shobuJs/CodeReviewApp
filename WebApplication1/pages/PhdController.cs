using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessLogic
        {
            public class PhdController
            {
                private string _UAIMS_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
                private Common objcommon = new Common();

                public DataSet GETBulkPhdAnnexureA(int degreeno, int deptno, int Admbatch)
                {
                    DataSet ds = null;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_DEGEREENO", degreeno);
                        objParams[1] = new SqlParameter("@P_BRANCHNO", deptno);
                        objParams[2] = new SqlParameter("@P_ADMBATCH", Admbatch);

                        ds = objSQLHelper.ExecuteDataSetSP("GET_BULK_PHD_ANNEXURE_A_DETAILS", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.EmpCreateController.GetStudentListForIdentityCard-> " + ex.ToString());
                    }
                    return ds;
                }

                //====================== added by dipali  on 30072018 -- for  -- update bulk phd annexure A  --//

                public int UpdateBulkPhdAnnexureA(string IDNO, int admbatch)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_IDNO", IDNO);
                        objParams[1] = new SqlParameter("@P_ADMBATCH", admbatch);
                        objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[2].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_BULK_PHD_ANNEXURE_A_UPDATE", objParams, true);
                        retStatus = Convert.ToInt32(ret);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.UpdateStudRegStaus-> " + ex.ToString());
                    }
                    return retStatus;
                }

                //================= Added by dipali on 30072018 -- for  -- BULK PHD Annexure A-------//

                public DataSet GETBulkPhdCourseRegistration(int degreeno, int deptno, int Sessionno)
                {
                    DataSet ds = null;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_DEGEREENO", degreeno);
                        objParams[1] = new SqlParameter("@P_BRANCHNO", deptno);
                        objParams[2] = new SqlParameter("@P_SESSIONNO", Sessionno);

                        ds = objSQLHelper.ExecuteDataSetSP("GET_BULK_PHD_COURSE_REGISTRATION_DETAILS", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.EmpCreateController.GetStudentListForIdentityCard-> " + ex.ToString());
                    }
                    return ds;
                }

                //====================== added by dipali  on 30072018 -- for  -- update bulk phd annexure A  --//

                public int UpdateBulkPhdCourseRegistration(string IDNO, int sessionno)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_IDNO", IDNO);
                        objParams[1] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[2].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_BULK_PHD_COURSE_REGISTRATION_UPDATE", objParams, true);
                        retStatus = Convert.ToInt32(ret);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.UpdateStudRegStaus-> " + ex.ToString());
                    }
                    return retStatus;
                }

                //====================== PHD BUlk Semester pramotion ==================09082018 ===//

                public DataSet GETBulkPhdSemesterPramotion(int sessiono, int degreeno, int branchno, int schemeno, int semesterno)
                {
                    DataSet ds = null;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessiono);
                        objParams[1] = new SqlParameter("@P_DEGEREENO", degreeno);
                        objParams[2] = new SqlParameter("@P_BRANCHNO", branchno);
                        objParams[3] = new SqlParameter("@P_SCHEMENO  ", schemeno);
                        objParams[4] = new SqlParameter("@P_SEMESTERNO", semesterno);
                        ds = objSQLHelper.ExecuteDataSetSP("GET_BULK_PHD_SEMESTER_PRAMOTION", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.EmpCreateController.GetStudentListForIdentityCard-> " + ex.ToString());
                    }
                    return ds;
                }

                public int UpdateBulkPhdSemesterPramotion(string IDNO, int semester, int upsem, int Sessionno, int schemeno, int degreeno, string coursenos)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[8];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", Sessionno);
                        objParams[1] = new SqlParameter("@P_SEMESTERNO", semester);
                        objParams[2] = new SqlParameter("@P_UPSEM", upsem);
                        objParams[3] = new SqlParameter("@P_SCHEMENO", schemeno);
                        objParams[4] = new SqlParameter("@P_DEGREENO", degreeno);
                        objParams[5] = new SqlParameter("@P_COURSENO", coursenos);
                        objParams[6] = new SqlParameter("@P_IDNO", IDNO);
                        objParams[7] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[7].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_BULK_PHD_SEMESTER_PROMOTION_UPDATE", objParams, true);
                        retStatus = Convert.ToInt32(ret);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.UpdateStudRegStaus-> " + ex.ToString());
                    }
                    return retStatus;
                }

                //-------------------------PHD ADMISSION STUDENT LIST EXCEL  --- 26072018-----------//
                public DataSet RetrievePhdStudentAdmittedlistExcel(int degreeno, int branchno, int userno, int admbatch)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_DEGREENO", degreeno);
                        objParams[1] = new SqlParameter("@P_USERNO", userno);
                        objParams[2] = new SqlParameter("@P_BRANCH", branchno);
                        objParams[3] = new SqlParameter("@P_ADMBATCH", admbatch);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_PHD_ADMITTED_STUDENT_LIST_EXCEL", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.RetrieveStudentFeesDetails-> " + ex.ToString());
                    }
                    return ds;
                }

                ///------------------Added by dipali on 27072018 -- for  -- BULK PHD STUDENT PROGRESS -------//

                public DataSet GETBulkStudProgress(int degreeno, int deptno, int Sessionno)
                {
                    DataSet ds = null;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_DEGEREENO", degreeno);
                        objParams[1] = new SqlParameter("@P_BRANCHNO", deptno);
                        objParams[2] = new SqlParameter("@P_SESSIONNO", Sessionno);

                        ds = objSQLHelper.ExecuteDataSetSP("GET_BULK_PHD_STUDENT_PROGRESS_DETAILS", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.EmpCreateController.GetStudentListForIdentityCard-> " + ex.ToString());
                    }
                    return ds;
                }

                //====================== added by dipali  on 27072018 -- for  -- update bulk progress status  --//

                public int UpdateBulkPhdProgresStudent(string IDNO, int sessionno)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_IDNO", IDNO);
                        objParams[1] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[2].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_BULK_PHD_STUDENT_PROGRESS_UPDATE", objParams, true);
                        retStatus = Convert.ToInt32(ret);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.UpdateStudRegStaus-> " + ex.ToString());
                    }
                    return retStatus;
                }

                //====================  Added by dipali on 02072018 for Phd degree Completed -----------//

                public DataTableReader GetPhdDegreeCompletedstudent(int idno)
                {
                    DataTableReader dtr = null;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);

                        dtr = objSQLHelper.ExecuteDataSetSP("PKG_PHD_GET_DEGREE_AWARD_DETAILS", objParams).Tables[0].CreateDataReader();
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentDetails->" + ex.ToString());
                    }
                    return dtr;
                }

                //----------------------------------------------
                public string UpdatePhdDegreeCompletedStudentdetails(Student objStudent)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    //int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        //Update Student
                        objParams = new SqlParameter[13];
                        //First Add Student Parameter
                        objParams[0] = new SqlParameter("@P_IDNO", objStudent.IdNo);
                        objParams[1] = new SqlParameter("@P_THESIS_TITLE_HINDI", objStudent.ThesisTitleHindi);
                        objParams[2] = new SqlParameter("@P_THESISTITLE", objStudent.ThesisTitle);
                        objParams[3] = new SqlParameter("@P_DESCIPLINE", objStudent.Descipline);
                        objParams[4] = new SqlParameter("@P_DEGREE_AMT", objStudent.PhddegreeTotalAmount);
                        objParams[5] = new SqlParameter("@P_FEES_NO", objStudent.PhdFeesRef);
                        objParams[6] = new SqlParameter("@P_PASSOUT_YEAR", objStudent.PhdPassoutyear);
                        objParams[7] = new SqlParameter("@P_CONVOCATION_YEAR", objStudent.PhdConvocationyear);
                        objParams[8] = new SqlParameter("@P_CONVOCATION_DATE", objStudent.PhdConvocationDate);
                        objParams[9] = new SqlParameter("@P_DEGREE_AWARD_REMARK", objStudent.PhdDegreeRemark);
                        objParams[10] = new SqlParameter("@P_DEGREE_APPROVAL_NO", objStudent.Uano);
                        objParams[11] = new SqlParameter("@P_STUDNAME_HINDI", objStudent.StudNameHindi);
                        objParams[12] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[12].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PHD_DEGREE_COMPLETED_UPDATE", objParams, true);
                        return ret.ToString();
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.UpdateStudent-> " + ex.ToString());
                    }
                }

                //-----------------------------------

                public DataSet RetrievePhdDegreeAwardDetailsExcel(int idno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PHD_GET_DEGREE_AWARD_EXCEL", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.RetrieveStudentFeesDetails-> " + ex.ToString());
                    }
                    return ds;
                }

                //===============Annexure changes status report -----06082018----//

                public DataSet RetrievePhdAnnexureAStatusDetails(int degreeno, int branchno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_DEGREENO", degreeno);
                        objParams[1] = new SqlParameter("@P_BRANCH", branchno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_PHD_ANNEXURE_STATUS_REPORT", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.RetrieveStudentFeesDetails-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet RetrievePhdAnnexureAStatusExcel(int degreeno, int branchno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_DEGREENO", degreeno);
                        objParams[1] = new SqlParameter("@P_BRANCH", branchno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_PHD_ANNEXURE_STATUS_REPORT_EXCEL", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.RetrieveStudentFeesDetails-> " + ex.ToString());
                    }
                    return ds;
                }

                //====================== PHD BUlk Semester pramotion ==================09082018 ===//

                //=================== Phd progress for student and faculty ========================//

                public DataSet GETPhdProgressStudFaculty(int idno)
                {
                    DataSet ds = null;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);

                        ds = objSQLHelper.ExecuteDataSetSP("GET_PHD_PROGRESS_STUD_FACULTY_DETAILS", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.EmpCreateController.GetStudentListForIdentityCard-> " + ex.ToString());
                    }
                    return ds;
                }

                // ---=======================-  get data for phd Defence report -- addde by dipali on  28082018-============================- //

                public DataTableReader GetStudentPhdDefenceDetails(int idno)
                {
                    DataTableReader dtr = null;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);

                        dtr = objSQLHelper.ExecuteDataSetSP("PKG_PHD_STUDENT_SP_RET_STUDENT_BYID_ANNEXURE_F", objParams).Tables[0].CreateDataReader();
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentDetails->" + ex.ToString());
                    }
                    return dtr;
                }

                //--===============  Phd Examiner Master -- added by dipali  on 22082018 ---==============================//

                public DataSet GETPhdExaminerDetails(int usertype)
                {
                    DataSet ds = null;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_USERTYPE", usertype);
                        ds = objSQLHelper.ExecuteDataSetSP("GET_PHD_EXAMINER_MASTER_DETAILS", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.studentController.GET_PHD_EXAMINER_MASTER_DETAILS-> " + ex.ToString());
                    }
                    return ds;
                }

                //  --- modify -- on 16112018
                public string AddPhdExaminerDetails(Student objstudentinfo, int status)
                {
                    string retStatus = string.Empty;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        //Add New student
                        objParams = new SqlParameter[13];
                        objParams[0] = new SqlParameter("@P_IDNO", objstudentinfo.IdNo);
                        objParams[1] = new SqlParameter("@P_NAME", objstudentinfo.StudName);
                        objParams[2] = new SqlParameter("@P_ADDRESS", objstudentinfo.PAddress);
                        objParams[3] = new SqlParameter("@P_MOBILE", objstudentinfo.StudentMobile);
                        objParams[4] = new SqlParameter("@P_CONTACT", objstudentinfo.PMobile);
                        objParams[5] = new SqlParameter("@P_EMAILID", objstudentinfo.EmailID);
                        objParams[6] = new SqlParameter("@P_EXTERNAL", objstudentinfo.NADID);

                        objParams[7] = new SqlParameter("@P_OTHERMOBILE", objstudentinfo.FatherMobile);  //---add other mobile ,contact no --16112018
                        objParams[8] = new SqlParameter("@P_OTHERCONTACT", objstudentinfo.MotherMobile);
                        objParams[9] = new SqlParameter("@P_UANO", objstudentinfo.Uano);
                        objParams[10] = new SqlParameter("@P_IPADDRESS", objstudentinfo.IPADDRESS);
                        objParams[11] = new SqlParameter("@P_STATUS", status);

                        objParams[12] = new SqlParameter("@P_OUT", SqlDbType.Int, 25);
                        objParams[12].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_PHD_EXAMINER_MASTER", objParams, true);

                        //if (ret.ToString().Equals("-99"))
                        //    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        //else
                        //    retStatus = Convert.ToInt32(ret);
                        retStatus = ret.ToString();
                    }
                    catch (Exception ex)
                    {
                        retStatus = "0";
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.AddStudentDetailsForProvisionalDegree-> " + ex.ToString());
                    }

                    return retStatus;
                }

                public DataSet GETPhdExaminerDetailsIDNO(int idno)
                {
                    DataSet ds = null;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        ds = objSQLHelper.ExecuteDataSetSP("GET_PHD_EXAMINER_MASTER_DETAILS_IDNO", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.studentController.GET_PHD_EXAMINER_MASTER_DETAILS-> " + ex.ToString());
                    }
                    return ds;
                }

                //--------------Phd Examiner Details -------------//

                public DataTableReader GetExaminerPHDDetails(int idno)
                {
                    DataTableReader dtr = null;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);

                        dtr = objSQLHelper.ExecuteDataSetSP("PKG_PHD_STUDENT_SP_EXAMINER_DETAILS", objParams).Tables[0].CreateDataReader();
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentDetails->" + ex.ToString());
                    }
                    return dtr;
                }

                //----- Supervisior add examiner details ------//

                public string AddPhdExaminerDetailsSupervisor(Student objstudentinfo)
                {
                    string retStatus = string.Empty;
                    try
                    {
                        //           @P_IDNO,@P_SUPERVISORNO,1,@P_DRCCHAIRMANNO,@P_EXAMINER1,@P_EXAMINER2,@P_EXAMINER3
                        //,@P_EXAMINER4,@P_EXAMINER5,@P_EXAMINER6,@P_EXAMINER7,@P_EXAMINER8,@P_EXAMINER9,@P_EXAMINER10,

                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[20];
                        objParams[0] = new SqlParameter("@P_IDNO", objstudentinfo.IdNo);
                        objParams[1] = new SqlParameter("@P_SUPERVISORNO", objstudentinfo.PhdSupervisorNo);
                        objParams[2] = new SqlParameter("@P_DRCCHAIRMANNO", objstudentinfo.DrcChairNo);
                        objParams[3] = new SqlParameter("@P_EXAMINER1", objstudentinfo.PhdExaminer1);
                        objParams[4] = new SqlParameter("@P_EXAMINER2", objstudentinfo.PhdExaminer2);
                        objParams[5] = new SqlParameter("@P_EXAMINER3", objstudentinfo.PhdExaminer3);
                        objParams[6] = new SqlParameter("@P_EXAMINER4", objstudentinfo.PhdExaminer4);
                        objParams[7] = new SqlParameter("@P_EXAMINER5", objstudentinfo.PhdExaminer5);
                        objParams[8] = new SqlParameter("@P_EXAMINER6", objstudentinfo.PhdExaminer6);
                        objParams[9] = new SqlParameter("@P_EXAMINER7", objstudentinfo.PhdExaminer7);
                        objParams[10] = new SqlParameter("@P_EXAMINER8", objstudentinfo.PhdExaminer8);
                        objParams[11] = new SqlParameter("@P_EXAMINER9", objstudentinfo.PhdExaminer9);
                        objParams[12] = new SqlParameter("@P_EXAMINER10", objstudentinfo.PhdExaminer10);
                        objParams[13] = new SqlParameter("@P_SYNNAME", objstudentinfo.PhdSynName);
                        objParams[14] = new SqlParameter("@P_SYNFILE", objstudentinfo.PhdSynFile);
                        objParams[15] = new SqlParameter("@P_JOINTSUPNO", objstudentinfo.JoinsupervisorNo);
                        objParams[16] = new SqlParameter("@P_PRESYNDATE", objstudentinfo.PhdPresyndate);
                        objParams[17] = new SqlParameter("@P_PRESYNNAME", objstudentinfo.PhdPreSynName);
                        objParams[18] = new SqlParameter("@P_PRESYNFILE", objstudentinfo.PhdPreSynFile);
                        objParams[19] = new SqlParameter("@P_OUT", SqlDbType.Int, 25);
                        objParams[19].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_PHD_EXAMINER_DETAILS_ADD_BY_SUPERVISIOR", objParams, true);

                        //if (ret.ToString().Equals("-99"))
                        //    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        //else
                        //    retStatus = Convert.ToInt32(ret);
                        retStatus = ret.ToString();
                    }
                    catch (Exception ex)
                    {
                        retStatus = "0";
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.AddStudentDetailsForProvisionalDegree-> " + ex.ToString());
                    }

                    return retStatus;
                }

                //--------- phd examiner drc & dean Confirmation -------//
                public string UpdateExaminerStatus(Student objStudent)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        //Update drc
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_IDNO", objStudent.IdNo);
                        objParams[1] = new SqlParameter("@P_STATUS", objStudent.PhdStatusValue);
                        objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[2].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_PHD_EXAMINER_UPDATE", objParams, true);
                        retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        return ret.ToString();
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.UpdateStudent-> " + ex.ToString());
                    }
                }

                //-------- phd dean reject supervisior -----------//
                public string RejectDeanStatus(Student objStudent, string Remark)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        //Update drc
                        objParams = new SqlParameter[3];

                        objParams[0] = new SqlParameter("@P_IDNO", objStudent.IdNo);
                        objParams[1] = new SqlParameter("@P_REMARK", Remark);
                        objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[2].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_PHD_EXAMINER_DEAN_REJECT", objParams, true);

                        retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        return ret.ToString();
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.RejectSupervisorStatus-> " + ex.ToString());
                    }
                }

                //--------------------------------------------------
                public string UpdatePhdDeanApproval(Student objStudent)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        //Update drc
                        objParams = new SqlParameter[12];
                        objParams[0] = new SqlParameter("@P_IDNO", objStudent.IdNo);
                        objParams[1] = new SqlParameter("@P_Examiner1Status", objStudent.PhdExaminer1Status);
                        objParams[2] = new SqlParameter("@P_Examiner2Status", objStudent.PhdExaminer2Status);
                        objParams[3] = new SqlParameter("@P_Examiner3Status", objStudent.PhdExaminer3Status);
                        objParams[4] = new SqlParameter("@P_Examiner4Status", objStudent.PhdExaminer4Status);
                        objParams[5] = new SqlParameter("@P_Examiner5Status", objStudent.PhdExaminer5Status);
                        objParams[6] = new SqlParameter("@P_Examiner6Status", objStudent.PhdExaminer6Status);
                        objParams[7] = new SqlParameter("@P_Examiner7Status", objStudent.PhdExaminer7Status);
                        objParams[8] = new SqlParameter("@P_Examiner8Status", objStudent.PhdExaminer8Status);
                        objParams[9] = new SqlParameter("@P_Examiner9Status", objStudent.PhdExaminer9Status);
                        objParams[10] = new SqlParameter("@P_Examiner10Status", objStudent.PhdExaminer10Status);
                        objParams[11] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[11].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_PHD_EXAMINER_DEAN_APPROVAL", objParams, true);
                        retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        return ret.ToString();
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.UpdateStudent-> " + ex.ToString());
                    }
                }

                //-------------phd- supervisior approval  viva_voice -- //
                public string UpdatePhdSupervisiorVivaApproval(Student objStudent)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        //Update drc
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_IDNO", objStudent.IdNo);
                        objParams[1] = new SqlParameter("@P_Examiner1Status", objStudent.PhdExaminer1Status);
                        objParams[2] = new SqlParameter("@P_VIVADATE", objStudent.YearOfExam);
                        objParams[3] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[3].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_PHD_EXAMINER_SUPERVISIOR_VIVA_APPROVAL", objParams, true);
                        retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        return ret.ToString();
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.UpdateStudent-> " + ex.ToString());
                    }
                }

                public DataSet RetrieveStudentDetailsPHD(string search, string category, string branchno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_SEARCHSTRING", search);
                        objParams[1] = new SqlParameter("@P_SEARCH", category);
                        objParams[2] = new SqlParameter("@P_BRANCHNO", branchno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STUDENT_SP_SEARCH_PHD_STUDENT", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.EmpCreateController.RetrieveEmpDetails-> " + ex.ToString());
                    }
                    return ds;
                }

                //======================== dean external Examiner confirmation  ====================//

                public string UpdatePhdDeanVivaApproval(Student objStudent)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        //Update drc
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_IDNO", objStudent.IdNo);
                        objParams[1] = new SqlParameter("@P_Examiner1Status", objStudent.PhdExaminer1Status);
                        objParams[2] = new SqlParameter("@P_VIVADATE", objStudent.YearOfExam);
                        objParams[3] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[3].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_PHD_EXAMINER_DEAN_VIVA_APPROVAL", objParams, true);
                        retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        return ret.ToString();
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.UpdateStudent-> " + ex.ToString());
                    }
                }

                //===============  Phd OutSide Member Master -- added by dipali  on 08012019 ---==============================//

                public DataSet GETPhdOutsideMemberDetails()
                {
                    DataSet ds = null;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("GET_PHD_OUTSIDE_MEMBER_MASTER_DETAILS", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.studentController.GET_PHD_EXAMINER_MASTER_DETAILS-> " + ex.ToString());
                    }
                    return ds;
                }

                //  --- modify -- on 16112018
                public string AddPhdOutsideMemberDetails(Student objstudentinfo)
                {
                    string retStatus = string.Empty;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        //Add New student
                        objParams = new SqlParameter[11];
                        objParams[0] = new SqlParameter("@P_IDNO", objstudentinfo.IdNo);
                        objParams[1] = new SqlParameter("@P_NAME", objstudentinfo.StudName);
                        objParams[2] = new SqlParameter("@P_ADDRESS", objstudentinfo.PAddress);
                        objParams[3] = new SqlParameter("@P_MOBILE", objstudentinfo.StudentMobile);
                        objParams[4] = new SqlParameter("@P_CONTACT", objstudentinfo.PMobile);
                        objParams[5] = new SqlParameter("@P_EMAILID", objstudentinfo.EmailID);
                        objParams[6] = new SqlParameter("@P_DESIGNATION", objstudentinfo.PhdDegreeRemark);
                        objParams[7] = new SqlParameter("@P_BRANCHNO", objstudentinfo.BranchNo);
                        objParams[8] = new SqlParameter("@P_UANO", objstudentinfo.Uano);
                        objParams[9] = new SqlParameter("@P_IPADDRESS", objstudentinfo.IPADDRESS);
                        objParams[10] = new SqlParameter("@P_OUT", SqlDbType.Int, 25);
                        objParams[10].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_PHD_OUTSIDE_MEMBER_MASTER", objParams, true);

                        retStatus = ret.ToString();
                    }
                    catch (Exception ex)
                    {
                        retStatus = "0";
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.AddStudentDetailsForProvisionalDegree-> " + ex.ToString());
                    }

                    return retStatus;
                }

                public DataSet GETPhdOutsideMemberDetailsIDNO(int idno)
                {
                    DataSet ds = null;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        ds = objSQLHelper.ExecuteDataSetSP("GET_PHD_OUTSIDE_MEMBER_MASTER_DETAILS_IDNO", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.studentController.GET_PHD_EXAMINER_MASTER_DETAILS-> " + ex.ToString());
                    }
                    return ds;
                }

                // =============  Bulk Drc Chairman Update  ===================//
                public DataSet GETPhdDrcChairmanOld(int degreeno, int department)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_DEGREENO", degreeno);
                        objParams[1] = new SqlParameter("@P_DEPTNO", department);
                        ds = objSQLHelper.ExecuteDataSetSP("GET_PHD_DRCCHAIRMAN_DETAILS_OLD", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.studentController.GET_PHD_EXAMINER_MASTER_DETAILS-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GETPhdDrcChairmanNew(int degreeno, int department)
                {
                    DataSet ds = null;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_DEGREENO", degreeno);
                        objParams[1] = new SqlParameter("@P_DEPTNO", department);
                        ds = objSQLHelper.ExecuteDataSetSP("GET_PHD_DRCCHAIRMAN_DETAILS_NEW", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.studentController.GET_PHD_EXAMINER_MASTER_DETAILS-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GETPhdDrcChairmanUpdateIdno(int idno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_UANO", idno);
                        ds = objSQLHelper.ExecuteDataSetSP("GET_PHD_DRCCHAIRMAN_DETAILS_IDNO", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.studentController.GET_PHD_EXAMINER_MASTER_DETAILS-> " + ex.ToString());
                    }
                    return ds;
                }

                public string UpdatePhdDrcChairmanStatus(Student objstudentinfo, string actionn)
                {
                    string retStatus = string.Empty;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        //Add New student
                        objParams = new SqlParameter[7];
                        objParams[0] = new SqlParameter("@P_UA_NO", objstudentinfo.IdNo);
                        objParams[1] = new SqlParameter("@P_DRCSTATUS", objstudentinfo.SuperRole);
                        objParams[2] = new SqlParameter("@P_UANO", objstudentinfo.Uano);
                        objParams[3] = new SqlParameter("@P_IPADDRESS", objstudentinfo.IPADDRESS);
                        objParams[4] = new SqlParameter("@P_ACTIONN", actionn);
                        objParams[5] = new SqlParameter("@P_DEPTNO", objstudentinfo.BranchNo);
                        objParams[6] = new SqlParameter("@P_OUT", SqlDbType.Int, 25);
                        objParams[6].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_UPD_PHD_DRC_CHAIRMAIN_DETAILS", objParams, true);

                        retStatus = ret.ToString();
                    }
                    catch (Exception ex)
                    {
                        retStatus = "0";
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.AddStudentDetailsForProvisionalDegree-> " + ex.ToString());
                    }

                    return retStatus;
                }

                //=========================== PHD ANNEXURE B ELIGIBLITY ================================//

                public DataTableReader GetStudentPHDDetailsAnnexureBEligibility(int idno)
                {
                    DataTableReader dtr = null;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);

                        dtr = objSQLHelper.ExecuteDataSetSP("PKG_PHD_STUDENT_ANNEXURE_B_ELIGIBILITY_DETAILS", objParams).Tables[0].CreateDataReader();
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentDetails->" + ex.ToString());
                    }
                    return dtr;
                }

                public string UpdatePHDStudentAnnexureB_Eligibility(Student objStudent, string Action)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    //int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        //Update Student Details
                        objParams = new SqlParameter[5];
                        //First Add Student Parameter
                        objParams[0] = new SqlParameter("@P_IDNO", objStudent.IdNo);
                        objParams[1] = new SqlParameter("@P_ACTION", Action);
                        objParams[2] = new SqlParameter("@P_UANO", objStudent.Uano);
                        objParams[3] = new SqlParameter("@P_IPADDRESS", objStudent.IPADDRESS);
                        objParams[4] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[4].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PHD_STUD_SP_UPD_STUDENT_ANNEXURE_B_ELIGIBILITY", objParams, true);

                        retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        return ret.ToString();
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.UpdateStudent-> " + ex.ToString());
                    }
                }

                //   ============================THESIS SUBMITION FOR DEAN ==============================//

                public DataTableReader GetStudentPhdThesisDetails(int idno)
                {
                    DataTableReader dtr = null;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);

                        dtr = objSQLHelper.ExecuteDataSetSP("PKG_PHD_STUDENT_THESIS_DETAILS", objParams).Tables[0].CreateDataReader();
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentDetails->" + ex.ToString());
                    }
                    return dtr;
                }

                public string UpdatePhdThesisSubmissionDate(Student objStudent)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    //int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        //Update Student Details
                        objParams = new SqlParameter[5];
                        //First Add Student Parameter
                        objParams[0] = new SqlParameter("@P_IDNO", objStudent.IdNo);
                        objParams[1] = new SqlParameter("@P_THS_SUBDATE", objStudent.ApprovedDate);
                        objParams[2] = new SqlParameter("@P_UANO", objStudent.Uano);
                        objParams[3] = new SqlParameter("@P_IPADDRESS", objStudent.IPADDRESS);
                        objParams[4] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[4].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PHD_STUD_SP_UPD_STUDENT_THESIS_DETAILS", objParams, true);

                        retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        return ret.ToString();
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.UpdateStudent-> " + ex.ToString());
                    }
                }

                //---------------------------  Phd Unsatisfactory progress---------------------------------------------------
                public DataSet GETBulkStudProgressOneUnsatisfactory(int degreeno, int deptno, int Sessionno)
                {
                    DataSet ds = null;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_DEGEREENO", degreeno);
                        objParams[1] = new SqlParameter("@P_BRANCHNO", deptno);
                        objParams[2] = new SqlParameter("@P_SESSIONNO", Sessionno);

                        ds = objSQLHelper.ExecuteDataSetSP("GET_BULK_PHD_STUDENT_PROGRESS_DETAILS_ONE_UNSATISFACTORY", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.EmpCreateController.GetStudentListForIdentityCard-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GETBulkStudProgressTwoUnsatisfactory(int degreeno, int deptno, int Sessionno)
                {
                    DataSet ds = null;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_DEGEREENO", degreeno);
                        objParams[1] = new SqlParameter("@P_BRANCHNO", deptno);
                        objParams[2] = new SqlParameter("@P_SESSIONNO", Sessionno);

                        ds = objSQLHelper.ExecuteDataSetSP("GET_BULK_PHD_STUDENT_PROGRESS_DETAILS_TWO_UNSATISFACTORY", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.EmpCreateController.GetStudentListForIdentityCard-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GETBulkStudProgressSatisfactoryExcel(int degreeno, int deptno, int Sessionno, int action)
                {
                    DataSet ds = null;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_DEGEREENO", degreeno);
                        objParams[1] = new SqlParameter("@P_BRANCHNO", deptno);
                        objParams[2] = new SqlParameter("@P_SESSIONNO", Sessionno);
                        objParams[3] = new SqlParameter("@P_ACTION", action);
                        ds = objSQLHelper.ExecuteDataSetSP("GET_PHD_STUDENT_PROGRESS_DETAILS_EXCEL", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.EmpCreateController.GetStudentListForIdentityCard-> " + ex.ToString());
                    }
                    return ds;
                }

                //--------------------PHD EXTEND  COMPHRENSIVE AND THESIS DATE------------------------------------//

                public DataTableReader GetPHDExtendedDateDetails(int idno)
                {
                    DataTableReader dtr = null;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);

                        dtr = objSQLHelper.ExecuteDataSetSP("PKG_PHD_STUDENT_EXTEND_DATE_DETAILS", objParams).Tables[0].CreateDataReader();
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentDetails->" + ex.ToString());
                    }
                    return dtr;
                }

                public int UpdatePhdExtendedDate(Student objStudent, string Action, string file, System.Web.UI.WebControls.FileUpload fuFile)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        bool flag = true;
                        string uploadPath = HttpContext.Current.Server.MapPath("~/PHD_FILES/");

                        //Upload the File
                        if (!fuFile.FileName.Equals(string.Empty))
                        {
                            if (System.IO.File.Exists(uploadPath + file))
                            {
                                //lblStatus.Text = "File already exists. Please upload another file or rename and upload.";
                                return Convert.ToInt32(CustomStatus.FileExists);
                            }
                            else
                            {
                                string uploadFile = System.IO.Path.GetFileName(fuFile.PostedFile.FileName);
                                fuFile.PostedFile.SaveAs(uploadPath + file);
                                flag = true;
                            }
                        }

                        if (flag == true)
                        {
                            objParams = new SqlParameter[7];

                            //First Add Student Parameter
                            objParams[0] = new SqlParameter("@P_IDNO", objStudent.IdNo);
                            objParams[1] = new SqlParameter("@P_THS_SUBDATE", objStudent.ApprovedDate);
                            objParams[2] = new SqlParameter("@P_UANO", objStudent.Uano);
                            objParams[3] = new SqlParameter("@P_IPADDRESS", objStudent.IPADDRESS);
                            objParams[4] = new SqlParameter("@P_ACTION", Action);
                            objParams[5] = new SqlParameter("@P_LINK", file);
                            objParams[6] = new SqlParameter("@P_OUT", SqlDbType.Int);
                            objParams[6].Direction = ParameterDirection.Output;

                            object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PHD_UPD_STUDENT_EXTENDED_DETAILS", objParams, true);

                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                            return Convert.ToInt32(ret);
                        }
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.NewsController.Add-> " + ex.ToString());
                    }
                    return retStatus;
                }

                ///---Added for Get new ReceiptNO ----22112017
                public DataSet GetNewReceiptData(string modeOfReceipt, string receipt_code)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objDataAccess = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_MODE_OF_RECEIPT", modeOfReceipt),
                    new SqlParameter("@P_RECEIPT_CODE", receipt_code),
                };
                        ds = objDataAccess.ExecuteDataSetSP("PKG_FEECOLLECT_NEW_RECEIPT_DATA_CONVOCATION", sqlParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.GetNewReceiptData() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }

                ///---add for INSERT-- convocation-- DCR  -- 22112017
                public int InsertPhdThesisDCR(FeeDemand objEntityClass)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[10];

                        objParams[0] = new SqlParameter("@P_SESSION_NO", objEntityClass.SessionNo);
                        objParams[1] = new SqlParameter("@P_IDNO", objEntityClass.StudentId);
                        objParams[2] = new SqlParameter("@P_ENROLLNO", objEntityClass.EnrollmentNo);
                        objParams[3] = new SqlParameter("@P_RECIEPTCODE", objEntityClass.ReceiptTypeCode);
                        objParams[4] = new SqlParameter("@P_SEMESTERNO", objEntityClass.SemesterNo);
                        objParams[5] = new SqlParameter("@P_PAYMENTTYPE", objEntityClass.PaymentTypeNo);
                        objParams[6] = new SqlParameter("@P_UA_NO", objEntityClass.UserNo);
                        objParams[7] = new SqlParameter("@P_RECIEPTNO", objEntityClass.Remark);
                        objParams[8] = new SqlParameter("@P_COUNTER_NO", objEntityClass.CounterNo);
                        objParams[9] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                        objParams[9].Direction = ParameterDirection.Output;

                        int ret = Convert.ToInt32(objSQLHelper.ExecuteNonQuerySP("PKG_CREATE_DCR_FOR_PHD_THESIS", objParams, true));
                        if (ret == 1)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.MarksEntryController.InsertStudentMarks() --> " + ex.Message + " " + ex.StackTrace);
                    }

                    return retStatus;
                }

                ///-------------------------------

                ///---Added for INSERT --Thesis-- DEMAND--22112017
                public int InsertPhdThesisDemand(FeeDemand objEntityClass)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[9];
                        objParams[0] = new SqlParameter("@P_SESSION_NO", objEntityClass.SessionNo);
                        objParams[1] = new SqlParameter("@P_IDNO", objEntityClass.StudentId);
                        objParams[2] = new SqlParameter("@P_ENROLLNO", objEntityClass.EnrollmentNo);
                        objParams[3] = new SqlParameter("@P_RECIEPTCODE", objEntityClass.ReceiptTypeCode);
                        objParams[4] = new SqlParameter("@P_SEMESTERNO", objEntityClass.SemesterNo);
                        objParams[5] = new SqlParameter("@P_PAYMENTTYPE", objEntityClass.PaymentTypeNo);
                        objParams[6] = new SqlParameter("@P_UA_NO", objEntityClass.CounterNo);
                        objParams[7] = new SqlParameter("@P_COUNTER_NO", objEntityClass.FeeCatNo);
                        objParams[8] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                        objParams[8].Direction = ParameterDirection.Output;

                        int ret = Convert.ToInt32(objSQLHelper.ExecuteNonQuerySP("PKG_CREATE_DEMAND_FOR_PHD_THESIS", objParams, true));
                        if (ret == 1)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.MarksEntryController.InsertStudentMarks() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return retStatus;
                }

                //-- Phd student details--26032018--//
                public DataSet GetPhdAnnexureBEligibilityDetailsExcel(int userno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_UANO", userno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_PHD_ANNEXURE_BELIGIBILITY_DETAIL_EXCEL", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.RetrieveStudentFeesDetails-> " + ex.ToString());
                    }
                    return ds;
                }

                //-----------------------   Phd Examiner Tracker  05032019----------------------------------------------//

                public DataTableReader GetPHDTrackerDetails(int idno)
                {
                    DataTableReader dtr = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);

                        dtr = objSQLHelper.ExecuteDataSetSP("PKG_PHD_STUDENT_SP_TRACKER_DETAILS", objParams).Tables[0].CreateDataReader();
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentDetails->" + ex.ToString());
                    }
                    return dtr;
                }

                // -----  Submit phd examiner details ---
                public string SubmitPhdExaminerDetails(Student objStudent)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[20];
                        objParams[0] = new SqlParameter("@P_IDNO", objStudent.IdNo);
                        objParams[1] = new SqlParameter("@P_Examiner1", objStudent.PhdExaminer1);
                        objParams[2] = new SqlParameter("@P_Examiner2", objStudent.PhdExaminer2);
                        objParams[3] = new SqlParameter("@P_Examiner3", objStudent.PhdExaminer3);
                        objParams[4] = new SqlParameter("@P_Examiner4", objStudent.PhdExaminer4);
                        objParams[5] = new SqlParameter("@P_Examiner1Status", objStudent.PhdExaminer1Status);
                        objParams[6] = new SqlParameter("@P_Examiner2Status", objStudent.PhdExaminer2Status);
                        objParams[7] = new SqlParameter("@P_Examiner3Status", objStudent.PhdExaminer3Status);
                        objParams[8] = new SqlParameter("@P_Examiner4Status", objStudent.PhdExaminer4Status);

                        objParams[9] = new SqlParameter("@P_ExaminerFile1", objStudent.PhdExaminerFile1);
                        objParams[10] = new SqlParameter("@P_ExaminerFile2", objStudent.PhdExaminerFile2);
                        objParams[11] = new SqlParameter("@P_ExaminerFile3", objStudent.PhdExaminerFile3);
                        objParams[12] = new SqlParameter("@P_ExaminerFile4", objStudent.PhdExaminerFile4);

                        objParams[13] = new SqlParameter("@P_PRIORITY_EX1", objStudent.PhdPriExaminer1);
                        objParams[14] = new SqlParameter("@P_PRIORITY_EX2", objStudent.PhdPriExaminer2);
                        objParams[15] = new SqlParameter("@P_PRIORITY_EX3", objStudent.PhdPriExaminer3);
                        objParams[16] = new SqlParameter("@P_PRIORITY_EX4", objStudent.PhdPriExaminer4);

                        objParams[17] = new SqlParameter("@P_UANO", objStudent.Uano);
                        objParams[18] = new SqlParameter("@P_IPADDRESS", objStudent.IPADDRESS);
                        objParams[19] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[19].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_PHD_INS_EXAMINER_DETAILS", objParams, true);
                        retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        return ret.ToString();
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.UpdateStudent-> " + ex.ToString());
                    }
                }

                // -----  Reject phd examiner details ---
                public string RejectPhdExaminerDetails(Student objStudent, string status)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[10];
                        objParams[0] = new SqlParameter("@P_IDNO", objStudent.IdNo);
                        objParams[1] = new SqlParameter("@P_Examiner1Status", objStudent.PhdExaminer1Status);
                        objParams[2] = new SqlParameter("@P_Examiner2Status", objStudent.PhdExaminer2Status);
                        objParams[3] = new SqlParameter("@P_Examiner3Status", objStudent.PhdExaminer3Status);
                        objParams[4] = new SqlParameter("@P_Examiner4Status", objStudent.PhdExaminer4Status);
                        objParams[5] = new SqlParameter("@P_UANO", objStudent.Uano);
                        objParams[6] = new SqlParameter("@P_REMARK", objStudent.Remark);
                        objParams[7] = new SqlParameter("@P_IPADDRESS", objStudent.IPADDRESS);
                        objParams[8] = new SqlParameter("@P_STATUS", status);
                        objParams[9] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[9].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_PHD_REJECT_EXAMINER", objParams, true);
                        retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        return ret.ToString();
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.UpdateStudent-> " + ex.ToString());
                    }
                }

                // -----  Approve  phd examiner  ---
                public string ApprovePhdExaminerByDean(Student objStudent)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[8];
                        objParams[0] = new SqlParameter("@P_IDNO", objStudent.IdNo);
                        objParams[1] = new SqlParameter("@P_Examiner1Status", objStudent.PhdExaminer1Status);
                        objParams[2] = new SqlParameter("@P_Examiner2Status", objStudent.PhdExaminer2Status);
                        objParams[3] = new SqlParameter("@P_Examiner3Status", objStudent.PhdExaminer3Status);
                        objParams[4] = new SqlParameter("@P_Examiner4Status", objStudent.PhdExaminer4Status);
                        objParams[5] = new SqlParameter("@P_UANO", objStudent.Uano);
                        objParams[6] = new SqlParameter("@P_IPADDRESS", objStudent.IPADDRESS);
                        objParams[7] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[7].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_PHD_APPROVE_EXAMINER", objParams, true);
                        retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        return ret.ToString();
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.UpdateStudent-> " + ex.ToString());
                    }
                }

                // -----  Dean Thesis Invitation for examiner  ---
                public string PhdThesisExaminerByDean(Student objStudent)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[12];
                        objParams[0] = new SqlParameter("@P_IDNO", objStudent.IdNo);
                        objParams[1] = new SqlParameter("@P_Examiner1Status", objStudent.PhdExaminer1Status);
                        objParams[2] = new SqlParameter("@P_Examiner2Status", objStudent.PhdExaminer2Status);
                        objParams[3] = new SqlParameter("@P_Examiner3Status", objStudent.PhdExaminer3Status);
                        objParams[4] = new SqlParameter("@P_Examiner4Status", objStudent.PhdExaminer4Status);

                        objParams[5] = new SqlParameter("@P_ExaminerFile1", objStudent.PhdExaminerFile1);
                        objParams[6] = new SqlParameter("@P_ExaminerFile2", objStudent.PhdExaminerFile2);
                        objParams[7] = new SqlParameter("@P_ExaminerFile3", objStudent.PhdExaminerFile3);
                        objParams[8] = new SqlParameter("@P_ExaminerFile4", objStudent.PhdExaminerFile4);

                        objParams[9] = new SqlParameter("@P_UANO", objStudent.Uano);
                        objParams[10] = new SqlParameter("@P_IPADDRESS", objStudent.IPADDRESS);
                        objParams[11] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[11].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_PHD_INS_EXAMINER_THESIS_DETAILS", objParams, true);
                        retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        return ret.ToString();
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.UpdateStudent-> " + ex.ToString());
                    }
                }

                // -----  Dean Thesis Approval for examiner  ---
                public string PhdThesisExaminerApprovalByDean(Student objStudent)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[8];
                        objParams[0] = new SqlParameter("@P_IDNO", objStudent.IdNo);
                        objParams[1] = new SqlParameter("@P_Examiner1Status", objStudent.PhdExaminer1Status);
                        objParams[2] = new SqlParameter("@P_Examiner2Status", objStudent.PhdExaminer2Status);
                        objParams[3] = new SqlParameter("@P_Examiner3Status", objStudent.PhdExaminer3Status);
                        objParams[4] = new SqlParameter("@P_Examiner4Status", objStudent.PhdExaminer4Status);
                        objParams[5] = new SqlParameter("@P_UANO", objStudent.Uano);
                        objParams[6] = new SqlParameter("@P_IPADDRESS", objStudent.IPADDRESS);
                        objParams[7] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[7].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_PHD_EXAMINER_THESIS_APPROVAL", objParams, true);
                        retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        return ret.ToString();
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.UpdateStudent-> " + ex.ToString());
                    }
                }

                // -----  Dean Thesis Approval for examiner  ---
                public string PhdExaminerDirectorApproval(Student objStudent)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[8];
                        objParams[0] = new SqlParameter("@P_IDNO", objStudent.IdNo);
                        objParams[1] = new SqlParameter("@P_Examiner1Status", objStudent.PhdExaminer1Status);
                        objParams[2] = new SqlParameter("@P_Examiner2Status", objStudent.PhdExaminer2Status);
                        objParams[3] = new SqlParameter("@P_Examiner3Status", objStudent.PhdExaminer3Status);
                        objParams[4] = new SqlParameter("@P_Examiner4Status", objStudent.PhdExaminer4Status);
                        objParams[5] = new SqlParameter("@P_UANO", objStudent.Uano);
                        objParams[6] = new SqlParameter("@P_IPADDRESS", objStudent.IPADDRESS);
                        objParams[7] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[7].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_PHD_FINAL_EXAMINER", objParams, true);
                        retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        return ret.ToString();
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.UpdateStudent-> " + ex.ToString());
                    }
                }

                //-----------------------------------------------------------------------------------------//
                ///---add for INSERT-- Progress report -- DCR  -- 22112017
                public int InsertPhdProgressDCR(FeeDemand objEntityClass)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[10];

                        objParams[0] = new SqlParameter("@P_SESSION_NO", objEntityClass.SessionNo);
                        objParams[1] = new SqlParameter("@P_IDNO", objEntityClass.StudentId);
                        objParams[2] = new SqlParameter("@P_ENROLLNO", objEntityClass.EnrollmentNo);
                        objParams[3] = new SqlParameter("@P_RECIEPTCODE", objEntityClass.ReceiptTypeCode);
                        objParams[4] = new SqlParameter("@P_SEMESTERNO", objEntityClass.SemesterNo);
                        objParams[5] = new SqlParameter("@P_PAYMENTTYPE", objEntityClass.PaymentTypeNo);
                        objParams[6] = new SqlParameter("@P_UA_NO", objEntityClass.UserNo);
                        objParams[7] = new SqlParameter("@P_RECIEPTNO", objEntityClass.Remark);
                        objParams[8] = new SqlParameter("@P_COUNTER_NO", objEntityClass.CounterNo);
                        objParams[9] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                        objParams[9].Direction = ParameterDirection.Output;

                        int ret = Convert.ToInt32(objSQLHelper.ExecuteNonQuerySP("PKG_CREATE_DCR_FOR_PHD_PROGRESS", objParams, true));
                        if (ret == 1)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.MarksEntryController.InsertStudentMarks() --> " + ex.Message + " " + ex.StackTrace);
                    }

                    return retStatus;
                }

                ///-------------------------------

                ///---Added for INSERT --Progress report -- DEMAND--22112017
                public int InsertPhdProgressDemand(FeeDemand objEntityClass)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[9];
                        objParams[0] = new SqlParameter("@P_SESSION_NO", objEntityClass.SessionNo);
                        objParams[1] = new SqlParameter("@P_IDNO", objEntityClass.StudentId);
                        objParams[2] = new SqlParameter("@P_ENROLLNO", objEntityClass.EnrollmentNo);
                        objParams[3] = new SqlParameter("@P_RECIEPTCODE", objEntityClass.ReceiptTypeCode);
                        objParams[4] = new SqlParameter("@P_SEMESTERNO", objEntityClass.SemesterNo);
                        objParams[5] = new SqlParameter("@P_PAYMENTTYPE", objEntityClass.PaymentTypeNo);
                        objParams[6] = new SqlParameter("@P_UA_NO", objEntityClass.CounterNo);
                        objParams[7] = new SqlParameter("@P_COUNTER_NO", objEntityClass.FeeCatNo);
                        objParams[8] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                        objParams[8].Direction = ParameterDirection.Output;

                        int ret = Convert.ToInt32(objSQLHelper.ExecuteNonQuerySP("PKG_CREATE_DEMAND_FOR_PHD_PROGRESS", objParams, true));
                        if (ret == 1)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.MarksEntryController.InsertStudentMarks() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return retStatus;
                }

                //--------phd Examiner Details Excel ----------------------29032019
                //-- Phd student details--26032018--//

                public DataSet RetrievePhdExaminerDetailsExcel(int idno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_PHD_EXAMINER_DETAIL_EXCEL", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.RetrieveStudentFeesDetails-> " + ex.ToString());
                    }
                    return ds;
                }

                //------------------------------- DRC CHAIRMAN LIST EXCEL-------------------------//
                public DataSet RetrievePhdDrchairmanlistExcel(int degreeno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_DEGREENO", degreeno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_PHD_DRCCHAIRMAN_LIST_EXCEL", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.RetrieveStudentFeesDetails-> " + ex.ToString());
                    }
                    return ds;
                }

                ///------------------Added by dipali on 06052019-- for  -- Annexure F student progress rpt details -------//

                public DataSet GETStudProgressRptDetails(int idno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);

                        ds = objSQLHelper.ExecuteDataSetSP("GET_PHD_STUDENT_PROGRESS_REPORT_DETAILS", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.EmpCreateController.GETStudProgressRptDetails-> " + ex.ToString());
                    }
                    return ds;
                }

                // =======================PHD NOC STUDENT DETAILS ============= Added by dipali on 07052019 =========//

                public DataSet GetPhdNOCDetails(string Rollno)
                {
                    DataSet dsPD = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_ROLLNO", Rollno);
                        dsPD = objSQLHelper.ExecuteDataSetSP("PKG_STUDENT_GET_NOC_DETAILS", objParams);
                    }
                    catch (Exception ex)
                    {
                        return dsPD;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.studentcontroller.GetPendingDocDetail-> " + ex.ToString());
                    }
                    return dsPD;
                }

                /// added by dipali on 07052019 -- update noc details.

                public string UpdateStudentNOCDetails(Student objStudent)
                {
                    string retun_status = string.Empty;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        //Update Student
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_IDNO", objStudent.IdNo);
                        objParams[1] = new SqlParameter("@P_STATUS", objStudent.Special);
                        objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[2].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_UPDATE_STUDENT_NOC", objParams, true);

                        retun_status = ret.ToString();
                    }
                    catch (Exception ex)
                    {
                        retun_status = "0";
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.UpdateStudent-> " + ex.ToString());
                    }

                    return retun_status;
                }

                //  added by dipali on 07052019  retriview noc details for excel

                public DataSet RetrievePhdNOCDetailsExcel()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        ds = objSQLHelper.ExecuteDataSet("PKG_ACD_PHD_STUDENT_NOC_DETAIL_EXCEL");
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.RetrieveStudentFeesDetails-> " + ex.ToString());
                    }
                    return ds;
                }

                // =======================PHD WITHDRAW DETAILS ===== added by dipali on 08052019 =========================//

                public DataTableReader GetPhdWithdrawStudentDetails(int idno)
                {
                    DataTableReader dtr = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);

                        dtr = objSQLHelper.ExecuteDataSetSP("PKG_PHD_WITHDRAW_STUDENT_BYID", objParams).Tables[0].CreateDataReader();
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentDetails->" + ex.ToString());
                    }
                    return dtr;
                }

                //----- Submit Phd Withdraw details ------//

                public string AddPhdWithdrawDetails(Student objstudentinfo)
                {
                    string retStatus = string.Empty;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_IDNO", objstudentinfo.IdNo);
                        objParams[1] = new SqlParameter("@P_REMARK", objstudentinfo.Remark);
                        objParams[2] = new SqlParameter("@P_ACTION", objstudentinfo.SuperRole);
                        objParams[3] = new SqlParameter("@P_OUT", SqlDbType.Int, 25);
                        objParams[3].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INS_PHD_WITHDRAW_DETAILS", objParams, true);

                        retStatus = ret.ToString();
                    }
                    catch (Exception ex)
                    {
                        retStatus = "0";
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.AddStudentDetailsForProvisionalDegree-> " + ex.ToString());
                    }

                    return retStatus;
                }

                // ============================ SUPERVISOR UPLOADED THESIS =========Added by dipali on 09052019=====================//

                public DataTableReader GetPhdStudentSupervisiorThesisDetails(int idno)
                {
                    DataTableReader dtr = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);

                        dtr = objSQLHelper.ExecuteDataSetSP("PKG_PHD_STUDENT_THESIS_DETAILS_FOR_SUPERVISIOR", objParams).Tables[0].CreateDataReader();
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentDetails->" + ex.ToString());
                    }
                    return dtr;
                }

                public string UpdatePhdThesisFile(Student objStudent)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    //int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        //Update Student Details
                        objParams = new SqlParameter[6];
                        //First Add Student Parameter
                        objParams[0] = new SqlParameter("@P_IDNO", objStudent.IdNo);
                        objParams[1] = new SqlParameter("@P_THS_NAME", objStudent.ThesisTitle);
                        objParams[2] = new SqlParameter("@P_THS_FILE", objStudent.PhdExaminerFile1);
                        objParams[3] = new SqlParameter("@P_UANO", objStudent.Uano);
                        objParams[4] = new SqlParameter("@P_IPADDRESS", objStudent.IPADDRESS);
                        objParams[5] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[5].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PHD_UPD_SUPERVISIOR_THESIS_DETAILS", objParams, true);

                        retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        return ret.ToString();
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.UpdateStudent-> " + ex.ToString());
                    }
                }

                //-- Phd student details--26032018--//
                public DataSet RetrievePhdWithdrawExcel()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);

                        ds = objSQLHelper.ExecuteDataSet("PKG_ACD_PHD_WITHDRAW_DETAIL_EXCEL");
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.RetrieveStudentFeesDetails-> " + ex.ToString());
                    }
                    return ds;
                }

                // --------------------Mtech Thesis Extension --------  Added by dipali on 30072019  -------------------//
                public DataTableReader GetMtechThesisExtendedDetails(int idno)
                {
                    DataTableReader dtr = null;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);

                        dtr = objSQLHelper.ExecuteDataSetSP("PKG_MTECH_STUDENT_EXTEND_DATE_DETAILS", objParams).Tables[0].CreateDataReader();
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentDetails->" + ex.ToString());
                    }
                    return dtr;
                }

                // --------------- Save Mtech thesis Details --------------------//
                public int SubmitMtechThesisExtensionDetails(Student objStudent)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[7];
                        //First Add Student Parameter
                        objParams[0] = new SqlParameter("@P_IDNO", objStudent.IdNo);
                        objParams[1] = new SqlParameter("@P_THS_EXTENDDATE", objStudent.ApprovedDate);
                        objParams[2] = new SqlParameter("@P_THS_FILE_NAME", objStudent.ThesisTitle);
                        objParams[3] = new SqlParameter("@P_THS_FILE_PATH", objStudent.PhdExaminerFile1);
                        objParams[4] = new SqlParameter("@P_THS_EXTUANO", objStudent.Uano);
                        objParams[5] = new SqlParameter("@P_THS_IP", objStudent.IPADDRESS);
                        objParams[6] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[6].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_INS_MTECH_STUDENT_EXTEND_DATE_DETAILS", objParams, true);

                        retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        return retStatus;
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.UpdateStudent-> " + ex.ToString());
                    }
                }

                // ================ PhD Progress report not submit student list -- added by dipali on 20082019  ===========//
                public DataSet RetrievePhdProgressNotSubmitExcel(int degree, int session)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_DEGREENO", degree);
                        objParams[1] = new SqlParameter("@P_SESSIONNO", session);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_PHD_PROGRESS_NOTSUBMIT_DETAIL_EXCEL", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.RetrieveStudentFeesDetails-> " + ex.ToString());
                    }
                    return ds;
                }

                //============== PHD Tracker get synopsis  and thesis details  =============//

                public DataSet RetrievePhdTrackerDoumentsDetails(int idno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSqlHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        ds = objSqlHelper.ExecuteDataSetSP("PKG_TRACKER_DOCUMENTS_DETAILS", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.RetrievePhdTrackerDoumentsDetails-> " + ex.ToString());
                    }
                    return ds;
                }

                // Added By Pritish S. on 18/06/2020 for CourseWorkDetails

                public DataTableReader GetStudentDetailsPhd(int idno)
                {
                    DataTableReader dtr = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);

                        dtr = objSQLHelper.ExecuteDataSetSP("PKG_SP_RET_STUDENT_REG_BYID_PHD", objParams).Tables[0].CreateDataReader();
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentRegistrationController.GetStudentDetails->" + ex.ToString());
                    }
                    return dtr;
                }

                public int AddUpdateCourseDetail(StudentRegistrationModel objSR)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[42];
                        objParams[0] = new SqlParameter("@P_IDNO", objSR.IDNO);
                        objParams[1] = new SqlParameter("@P_CCODE1", objSR.Ccode1);
                        objParams[2] = new SqlParameter("@P_COURSENAME1", objSR.CourseName1);
                        objParams[3] = new SqlParameter("@P_COLLEGE1", objSR.CollegeName1);
                        objParams[4] = new SqlParameter("@P_APPEARANCEYEAR1", objSR.AppearanceYear1);
                        objParams[5] = new SqlParameter("@P_CCODE2", objSR.Ccode2);
                        objParams[6] = new SqlParameter("@P_COURSENAME2", objSR.CourseName2);
                        objParams[7] = new SqlParameter("@P_COLLEGE2", objSR.CollegeName2);
                        objParams[8] = new SqlParameter("@P_APPEARANCEYEAR2", objSR.AppearanceYear2);
                        objParams[9] = new SqlParameter("@P_CCODE3", objSR.Ccode3);
                        objParams[10] = new SqlParameter("@P_COURSENAME3", objSR.CourseName3);
                        objParams[11] = new SqlParameter("@P_COLLEGE3", objSR.CollegeName3);
                        objParams[12] = new SqlParameter("@P_APPEARANCEYEAR3", objSR.AppearanceYear3);
                        objParams[13] = new SqlParameter("@P_CCODE4", objSR.Ccode4);
                        objParams[14] = new SqlParameter("@P_COURSENAME4", objSR.CourseName4);
                        objParams[15] = new SqlParameter("@P_COLLEGE4", objSR.CollegeName4);
                        objParams[16] = new SqlParameter("@P_APPEARANCEYEAR4", objSR.AppearanceYear4);
                        objParams[17] = new SqlParameter("@P_CCODE5", objSR.Ccode5);
                        objParams[18] = new SqlParameter("@P_COURSENAME5", objSR.CourseName5);
                        objParams[19] = new SqlParameter("@P_COLLEGE5", objSR.CollegeName5);
                        objParams[20] = new SqlParameter("@P_APPEARANCEYEAR5", objSR.AppearanceYear5);
                        objParams[21] = new SqlParameter("@P_CCODE6", objSR.Ccode6);
                        objParams[22] = new SqlParameter("@P_COURSENAME6", objSR.CourseName6);
                        objParams[23] = new SqlParameter("@P_COLLEGE6", objSR.CollegeName6);
                        objParams[24] = new SqlParameter("@P_APPEARANCEYEAR6", objSR.AppearanceYear6);
                        objParams[25] = new SqlParameter("@P_CCODE7", objSR.Ccode7);
                        objParams[26] = new SqlParameter("@P_COURSENAME7", objSR.CourseName7);
                        objParams[27] = new SqlParameter("@P_COLLEGE7", objSR.CollegeName7);
                        objParams[28] = new SqlParameter("@P_APPEARANCEYEAR7", objSR.AppearanceYear7);
                        objParams[29] = new SqlParameter("@P_CCODE8", objSR.Ccode8);
                        objParams[30] = new SqlParameter("@P_COURSENAME8", objSR.CourseName8);
                        objParams[31] = new SqlParameter("@P_COLLEGE8", objSR.CollegeName8);
                        objParams[32] = new SqlParameter("@P_APPEARANCEYEAR8", objSR.AppearanceYear8);
                        objParams[33] = new SqlParameter("@P_RESULT1", objSR.Result1);
                        objParams[34] = new SqlParameter("@P_RESULT2", objSR.Result2);
                        objParams[35] = new SqlParameter("@P_RESULT3", objSR.Result3);
                        objParams[36] = new SqlParameter("@P_RESULT4", objSR.Result4);
                        objParams[37] = new SqlParameter("@P_RESULT5", objSR.Result5);
                        objParams[38] = new SqlParameter("@P_RESULT6", objSR.Result6);
                        objParams[39] = new SqlParameter("@P_RESULT7", objSR.Result7);
                        objParams[40] = new SqlParameter("@P_RESULT8", objSR.Result8);

                        objParams[41] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[41].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_PHD_COURSE_DETAILS_UPDATE", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Academic.AddUpdateCourseDetail-> " + ex.ToString());
                    }
                    return retStatus;
                }

                // Added By Pritish S. on 20/06/2020

                public DataSet GetAllPublicationDetails(int idNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_GETALL_PUBLICATION_DETAILS", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ServiceBookController.GetAllPublicationDetails-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public DataSet GetSinglePublicationDetails(int pubTrxno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_PUBTRXNO", pubTrxno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_GETSINGLE_PUBLICATION_DETAILS", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ServiceBookController.GetSinglePublicationDetails-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public int AddUpdPublicationDetails(PhdPostAdmission objPubDel, DataTable SB_AUTHORLIST_RECORD)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[32];
                        objParams[0] = new SqlParameter("@P_IDNO", objPubDel.IDNO);
                        objParams[1] = new SqlParameter("@P_PUBLICATION_TYPE", objPubDel.PUBLICATION_TYPE);
                        objParams[2] = new SqlParameter("@P_TITLE", objPubDel.TITLE);
                        objParams[3] = new SqlParameter("@P_SUBJECT", objPubDel.SUBJECT);

                        if (!objPubDel.PUBLICATIONDATE.Equals(DateTime.MinValue))
                        {
                            objParams[4] = new SqlParameter("@P_PUBLICATIONDATE", objPubDel.PUBLICATIONDATE);
                            objParams[5] = new SqlParameter("@P_DETAILS", objPubDel.DETAILS);
                            objParams[6] = new SqlParameter("@P_SPONSORED_AMOUNT", objPubDel.SPONSORED_AMOUNT);
                        }
                        else
                        {
                            objParams[4] = new SqlParameter("@P_PUBLICATIONDATE", DBNull.Value);
                            objParams[5] = new SqlParameter("@P_DETAILS", objPubDel.DETAILS);
                            objParams[6] = new SqlParameter("@P_SPONSORED_AMOUNT", objPubDel.SPONSORED_AMOUNT);
                        }

                        objParams[7] = new SqlParameter("@P_COLLEGE_CODE", objPubDel.COLLEGE_CODE);
                        objParams[8] = new SqlParameter("@P_NAME", objPubDel.CONFERENCE_NAME);
                        objParams[9] = new SqlParameter("@P_ORGANISOR", objPubDel.ORGANISOR);
                        objParams[10] = new SqlParameter("@P_PAGENO", objPubDel.PAGENO);
                        objParams[11] = new SqlParameter("@P_PUBLICATION", objPubDel.PUBLICATION);
                        objParams[12] = new SqlParameter("@P_ATTACHMENT", objPubDel.ATTACHMENTS);
                        objParams[13] = new SqlParameter("@P_ISBN", objPubDel.ISBN);
                        objParams[14] = new SqlParameter("@P_VolumeNo", objPubDel.VOLUME_NO);
                        objParams[15] = new SqlParameter("@P_IssueNo", objPubDel.ISSUE_NO);
                        objParams[16] = new SqlParameter("@P_Publication_Status", objPubDel.PUB_STATUS);
                        objParams[17] = new SqlParameter("@P_Year", objPubDel.YEAR);
                        objParams[18] = new SqlParameter("@p_Location", objPubDel.LOCATION);
                        objParams[19] = new SqlParameter("@P_Publisher", objPubDel.PUBLISHER);
                        objParams[20] = new SqlParameter("@P_IsConference", objPubDel.IS_CONFERENCE);
                        objParams[21] = new SqlParameter("@SB_AUTHORLIST_RECORD", SB_AUTHORLIST_RECORD);
                        objParams[22] = new SqlParameter("@P_PUBTRXNO", objPubDel.PUBTRXNO);
                        objParams[23] = new SqlParameter("@P_IsJournalScopus", objPubDel.IsJournalScopus);
                        //IsJournalScopus
                        objParams[24] = new SqlParameter("@P_IMPACTFACTORS", objPubDel.IMPACTFACTORS);
                        objParams[25] = new SqlParameter("@P_CITATIONINDEX", objPubDel.CITATIONINDEX);
                        objParams[26] = new SqlParameter("@P_EISSN", objPubDel.EISSN);
                        objParams[27] = new SqlParameter("@P_PUB_ADD", objPubDel.PUB_ADD);

                        #region Add on 13-01-20 By Sonali Ambedare

                        objParams[28] = new SqlParameter("@P_Month", objPubDel.MONTH);
                        objParams[29] = new SqlParameter("@P_DOINO", objPubDel.DOIN);
                        objParams[30] = new SqlParameter("@P_INDEXING_TYPE", objPubDel.INDEXING_TYPE);

                        #endregion Add on 13-01-20 By Sonali Ambedare

                        objParams[31] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[31].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_INSERT_PUBLICATION_DETAILS", objParams, true);
                        if (Convert.ToInt32(ret) == -1)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordFound);
                        }
                        else
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.ServiceBookController.AddPublicationDetails-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public void upload_new_files(string folder, int idno, string primary, string table, string initials, System.Web.UI.WebControls.FileUpload fuFile)
                {
                    string uploadPath = HttpContext.Current.Server.MapPath("~/ESTABLISHMENT/upload_files/" + folder + "/" + idno + "/");
                    if (!System.IO.Directory.Exists(uploadPath))
                    {
                        System.IO.Directory.CreateDirectory(uploadPath);
                    }
                    //Upload the File
                    if (!fuFile.PostedFile.FileName.Equals(string.Empty))
                    {
                        int newfilename = Convert.ToInt32(objcommon.LookUp(table, "max(" + primary + ")", ""));
                        string uploadFile = initials + newfilename + System.IO.Path.GetExtension(fuFile.PostedFile.FileName);
                        fuFile.PostedFile.SaveAs(uploadPath + uploadFile);
                    }
                }

                public void update_upload(string folder, int trxno, string lastfilename, int idno, string initials, System.Web.UI.WebControls.FileUpload fuFile)
                {
                    //Upload the File
                    string uploadPath = HttpContext.Current.Server.MapPath("~/ESTABLISHMENT/upload_files/" + folder + "/" + idno + "/");
                    if (!System.IO.Directory.Exists(uploadPath))
                    {
                        System.IO.Directory.CreateDirectory(uploadPath);
                    }
                    if (!fuFile.PostedFile.FileName.Equals(string.Empty))
                    {
                        //Update Assignment
                        string oldFileName = string.Empty;
                        oldFileName = initials + trxno + System.IO.Path.GetExtension(lastfilename);

                        if (System.IO.File.Exists(uploadPath + oldFileName))
                        {
                            System.IO.File.Delete(uploadPath + oldFileName);
                        }

                        string uploadFile = initials + trxno + System.IO.Path.GetExtension(fuFile.PostedFile.FileName);
                        fuFile.PostedFile.SaveAs(uploadPath + uploadFile);
                    }
                }

                public int DeletePublicationDetails(int pubTrxno)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_PUBTRXNO", pubTrxno);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_DEL_PUBLICATION_DETAILS", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordDeleted);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.ServiceBookController.DeleteEmpImage.-> " + ex.ToString());
                    }
                    return retStatus;
                }

                // Meeting
                public DataSet GetAllMeetingDetails(int idNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_GETALL_MEETING_DETAILS", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ServiceBookController.GetAllPublicationDetails-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public int AddUpdMeetingDetails(PhdPostAdmission objPubDel)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[8];
                        objParams[0] = new SqlParameter("@P_IDNO", objPubDel.IDNO);
                        objParams[1] = new SqlParameter("@P_MEETINGDATE", objPubDel.MEETINGDATE);
                        objParams[2] = new SqlParameter("@P_MEETINGNAME", objPubDel.MEETINGNAME);
                        objParams[3] = new SqlParameter("@P_MEETINGVENUE", objPubDel.MEETINGVENUE);
                        objParams[4] = new SqlParameter("@P_MEETINGREMARKS", objPubDel.REMARK);
                        objParams[5] = new SqlParameter("@P_ATTACHMENT", objPubDel.ATTACHMENTS);
                        objParams[6] = new SqlParameter("@P_RECNO", objPubDel.RECNO);
                        objParams[7] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[7].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_INSERT_MEETING_DETAILS", objParams, true);
                        if (Convert.ToInt32(ret) == -1)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordFound);
                        }
                        else
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.ServiceBookController.AddPublicationDetails-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int DeleteMeetingDetails(int recno)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_RECNO", recno);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_DEL_MEETING_DETAILS", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordDeleted);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.ServiceBookController.DeleteEmpImage.-> " + ex.ToString());
                    }
                    return retStatus;
                }

                //Progress
                public DataSet GetAllProgressDetails(int idNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_GETALL_PROGRESS_DETAILS", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ServiceBookController.GetAllPublicationDetails-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public int AddUpdProgressDetails(PhdPostAdmission objPubDel)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[9];
                        objParams[0] = new SqlParameter("@P_IDNO", objPubDel.IDNO);
                        objParams[1] = new SqlParameter("@P_FROMDATE", objPubDel.FROMDATE);
                        objParams[2] = new SqlParameter("@P_TODATE", objPubDel.TODATE);
                        objParams[3] = new SqlParameter("@P_SUBMISSIONDATE", objPubDel.SUBMISSIONDATE);
                        objParams[4] = new SqlParameter("@P_DURATION", objPubDel.DURATION);
                        objParams[5] = new SqlParameter("@P_REMARKS", objPubDel.REMARK);
                        objParams[6] = new SqlParameter("@P_RECNO", objPubDel.RECNO);
                        objParams[7] = new SqlParameter("@P_ATTACHMENT", objPubDel.ATTACHMENTS);
                        objParams[8] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[8].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_INSERT_PROGRESS_DETAILS", objParams, true);
                        if (Convert.ToInt32(ret) == -1)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordFound);
                        }
                        else
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.ServiceBookController.AddPublicationDetails-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int DeleteProgressDetails(int recno)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_RECNO", recno);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_DEL_PROGRESS_DETAILS", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordDeleted);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.ServiceBookController.DeleteEmpImage.-> " + ex.ToString());
                    }
                    return retStatus;
                }

                //SYNOPSIS
                public DataSet GetAllSynopsisDetails(int idNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_GETALL_SYNOPSIS_DETAILS", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ServiceBookController.GetAllPublicationDetails-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public int AddUpdSynopsisDetails(PhdPostAdmission objPubDel)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_IDNO", objPubDel.IDNO);
                        objParams[1] = new SqlParameter("@P_SUBMISSIONDATE", objPubDel.SUBMISSIONDATE);
                        objParams[2] = new SqlParameter("@P_COMMENT", objPubDel.REMARK);
                        objParams[3] = new SqlParameter("@P_RECNO", objPubDel.RECNO);
                        objParams[4] = new SqlParameter("@P_ATTACHMENT", objPubDel.ATTACHMENTS);
                        objParams[5] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[5].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_INSERT_SYNOPSIS_DETAILS", objParams, true);
                        if (Convert.ToInt32(ret) == -1)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordFound);
                        }
                        else
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.ServiceBookController.AddPublicationDetails-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int DeleteSynopsisDetails(int recno)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_RECNO", recno);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_DEL_SYNOPSIS_DETAILS", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordDeleted);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.ServiceBookController.DeleteEmpImage.-> " + ex.ToString());
                    }
                    return retStatus;
                }

                //THESIS
                public DataSet GetAllThesisDetails(int idNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_GETALL_THESIS_DETAILS", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ServiceBookController.GetAllPublicationDetails-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public int AddUpdThesisDetails(PhdPostAdmission objPubDel)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_IDNO", objPubDel.IDNO);
                        objParams[1] = new SqlParameter("@P_SUBMISSIONDATE", objPubDel.SUBMISSIONDATE);
                        objParams[2] = new SqlParameter("@P_COMMENT", objPubDel.REMARK);
                        objParams[3] = new SqlParameter("@P_RECNO", objPubDel.RECNO);
                        objParams[4] = new SqlParameter("@P_ATTACHMENT", objPubDel.ATTACHMENTS);
                        objParams[5] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[5].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_INSERT_THESIS_DETAILS", objParams, true);
                        if (Convert.ToInt32(ret) == -1)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordFound);
                        }
                        else
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.ServiceBookController.AddPublicationDetails-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int DeleteThesisDetails(int recno)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_RECNO", recno);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_DEL_THESIS_DETAILS", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordDeleted);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.ServiceBookController.DeleteEmpImage.-> " + ex.ToString());
                    }
                    return retStatus;
                }

                //CurrentStatus
                public DataSet GetAllCurrentStatusDetails(int idNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_GETALL_CURRENTSTATUS_DETAILS", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ServiceBookController.GetAllPublicationDetails-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public int AddUpdCurrentStatusDetails(PhdPostAdmission objPubDel)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_IDNO", objPubDel.IDNO);
                        objParams[1] = new SqlParameter("@P_STATUSDATE", objPubDel.SUBMISSIONDATE);
                        objParams[2] = new SqlParameter("@P_STATUSNO", objPubDel.STATUSNO);
                        objParams[3] = new SqlParameter("@P_RECNO", objPubDel.RECNO);
                        objParams[4] = new SqlParameter("@P_ATTACHMENT", objPubDel.ATTACHMENTS);
                        objParams[5] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[5].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_INSERT_CURRENTSTATUS_DETAILS", objParams, true);
                        if (Convert.ToInt32(ret) == -1)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordFound);
                        }
                        else
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.ServiceBookController.AddPublicationDetails-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int DeleteCurrentStatusDetails(int recno)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_RECNO", recno);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_DEL_CURRENTSTATUS_DETAILS", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordDeleted);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.ServiceBookController.DeleteEmpImage.-> " + ex.ToString());
                    }
                    return retStatus;
                }

                // Details of Oral Examination
                public int AddOralExamDetails(PhdPostAdmission objPubDel)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_ACD_PHD_ORAL_EXAM_DETAILS", objPubDel.Phd_Oral_Exam);
                        objParams[1] = new SqlParameter("@P_IDNO", objPubDel.IDNO);
                        objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[2].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_INS_ACD_PHD_ORAL_EXAM_DETAILS", objParams, true);
                        if (Convert.ToInt32(ret) == -1)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordFound);
                        }
                        else
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.PhdController.AddOralExamDetails-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int DeleteOralExamDetails(int recno)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_RECNO", recno);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ACD_DEL_PHD_ORAL_EXAM_DETAILS", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordDeleted);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.PhdController.DeleteOralExamDetails.-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int DeleteOralDetails(int recno)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_RECNO", recno);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_DEL_ORAL_DETAILS", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordDeleted);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.ServiceBookController.DeleteEmpImage.-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int InsertOralExamDetails(PhdPostAdmission objPubDel)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_IDNO", objPubDel.IDNO);
                        objParams[1] = new SqlParameter("@P_ORALEXAMDATE", objPubDel.Oralexamdate);
                        objParams[2] = new SqlParameter("@P_VENUE", objPubDel.OralVenue);
                        objParams[3] = new SqlParameter("@P_COMMENT", objPubDel.OralComment);
                        objParams[4] = new SqlParameter("@P_ATTACHMENT", objPubDel.ATTACHMENTS);

                        objParams[5] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[5].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_PHD_ORAL_EXAM_DETAILS", objParams, true);
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

                //Details of Viva Voce
                public int AddVivaVoceDetails(PhdPostAdmission objPubDel)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_ACD_PHD_VIVA_VOCE_DETAILS", objPubDel.Phd_Viva_Voce);
                        objParams[1] = new SqlParameter("@P_IDNO", objPubDel.IDNO);
                        objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[2].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_INS_ACD_PHD_VIVA_VOCE_DETAILS", objParams, true);
                        if (Convert.ToInt32(ret) == -1)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordFound);
                        }
                        else
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.PhdController.AddVivaVoceDetails-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int InsertVivaVoceDetails(PhdPostAdmission objPubDel)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[9];
                        objParams[0] = new SqlParameter("@P_IDNO", objPubDel.IDNO);
                        objParams[1] = new SqlParameter("@P_VIVAVOCEDATE", objPubDel.VivaDate);
                        objParams[2] = new SqlParameter("@P_VENUE", objPubDel.Venue);
                        objParams[3] = new SqlParameter("@P_INTERNAL_EXAMINER", objPubDel.InternalExaminer);
                        objParams[4] = new SqlParameter("@P_EXTERNAL_EXAMINER", objPubDel.ExternalExaminer);
                        objParams[5] = new SqlParameter("@P_COMMENT", objPubDel.VivaComments);
                        objParams[6] = new SqlParameter("@P_STATUS", objPubDel.VivaStatus);
                        objParams[7] = new SqlParameter("@P_ATTACHMENT", objPubDel.ATTACHMENTS);

                        objParams[8] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[8].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_PHD_VIVA_VOCE_DETAILS", objParams, true);
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

                public int DeleteVivaVoceDetails(int recno)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_RECNO", recno);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_DEL_VIVA_VOCE_DETAILS", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordDeleted);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.ServiceBookController.DeleteEmpImage.-> " + ex.ToString());
                    }
                    return retStatus;
                }

                #region

                public int InsertPhd_Scrutiny_Student(int UserNo, string Stude_Id, string Title, int Supervisor, int Co_Supervisor, int Assing_To_Faculty, int College_id, int UA_NO, int degreeno, int branchno, int area_int_no, int Campus, int Admbatch)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[14];
                        objParams[0] = new SqlParameter("@P_USERNO", UserNo);
                        objParams[1] = new SqlParameter("@P_USERNAME", Stude_Id);
                        objParams[2] = new SqlParameter("@P_TITLE", Title);
                        objParams[3] = new SqlParameter("@P_SUPERVISOR", Supervisor);
                        objParams[4] = new SqlParameter("@P_CO_SUPERVISOR", Co_Supervisor);
                        objParams[5] = new SqlParameter("@P_ASSING_TO_FACULTY", Assing_To_Faculty);
                        objParams[6] = new SqlParameter("@P_COLLEGE_ID", College_id);
                        objParams[7] = new SqlParameter("@P_UA_NO", UA_NO);
                        objParams[8] = new SqlParameter("@P_DEGREENO", degreeno);
                        objParams[9] = new SqlParameter("@P_BRANCHNO", branchno);
                        objParams[10] = new SqlParameter("@P_AREA_INT_NO", area_int_no);
                        objParams[11] = new SqlParameter("@P_CAMPUSNO", Campus);
                        objParams[12] = new SqlParameter("@P_ADMBATCH", Admbatch);

                        objParams[13] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[13].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_INSERT_PHD_NEW_REGISTRATION_STUDENT", objParams, true);
                        if (Convert.ToInt32(ret) == 1)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        else if (Convert.ToInt32(ret) == 2)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        else if (Convert.ToInt32(ret) == 2627)
                            retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Academic.AddProjectDetails-> " + ex.ToString());
                    }

                    return retStatus;
                }

                public int InsertPhd_Scrutiny_Student_Submit_To(int UserNo, string Stude_Id, int Submit_To, int UA_NO, int Number, int Dean, int Co_Supervisor)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[8];
                        objParams[0] = new SqlParameter("@P_USERNO", UserNo);
                        objParams[1] = new SqlParameter("@P_USERNAME", Stude_Id);
                        objParams[2] = new SqlParameter("@P_SUBMIT_TO", Submit_To);
                        objParams[3] = new SqlParameter("@P_UA_NO", UA_NO);
                        objParams[4] = new SqlParameter("@P_FILTER", Number);
                        objParams[5] = new SqlParameter("@P_DEAN_NAME", Dean);
                        objParams[6] = new SqlParameter("@P_CO_SUPERVISOR", Co_Supervisor);

                        objParams[7] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[7].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_INSERT_PHD_NEW_REGISTRATION_STUDENT_SUBMIT_TO", objParams, true);
                        if (Convert.ToInt32(ret) == 1)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        else if (Convert.ToInt32(ret) == 2)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        else if (Convert.ToInt32(ret) == 2627)
                            retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Academic.AddProjectDetails-> " + ex.ToString());
                    }

                    return retStatus;
                }

                public int InsertPhd_Progress_Review_Details(int idno, int Review_Thesis_no, int Review_Or_Thesis, string MemberPresent, string Date, int Outcome, string Remark, int UA_NO)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[9];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        objParams[1] = new SqlParameter("@P_REVIEW_THESIS_NO", Review_Thesis_no);
                        objParams[2] = new SqlParameter("@P_REVIEW_OR_THESIS", Review_Or_Thesis);

                        objParams[3] = new SqlParameter("@P_MEMBER", MemberPresent);
                        objParams[4] = new SqlParameter("@P_PRESE_DATE", Date);
                        objParams[5] = new SqlParameter("@P_OUTCOME", Outcome);
                        objParams[6] = new SqlParameter("@P_REMARK", Remark);
                        objParams[7] = new SqlParameter("@P_UA_NO", UA_NO);
                        objParams[8] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[8].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_INSERT_PHD_PROGRESS_REVIEW_THESIS_DETAILS", objParams, true);
                        if (Convert.ToInt32(ret) == 1)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        else if (Convert.ToInt32(ret) == 2)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        else if (Convert.ToInt32(ret) == 2627)
                            retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Academic.AddProjectDetails-> " + ex.ToString());
                    }

                    return retStatus;
                }

                public int InsertPhd_Final_Examination_schedule(int Idno, string Tittle, int SuperVisor, int Co_SuperVisor, string ExamDate, string Time, string Description, int Mode, int UA_NO)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[10];
                        objParams[0] = new SqlParameter("@P_IDNO", Idno);
                        objParams[1] = new SqlParameter("@P_TITTLE", Tittle);
                        objParams[2] = new SqlParameter("@P_SUPERVISOR", SuperVisor);

                        objParams[3] = new SqlParameter("@P_CO_SUPERVISOR", Co_SuperVisor);
                        objParams[4] = new SqlParameter("@P_DATE", ExamDate);
                        objParams[5] = new SqlParameter("@P_TIME", Time);
                        objParams[6] = new SqlParameter("@P_DESCRIPTION", Description);
                        objParams[7] = new SqlParameter("@P_MODE", Mode);
                        objParams[8] = new SqlParameter("@P_UA_NO", UA_NO);
                        objParams[9] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[9].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_INSERT_PHD_FINAL_EXAMINATION_DETAILS", objParams, true);
                        if (Convert.ToInt32(ret) == 1)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        else if (Convert.ToInt32(ret) == 2)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        else if (Convert.ToInt32(ret) == 2627)
                            retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Academic.AddProjectDetails-> " + ex.ToString());
                    }

                    return retStatus;
                }

                public int InsertPhd_Final_Examination_schedule_Outcome(int Idno, string Member, string Date, string Remark, int OutCome, int UA_NO, int Filter)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[8];
                        objParams[0] = new SqlParameter("@P_IDNO", Idno);
                        objParams[1] = new SqlParameter("@P_MEMBER_PRESENT", Member);
                        objParams[2] = new SqlParameter("@P_PRESENT_DATE", Date);

                        objParams[3] = new SqlParameter("@P_REMARK", Remark);
                        objParams[4] = new SqlParameter("@P_OUTCOME", OutCome);

                        objParams[5] = new SqlParameter("@P_UA_NO", UA_NO);
                        objParams[6] = new SqlParameter("@P_FILER", Filter);
                        objParams[7] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[7].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_INSERT_PHD_FINAL_EXAMINATION_DETAILS", objParams, true);
                        if (Convert.ToInt32(ret) == 1)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        else if (Convert.ToInt32(ret) == 2)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        else if (Convert.ToInt32(ret) == 2627)
                            retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Academic.AddProjectDetails-> " + ex.ToString());
                    }

                    return retStatus;
                }

                public int InsertPhd_Scrutiny_Interview_Schedule_Student(int UserNo, string UserName, string Tittle, int Supervisor, int Co_Supervisor, int Mode, DateTime Interview_Date, string Time_Slot, string Interview_Descri, int Uano)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[11];
                        objParams[0] = new SqlParameter("@P_USERNO", UserNo);
                        objParams[1] = new SqlParameter("@P_USERNAME", UserName);
                        objParams[2] = new SqlParameter("@P_TITTLE", Tittle);
                        objParams[3] = new SqlParameter("@P_SUPERVISOR", Supervisor);
                        objParams[4] = new SqlParameter("@P_CO_SUPERVISOR", Co_Supervisor);
                        objParams[5] = new SqlParameter("@P_MODE", Mode);
                        objParams[6] = new SqlParameter("@P_INTERVIEW_DATE", Interview_Date);
                        objParams[7] = new SqlParameter("@P_TIME_SLOT", Time_Slot);
                        objParams[8] = new SqlParameter("@P_INTERVIEW_DESCRI", Interview_Descri);
                        objParams[9] = new SqlParameter("@P_UA_NO", Uano);

                        objParams[10] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[10].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_INSERT_PHD_STUDENT_INTERVIEW_SCHEDULE", objParams, true);
                        if (Convert.ToInt32(ret) == 1)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        else if (Convert.ToInt32(ret) == 2)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        else if (Convert.ToInt32(ret) == 2627)
                            retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Academic.AddProjectDetails-> " + ex.ToString());
                    }

                    return retStatus;
                }

                public int InsertPhd_Scrutiny_Interview_Outcome_Student(int UserNo, string UserName, string MemberPresent, DateTime PresesentDate, int InterviewOutcome, int Uano, string Remark, string Password)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[9];
                        objParams[0] = new SqlParameter("@P_USERNO", UserNo);
                        objParams[1] = new SqlParameter("@P_USERNAME", UserName);
                        objParams[2] = new SqlParameter("@P_MEMBERPRESENT", MemberPresent);
                        objParams[3] = new SqlParameter("@P_PRESESENTDATE", PresesentDate);
                        objParams[4] = new SqlParameter("@P_INTERVIEWOUTCOME", InterviewOutcome);
                        objParams[5] = new SqlParameter("@P_UA_NO", Uano);
                        objParams[6] = new SqlParameter("@P_REMARK", Remark);
                        objParams[7] = new SqlParameter("@P_PASSWORD", Password);

                        objParams[8] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[8].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_INSERT_PHD_STUDENT_INTERVIEW_OUTCOME", objParams, true);
                        if (Convert.ToInt32(ret) == 1)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        else if (Convert.ToInt32(ret) == 2)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        else if (Convert.ToInt32(ret) == 2627)
                            retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Academic.AddProjectDetails-> " + ex.ToString());
                    }

                    return retStatus;
                }

                public int InsertUpload_ProposalFile(int UserNo, string UserName, string FileName)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_USERNO", UserNo);
                        objParams[1] = new SqlParameter("@P_USERNAME", UserName);
                        objParams[2] = new SqlParameter("@P_FILENAME", FileName);

                        objParams[3] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[3].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_INSERT_PHD_STUDENT_UPLOADE_PROPOSAL", objParams, true);
                        if (Convert.ToInt32(ret) == 1)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        else if (Convert.ToInt32(ret) == 2)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        else if (Convert.ToInt32(ret) == 2627)
                            retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Academic.AddProjectDetails-> " + ex.ToString());
                    }

                    return retStatus;
                }

                public int InsertPhdstudent_firstPresentation(int idno, string Regno, int User_No, string UserName, int IsResearch, string Remark, string Final_Title, int Condi_Registration, DateTime Registration_Date, string Member_present, DateTime Presentation_Date, int UaNo, int Status)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[14];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        objParams[1] = new SqlParameter("@P_REGNO", Regno);
                        objParams[2] = new SqlParameter("@P_USERNO", User_No);
                        objParams[3] = new SqlParameter("@P_USERNAME", UserName);
                        objParams[4] = new SqlParameter("@P_ISRESEARCH", IsResearch);
                        objParams[5] = new SqlParameter("@P_REMARK", Remark);
                        objParams[6] = new SqlParameter("@P_FINAL_TITLE", Final_Title);
                        objParams[7] = new SqlParameter("@P_REGISTRATION", Condi_Registration);
                        objParams[8] = new SqlParameter("@P_REGISTRATION_DATE", Registration_Date);
                        objParams[9] = new SqlParameter("@P_MEMBER_PRESENT", Member_present);

                        objParams[10] = new SqlParameter("@P_PRESENTATION_DATE", Presentation_Date);
                        objParams[11] = new SqlParameter("@P_UANO", UaNo);
                        objParams[12] = new SqlParameter("@P_OFFER_LATER_STATUS", Status);

                        objParams[13] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[13].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_INSERT_PHD_STUDENT_FIRST_PRESENTATION_DATA", objParams, true);
                        if (Convert.ToInt32(ret) == 1)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        else if (Convert.ToInt32(ret) == 2)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        else if (Convert.ToInt32(ret) == 2627)
                            retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Academic.AddProjectDetails-> " + ex.ToString());
                    }

                    return retStatus;
                }

                public int InsertUpload_ComprehensiveProposalFile(int Idno, string Regno, string FileName)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_IDNO", Idno);
                        objParams[1] = new SqlParameter("@P_REGNO", Regno);
                        objParams[2] = new SqlParameter("@P_FILENAME", FileName);

                        objParams[3] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[3].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_INSERT_PHD_STUDENT_UPLOADE_COMPREHENSIVE_PROPOSAL", objParams, true);
                        if (Convert.ToInt32(ret) == 1)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        else if (Convert.ToInt32(ret) == 2)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        else if (Convert.ToInt32(ret) == 2627)
                            retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Academic.AddProjectDetails-> " + ex.ToString());
                    }

                    return retStatus;
                }

                public int InsertProposalOnlineFees(int Idno, decimal Amount, string OrderId, string RecieptCode, string PayMode, int PayType, decimal ServiceChrge, string Remark, decimal TotalAmount)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[10];
                        objParams[0] = new SqlParameter("@P_IDNO", Idno);
                        objParams[1] = new SqlParameter("@P_AMOUNT", Amount);
                        objParams[2] = new SqlParameter("@P_ORDER_ID", OrderId);

                        objParams[3] = new SqlParameter("@P_RECIEPTCODE", RecieptCode);
                        objParams[4] = new SqlParameter("@P_PAY_MODE", PayMode);
                        objParams[5] = new SqlParameter("@P_PAYMENTTYPE", PayType);
                        objParams[6] = new SqlParameter("@P_SERVICE_CHARGE", ServiceChrge);
                        objParams[7] = new SqlParameter("@P_REMARK", Remark);
                        objParams[8] = new SqlParameter("@P_TOTAL_AMOUNT", TotalAmount);
                        objParams[9] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[9].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_INSERT_PROPOSAL_ONLINE_FEES", objParams, true);
                        if (Convert.ToInt32(ret) == 1)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        else if (Convert.ToInt32(ret) == 2)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        else if (Convert.ToInt32(ret) == 2627)
                            retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Academic.AddProjectDetails-> " + ex.ToString());
                    }

                    return retStatus;
                }

                public int InsertProposalOfflineFees(int Idno, decimal Amount, string OrderId, string RecieptCode, string PayMode, int PayType, decimal ServiceChrge, string Remark, decimal DepositeAmount, int BankNo, string BankName, string BankBranch, string FileName, DateTime Payment_Date, int Dcr_No)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[16];
                        objParams[0] = new SqlParameter("@P_IDNO", Idno);
                        objParams[1] = new SqlParameter("@P_AMOUNT", Amount);
                        objParams[2] = new SqlParameter("@P_ORDER_ID", OrderId);

                        objParams[3] = new SqlParameter("@P_RECIEPTCODE", RecieptCode);
                        objParams[4] = new SqlParameter("@P_PAY_MODE", PayMode);
                        objParams[5] = new SqlParameter("@P_PAYMENTTYPE", PayType);
                        objParams[6] = new SqlParameter("@P_SERVICE_CHARGE", ServiceChrge);
                        objParams[7] = new SqlParameter("@P_REMARK", Remark);
                        objParams[8] = new SqlParameter("@P_TOTAL_AMOUNT", DepositeAmount);
                        objParams[9] = new SqlParameter("@P_BANK_NO", BankNo);
                        objParams[10] = new SqlParameter("@P_BANK_NAME", BankName);

                        objParams[11] = new SqlParameter("@P_BRANCH_NAME", BankBranch);
                        objParams[12] = new SqlParameter("@P_DOC_FILENAME", FileName);
                        objParams[13] = new SqlParameter("@P_CHALAN_DATE", Payment_Date);
                        objParams[14] = new SqlParameter("@P_DCR_NO", Dcr_No);
                        objParams[15] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[15].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_INSERT_PROPOSAL_ONLINE_FEES", objParams, true);
                        if (Convert.ToInt32(ret) == 1)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        else if (Convert.ToInt32(ret) == 2)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        else if (Convert.ToInt32(ret) == 2627)
                            retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Academic.AddProjectDetails-> " + ex.ToString());
                    }

                    return retStatus;
                }

                public int InsertPhdstudentComprehensiveProposalReview(int idno, string MemberPresent, string Remark, int Registration, int OutCome, string Tittle, int UA_NO, int Filtr)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[9];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        objParams[1] = new SqlParameter("@P_MEMBER_PRESENT", MemberPresent);
                        objParams[2] = new SqlParameter("@P_REMARK", Remark);

                        objParams[3] = new SqlParameter("@P_REGISTRATION", Registration);
                        objParams[4] = new SqlParameter("@P_OUTCOME", OutCome);
                        objParams[5] = new SqlParameter("@P_TITTLE", Tittle);
                        objParams[6] = new SqlParameter("@P_UANO", UA_NO);
                        objParams[7] = new SqlParameter("@P_FILTER", Filtr);
                        objParams[8] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[8].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PHD_STUDENT_COMPREHENSIVE_PROPOSAL_REVIEW", objParams, true);
                        if (Convert.ToInt32(ret) == 1)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        else if (Convert.ToInt32(ret) == 2)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        else if (Convert.ToInt32(ret) == 2627)
                            retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Academic.AddProjectDetails-> " + ex.ToString());
                    }

                    return retStatus;
                }

                public int UpdateFinalUserConformation(int Userno, int CampusNo, int WeekDayNo, int ua_no)
                {
                    int status = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_USERNO", Userno);
                        objParams[1] = new SqlParameter("@P_CAMPUSNO", CampusNo);
                        objParams[2] = new SqlParameter("@P_WEEKDAYNO", WeekDayNo);
                        objParams[3] = new SqlParameter("@P_UA_NO", ua_no);
                        objParams[4] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                        objParams[4].Direction = ParameterDirection.Output;

                        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_UPDATE_PHD_STUDENT_FINAL_STATUS", objParams, true);

                        if (obj != null && obj.ToString() != "-99")
                            status = Convert.ToInt32(CustomStatus.RecordSaved);
                        else
                            status = Convert.ToInt32(CustomStatus.Error);
                    }
                    catch (Exception ex)
                    {
                        status = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.DocumentController.AddDocument() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return status;
                }

                #endregion // ADD BY ROSHAN PATIL ON 02-11-20200
            }
        }//END: BusinessLayer.BusinessLogic
    }//END: UAIMS
}//END: IITMS