using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities;
using System;
using System.Data;
using System.Data.SqlClient;

namespace IITMS.UAIMS.BusinessLogicLayer.BusinessLogic
{
    public class ProjectsController
    {
        private string connectionstring = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

        /// <summary>
        /// Created By - SatishT
        /// Create Date - 09Feb2022
        /// Purpose - To Save data for Project Coordinator Allotment
        /// Call From Page - Project_Coordinator_Allotment.aspx.cs
        /// </summary>
        /// <param name="objProjCo"></param>
        /// <returns></returns>
        public int AddProjectCoordinator(Projects_Coordinator objProjCo, string degreeno, string branchno)
        {
            int status = 0;
            try
            {
                SQLHelper objSqlHelper = new SQLHelper(connectionstring);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[7];
                objParams[0] = new SqlParameter("@P_ACADEMIC_SESSION_NO", objProjCo.AcademicSessionNo);
                objParams[1] = new SqlParameter("@P_DEGREENO", degreeno);
                objParams[2] = new SqlParameter("@P_BRANCHNO", branchno);
                objParams[3] = new SqlParameter("@P_FACULTY_NO", objProjCo.FacultyNo);
                objParams[4] = new SqlParameter("@P_COORDINATOR_NO", objProjCo.CoordinatorNo);
                objParams[5] = new SqlParameter("@P_CREATED_BY", objProjCo.CreatedBy);
                objParams[6] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[6].Direction = ParameterDirection.Output;

                object obj = objSqlHelper.ExecuteNonQuerySP("PKG_ACAD_INSERT_PROJECT_COORDINATOR_ALLOTMENT", objParams, true);

                if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001")
                {
                    status = Convert.ToInt32(CustomStatus.RecordSaved);
                }
                else if (obj.ToString() == "-1001")
                {
                    status = Convert.ToInt32(CustomStatus.DuplicateRecord);
                }
                else
                {
                    status = Convert.ToInt32(CustomStatus.Error);
                }
            }
            catch (Exception ex)
            {
                status = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ProjectsController.AddProjectCoordinator() --> " + ex.Message + " " + ex.StackTrace);
            }
            return status;
        }

        /// <summary>
        /// Created By - SatishT
        /// Create Date - 09Feb2022
        /// Purpose - To Get data for Project Coordinator Allotment
        /// Call From Page - Project_Coordinator_Allotment.aspx.cs
        /// </summary>
        /// <param name="objProjCo"></param>
        /// <returns></returns>
        public DataSet GetProjectCoordinatorData()
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionstring);
                SqlParameter[] objParams = new SqlParameter[0];

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_PROJECT_COORDINATOR", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ProjectsController.GetProjectCoordinatorData() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        /// <summary>
        /// Created By - SatishT
        /// Create Date - 09Feb2022
        /// Purpose - To Update data for Project Coordinator Allotment
        /// Call From Page - Project_Coordinator_Allotment.aspx.cs
        /// </summary>
        /// <param name="objProjCo"></param>
        /// <returns></returns>
        public int UpdateProjectCoordinator(Projects_Coordinator objProjCo, string degreeno, string branchno)
        {
            int status = 0;
            try
            {
                SQLHelper objSqlHelper = new SQLHelper(connectionstring);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[8];

                objParams[0] = new SqlParameter("@P_ACADEMIC_SESSION_NO", objProjCo.AcademicSessionNo);
                objParams[1] = new SqlParameter("@P_FACULTY_NO", objProjCo.FacultyNo);
                objParams[2] = new SqlParameter("@P_DEGREENO", degreeno);
                objParams[3] = new SqlParameter("@P_BRANCHNO", branchno);
                objParams[4] = new SqlParameter("@P_COORDINATOR_NO", objProjCo.CoordinatorNo);
                objParams[5] = new SqlParameter("@P_CREATED_BY", objProjCo.CreatedBy);
                objParams[6] = new SqlParameter("@P_ALLOTMENT_NO", objProjCo.AllotmentNo);
                objParams[7] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[7].Direction = ParameterDirection.Output;
                object obj = objSqlHelper.ExecuteNonQuerySP("PKG_ACAD_UPDATE_PROJECT_COORDINATOR_ALLOTMENT", objParams, true);

                if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001")
                    status = Convert.ToInt32(CustomStatus.RecordUpdated);
                else
                    status = Convert.ToInt32(CustomStatus.Error);
            }
            catch (Exception ex)
            {
                status = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ProjectsController.UpdateProjectCoordinator() --> " + ex.Message + " " + ex.StackTrace);
            }
            return status;
        }

        /// <summary>
        /// Created By - SatishT
        /// Create Date - 11022022
        /// Purpose - To Save data for Project Group Creation
        /// Call From Page - Project_Group_Creation.aspx.cs
        /// </summary>
        /// <param name="objProjCo"></param>
        /// <returns></returns>
        public int AddProjectGroupCreation(ProjectGroupCreation objProjGrp, int DEGREENO, int BRANCHNO, string MODULECODE, string PROJECT_CODE, int third_coexaminer)
        {
            int status = 0;
            try
            {
                SQLHelper objSqlHelper = new SQLHelper(connectionstring);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[17];
                objParams[0] = new SqlParameter("@P_ACADEMIC_SESSION_NO", objProjGrp.AcademicSessionNo);
                objParams[1] = new SqlParameter("@P_DEGREENO", DEGREENO);
                objParams[2] = new SqlParameter("@P_BRANHCNO", BRANCHNO);
                objParams[3] = new SqlParameter("@P_FACULTYNO", objProjGrp.FacultyNo);
                objParams[4] = new SqlParameter("@P_MODULENO", objProjGrp.ModuleNo);
                objParams[5] = new SqlParameter("@P_CCODE", MODULECODE);
                objParams[6] = new SqlParameter("@P_PROJECTID", objProjGrp.ProjectId);
                objParams[7] = new SqlParameter("@P_PROJECTTITLE", objProjGrp.ProjectTitle);
                objParams[8] = new SqlParameter("@P_GROUPMEMBERIDS", objProjGrp.GroupMemberIds);
                objParams[9] = new SqlParameter("@P_SUPERVISORID", objProjGrp.SupervisorId);
                objParams[10] = new SqlParameter("@P_COSUPERVISORID", objProjGrp.CoSupervisorId);
                objParams[11] = new SqlParameter("@P_EXAMINERID", objProjGrp.ExaminerId);
                objParams[12] = new SqlParameter("@P_COEXAMINERID", objProjGrp.CoExaminerId);
                objParams[13] = new SqlParameter("@P_CREATED_BY", objProjGrp.CreatedBy);
                objParams[14] = new SqlParameter("@P_PROJECT_CODE", PROJECT_CODE);
                objParams[15] = new SqlParameter("@P_THIRD_CO_EXAMINER", third_coexaminer);
                objParams[16] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[16].Direction = ParameterDirection.Output;

                object obj = objSqlHelper.ExecuteNonQuerySP("PKD_ACD_INSERT_PROJECT_GROUP", objParams, true);

                if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001")
                {
                    status = Convert.ToInt32(CustomStatus.RecordSaved);
                }
                else if (obj.ToString() == "-1001")
                {
                    status = Convert.ToInt32(CustomStatus.DuplicateRecord);
                }
                else
                {
                    status = Convert.ToInt32(CustomStatus.Error);
                }
            }
            catch (Exception ex)
            {
                status = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ProjectsController.AddProjectGroupCreation() --> " + ex.Message + " " + ex.StackTrace);
            }
            return status;
        }

        /// <summary>
        /// Created By - SatishT
        /// Create Date - 11feb2022
        /// Purpose - To Get ProjectID for next Entry
        /// Call From Page - Project_Group_Creation.aspx.cs
        /// </summary>
        /// <returns></returns>
        /// //ADDED BY AASHNA
        public SqlDataReader GetUSERDEATILSFORMODULE(int ua_no)
        {
            SqlDataReader dr = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionstring);
                SqlParameter[] objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_UA_NO", ua_no);
                dr = objSQLHelper.ExecuteReaderSP("PKG_GET_SCHEME_SEMESTER_BY_USERID", objParams);
            }
            catch (Exception ex)
            {
                return dr;
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CourseController.GetShemeSemester-> " + ex.ToString());
            }
            return dr;
        }

        public DataSet GetProjectID()
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionstring);
                SqlParameter[] objParams = new SqlParameter[0];

                ds = objSQLHelper.ExecuteDataSetSP("PKG_GENERATE_PROJECT_ID_FOR_PROJECT_GROUP", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ProjectsController.GetProjectID() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        /// <summary>
        /// Created By - SatishT
        /// Create Date - 11022022
        /// Purpose - To Update data for Project Group Creation
        /// Call From Page - Project_Group_Creation.aspx.cs
        /// </summary>
        /// <param name="objProjGrp"></param>
        /// <returns></returns>
        public int UpdateProjectGroup(ProjectGroupCreation objProjGrp, int DEGREENO, int BRANCHNO, string MODULECODE, string projectcode, int third_coexaminer)
        {
            int status = 0;
            try
            {
                SQLHelper objSqlHelper = new SQLHelper(connectionstring);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[18];
                objParams[0] = new SqlParameter("@P_ACADEMIC_SESSION_NO", objProjGrp.AcademicSessionNo);
                objParams[1] = new SqlParameter("@P_FACULTYNO", objProjGrp.FacultyNo);
                objParams[2] = new SqlParameter("@P_DEGREENO", DEGREENO);
                objParams[3] = new SqlParameter("@P_BRANHCNO", BRANCHNO);
                objParams[4] = new SqlParameter("@P_MODULENO", objProjGrp.ModuleNo);
                objParams[5] = new SqlParameter("@P_CCODE", MODULECODE);
                objParams[6] = new SqlParameter("@P_PROJECTID", objProjGrp.ProjectId);
                objParams[7] = new SqlParameter("@P_PROJECTTITLE", objProjGrp.ProjectTitle);
                objParams[8] = new SqlParameter("@P_GROUPMEMBERIDS", objProjGrp.GroupMemberIds);
                objParams[9] = new SqlParameter("@P_SUPERVISORID", objProjGrp.SupervisorId);
                objParams[10] = new SqlParameter("@P_COSUPERVISORID", objProjGrp.CoSupervisorId);
                objParams[11] = new SqlParameter("@P_EXAMINERID", objProjGrp.ExaminerId);
                objParams[12] = new SqlParameter("@P_COEXAMINERID", objProjGrp.CoExaminerId);
                objParams[13] = new SqlParameter("@P_CREATED_BY", objProjGrp.CreatedBy);
                objParams[14] = new SqlParameter("@P_PROJECT_GROUPNO", objProjGrp.ProjectGroupNo);
                objParams[15] = new SqlParameter("@P_PROJECT_CODE", projectcode);
                objParams[16] = new SqlParameter("@P_THIRD_CO_EXAMINER", third_coexaminer);
                objParams[17] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[17].Direction = ParameterDirection.Output;
                object obj = objSqlHelper.ExecuteNonQuerySP("PKG_ACAD_UPDATE_PROJECT_PROJECT_GROUP", objParams, true);

                if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001")
                    status = Convert.ToInt32(CustomStatus.RecordUpdated);
                else
                    status = Convert.ToInt32(CustomStatus.Error);
            }
            catch (Exception ex)
            {
                status = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ProjectsController.UpdateProjectCoordinator() --> " + ex.Message + " " + ex.StackTrace);
            }
            return status;
        }

        /// <summary>
        /// Created By - SatishT
        /// Create Date - 11FEB2022
        /// Purpose - To Get data for Project Group Creation
        /// Call From Page - Project_Group_Creation.aspx.cs
        /// </summary>
        /// <returns></returns>
        public DataSet GetProjectGroupCreationData()
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionstring);
                SqlParameter[] objParams = new SqlParameter[0];

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_PROJECT_GROUP_DATA", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ProjectsController.GetProjectGroupCreationData() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        /// <summary>
        /// Created By - SatishT
        /// Create Date - 14FEB2022
        /// Purpose - To Get data for Project Management
        /// Call From Page - Project_Management.aspx.cs
        /// </summary>
        /// <returns></returns>
        public DataSet GetProjectManagementData(int courseno, int ua_no)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionstring);
                SqlParameter[] objParams = new SqlParameter[2];
                objParams[0] = new SqlParameter("@P_COURSENO", courseno);
                objParams[1] = new SqlParameter("@P_COORDINATORNO", ua_no);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_PROJECT_MANAGEMENT_DATA_BY_SUBID", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ProjectsController.GetProjectManagementData() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        /// <summary>
        /// Created By - SatishT
        /// Create Date - 15FEB2022
        /// Purpose - To Get data for Project Members
        /// Call From Page - Project_Management.aspx.cs
        /// </summary>
        /// <returns></returns>
        public DataSet GetProjectMembersData(string ProjectGrpId)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionstring);
                SqlParameter[] objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@ProjectGroupId", ProjectGrpId);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_PROJECT_MEMBERS", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ProjectsController.GetProjectMembersData() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        /// <summary>
        /// Save Mark entry for Project
        /// </summary>
        /// <param name="sessionno"></param>
        /// <param name="courseno"></param>
        /// <param name="ccode"></param>
        /// <param name="idnos"></param>
        /// <param name="marks"></param>
        /// <param name="lock_status"></param>
        /// <param name="exam"></param>
        /// <param name="ua_no"></param>
        /// <returns></returns>

        //ADDED BY AASHNA 14-12-2022
        public int UpdateMarkEntryAllExam(int sessionno, int courseno, int Assesmentno, int campusno, string idnos, string marks, string icgrade, string absstud, int ua_no, string assname, string repstatus, string campusnos, string isabsent)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);

            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionstring);
                SqlParameter[] objParams = new SqlParameter[]
                         {
                            //Parameters for MARKS
                            new SqlParameter("@P_SESSIONNO", sessionno),
                            new SqlParameter("@P_COURSENO", courseno),
                            new SqlParameter("@P_ASSESSMENT_NO",Assesmentno),
                            new SqlParameter("@P_CAMPUSNO", campusno),
                            new SqlParameter("@P_IDNOS", idnos),
                            new SqlParameter("@P_MARKS", marks),
                            new SqlParameter("@P_IC_GRADE_STUDS", icgrade),
                            new SqlParameter("@P_ABS_STUDS", absstud),
                            new SqlParameter("@P_CAMPUSNOS", campusnos),
                            new SqlParameter("@P_ENTRY_BY_UANO", ua_no),
                            new SqlParameter("@P_ASSESSMENT_COMPONENT_NAME", assname),
                            new SqlParameter("@P_REPEAT_STATUS", repstatus),
                            new SqlParameter("@P_ISABSENT_PROJECT", isabsent),
                            new SqlParameter("@P_OUT", SqlDbType.Int)
                        };
                objParams[objParams.Length - 1].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_INSERT_PROJECT_GROUP_MARKS", objParams, true);
                if (ret != null && ret.ToString() == "1")
                {
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                }
                else
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.UpdateMarkEntry --> " + ex.ToString());
            }
            return retStatus;
        }

        /// <summary>
        /// Created By - SatishT
        /// Create Date - 21FEB2022
        /// Purpose - To Get data for Project Members Mark entry
        /// Call From Page - Project_Management.aspx.cs
        /// </summary>
        /// <returns></returns>

        public DataSet GetProjectMembersDataMarkEntry(string ProjectGrpId, int ExamNo)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionstring);
                SqlParameter[] objParams = new SqlParameter[2];
                objParams[0] = new SqlParameter("@ProjectGroupId", ProjectGrpId);
                objParams[1] = new SqlParameter("@P_ExamNo", ExamNo);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_PROJECT_MEMBERS_MARK_ENTRY", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ProjectsController.GetProjectMembersData() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        //ADDED BY AASHNA 01-03-2022
        public DataSet GetSTUDENTLISTFORCOORDINATOR(int coordinator)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionstring);
                SqlParameter[] objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_COORDINATORNO", coordinator);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_STUDENT_LIST_FIRCOORDINATO", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ProjectsController.GetProjectManagementData() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        //ADDED BY AASHNA 01-03-2022
        public DataSet GetProjectMarksforhod()
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionstring);
                SqlParameter[] objParams = new SqlParameter[0];
                ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_STUDENT_LIST_FOR_HOD", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ProjectsController.GetProjectManagementData() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        //ADDED BY AASHNA 01-03-2022
        public DataSet GetProjectMarksforEXAMINER()
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionstring);
                SqlParameter[] objParams = new SqlParameter[0];
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_DATA_FOR_EXAMINER", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ProjectsController.GetProjectManagementData() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        //ADDED BY AASHNA 22-02-2022
        public DataSet GetProjectMarkSubData(int sessionno, int courseno, int examno, int degreeno, int branchno)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionstring);
                SqlParameter[] objParams = new SqlParameter[5];
                objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                objParams[1] = new SqlParameter("@P_COURSENO", courseno);
                objParams[2] = new SqlParameter("@P_EXAMNO", examno);
                objParams[3] = new SqlParameter("@P_DEGREENO", degreeno);
                objParams[4] = new SqlParameter("@P_BRANCHNO", branchno);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_PROJECT_MARKS_SUBIMISSION", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ProjectsController.GetProjectManagementData() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        //ADDED BY AASHNA 22-02-2022

        public int InsProjectMarkSubmission(int idno)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);

            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionstring);
                SqlParameter[] objParams = new SqlParameter[]
                        {
                            new SqlParameter("@P_IDNO", idno),
                            new SqlParameter("@P_OUT", SqlDbType.Int)
                        };
                objParams[objParams.Length - 1].Direction = ParameterDirection.Output;
                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INS__PROJECT_MARK_SUBMISSION", objParams, true);
                if (ret != null && ret.ToString() == "2")
                {
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                }
                else
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.UpdateMarkEntry --> " + ex.ToString());
            }
            return retStatus;
        }

        //ADDED BY AASHNA 22-02-2022
        public int InsProjectMarkSubmissionForHOD(int idno)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);

            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionstring);
                SqlParameter[] objParams = new SqlParameter[]
                        {
                            new SqlParameter("@P_IDNO", idno),
                            new SqlParameter("@P_OUT", SqlDbType.Int)
                        };
                objParams[objParams.Length - 1].Direction = ParameterDirection.Output;
                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INS_PROJECT_MARK_SUBMISSION_FORHOD", objParams, true);
                if (ret != null && ret.ToString() == "2")
                {
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                }
                else
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.UpdateMarkEntry --> " + ex.ToString());
            }
            return retStatus;
        }

        public string GetStudentIdnos(ProjectGroupCreation objProjGrp)
        {
            string status = "";
            try
            {
                SQLHelper objSqlHelper = new SQLHelper(connectionstring);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[4];
                objParams[0] = new SqlParameter("@P_ACADEMIC_SESSION_NO", objProjGrp.AcademicSessionNo);
                objParams[1] = new SqlParameter("@P_PROJECT_GROUPNO", objProjGrp.ProjectGroupNo);
                objParams[2] = new SqlParameter("@P_GROUPMEMBERIDS", objProjGrp.GroupMemberIds);
                objParams[3] = new SqlParameter("@P_OUT", SqlDbType.NVarChar, 100);
                objParams[3].Direction = ParameterDirection.Output;
                object obj = objSqlHelper.ExecuteNonQuerySP("PKG_ACAD_GET_STUDIDNOS", objParams, true);
                status = Convert.ToString(obj);
                if (Convert.ToString(obj) != null && Convert.ToString(obj) != "")
                    status = (Convert.ToString(obj));
            }
            catch (Exception ex)
            {
                status = Convert.ToString(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ProjectsController.UpdateProjectCoordinator() --> " + ex.Message + " " + ex.StackTrace);
            }
            return status;
        }

        //adeed by aashna 16-12-2022
        public int UnlockProject(int sessionno, int courseno, string idno)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);

            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionstring);
                SqlParameter[] objParams = new SqlParameter[]
                        {
                            new SqlParameter("@P_SESSIONNO", sessionno),
                            new SqlParameter("@P_COURSENO", courseno),
                            new SqlParameter("@P_STUDIDS", idno),
                            new SqlParameter("@P_OUT", SqlDbType.Int)
                        };
                objParams[objParams.Length - 1].Direction = ParameterDirection.Output;
                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACA_UNLOCK_PROJECT", objParams, true);
                if (ret != null && ret.ToString() == "1")
                {
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                }
                else
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.MarksEntryController.UpdateAuditPoint --> " + ex.ToString());
            }
            return retStatus;
        }

        //ADDED BY AASHNA 14-12-2022
        public int LockMarkEntryAllExam(int sessionno, int courseno, string idnos, string marks, int ua_no, string ccode)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);

            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionstring);
                SqlParameter[] objParams = new SqlParameter[]
                         {
                            //Parameters for MARKS
                            new SqlParameter("@P_SESSIONNO", sessionno),
                            new SqlParameter("@P_COURSENO", courseno),
                            new SqlParameter("@P_CCODE",ccode),
                            new SqlParameter("@P_IDNOS", idnos),
                            new SqlParameter("@P_ENTRY_BY_UANO", ua_no),
                            new SqlParameter("@P_IPADDRESS", marks),
                            new SqlParameter("@P_OUT", SqlDbType.Int)
                        };
                objParams[objParams.Length - 1].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_SP_GRADE_ALLOTMENT_STUD_WISE_SLIIT_PROJECT", objParams, true);
                if (ret != null && ret.ToString() == "1")
                {
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                }
                else
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.UpdateMarkEntry --> " + ex.ToString());
            }
            return retStatus;
        }
    }
}