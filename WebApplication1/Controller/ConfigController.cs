using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;

//======================================================================================
// PROJECT NAME  : UAIMS
// MODULE NAME   : ACADEMIC
// PAGE NAME     : Configuration page
// CREATION DATE : 30-MARCH-2012
// CREATED BY    : ASHISH DHAKATE
// MODIFIED DATE :
// MODIFIED DESC :
//======================================================================================
using System;
using System.Data;
using System.Data.SqlClient;

namespace IITMS.UAIMS.BusinessLayer.BusinessLogic
{
    public class ConfigController
    {
        private string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

        public DataSet GetAllEvents()
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = new SqlParameter[0];

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_EVENT_GET_ALL", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMS.IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ConfigController.GetAllQualifyExam() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public int AddConfig(Config objConfig)
        {
            int status = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_EVENTNO",objConfig.EventNo),
                    new SqlParameter("@P_STATUS", objConfig.Status),
                    new SqlParameter("@P_COLLEGE_CODE",objConfig.CollegeCode),
                    new SqlParameter("@P_CONFIGNO", status)
                };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_INSERT_CONFIG", sqlParams, true);

                if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001")
                    status = Convert.ToInt32(CustomStatus.RecordSaved);
                else
                    status = Convert.ToInt32(CustomStatus.Error);
            }
            catch (Exception ex)
            {
                status = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.BatchController.AddBatch() --> " + ex.Message + " " + ex.StackTrace);
            }
            return status;
        }

        //Added by Pritish S. on 15/12/2020
        public int AddCollegeSchemeConfig(DataTable dtResult)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[2];
                objParams[0] = new SqlParameter("@tblCollegeSchemeMap", dtResult);
                objParams[1] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[1].Direction = ParameterDirection.Output;

                if (objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_INSERT_COLLEGE_SCHEME_CONFIG", objParams, false) != null)
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ConfigController.AddCollegeSchemeConfig -> " + ex.ToString());
            }
            return retStatus;
        }

        // Added by Pritish on 31/12/2020
        public int AddLabelConfig(int recNo, string labelId, string labelName, string collegeid)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[5];
                objParams[0] = new SqlParameter("@P_RECNO", recNo);
                objParams[1] = new SqlParameter("@P_LABELID", labelId);
                objParams[2] = new SqlParameter("@P_LABELNAME", labelName);
                objParams[3] = new SqlParameter("@P_COLLEGEID", collegeid);

                objParams[4] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[4].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_INSERT_LABEL_CONFIG", objParams, true);

                if (Convert.ToInt32(ret) == 1)
                {
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                }
                else if (Convert.ToInt32(ret) == 2)
                {
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                }
                else if (Convert.ToInt32(ret) == 3)
                {
                    retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                }
                else
                {
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                }
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CourseController.UpdateOfferedCourse -> " + ex.ToString());
            }

            return retStatus;
        }

        public DataSet GetOnlineAdmissionConfigurationDetails(int admbatch, string college_id, int ugpg, int commandtype)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[4];
                objParams[0] = new SqlParameter("@P_ADMBATCH", admbatch);
                objParams[1] = new SqlParameter("@P_COLLEGE_ID", college_id);
                objParams[2] = new SqlParameter("@P_UGPG", ugpg);
                objParams[3] = new SqlParameter("@P_COMMAND_TYPE", commandtype);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_ALL_ONLINE_ADMISSION_CONFIG", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMS.IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ConfigController.GetAllQualifyExam() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public int AddOnlineAdmissionConfigDetails(Config objcon, string text, string value, string examtime, string date1, string time1, string date2, string time2)
        {
            //int status = Convert.ToInt32(CustomStatus.Others);
            int status = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = null;

                //Add New Adm
                objParams = new SqlParameter[24];

                objParams[0] = new SqlParameter("@P_DEGREENO", objcon.DegreeNoS);
                objParams[1] = new SqlParameter("@P_BRANCHNO", objcon.BranchNoS);
                objParams[2] = new SqlParameter("@P_FEES", objcon.FeeS);
                objParams[3] = new SqlParameter("@P_START_DATE", objcon.StartDate);
                objParams[4] = new SqlParameter("@P_END_DATE", objcon.EndDate);
                objParams[5] = new SqlParameter("@P_ADMBATCH", objcon.Admbatch);
                //objParams[6] = new SqlParameter("@P_COLLEGE_ID", objcon.College_id);
                objParams[6] = new SqlParameter("@P_COLLEGE_IDS", objcon.College_ids); //added by swapnil thakare on dated 23-09-2021
                objParams[7] = new SqlParameter("@P_CONFIG_ID", objcon.ConfigIDS);
                objParams[8] = new SqlParameter("@P_UGPG", objcon.UGPG);
                objParams[9] = new SqlParameter("@P_WEEKDAYS", objcon.chkWeek);
                objParams[10] = new SqlParameter("@P_CAMPUSNOS", objcon.CampusNos);
                objParams[11] = new SqlParameter("@P_INTAKES", objcon.Intakes);
                objParams[12] = new SqlParameter("@P_UANO", objcon.Uano);
                objParams[13] = new SqlParameter("@P_EXAMDATE", objcon.ExamDate);
                objParams[14] = new SqlParameter("@P_MODE", objcon.Mode);
                objParams[15] = new SqlParameter("@P_MEDIUM", objcon.Medium);
                objParams[16] = new SqlParameter("@P_CAMPUS_NO", text);
                objParams[17] = new SqlParameter("@P_CAPACITY", value);
                objParams[18] = new SqlParameter("@P_EXAM_TIME", examtime);

                objParams[19] = new SqlParameter("@P_ADMEXAMDATE_FIRST", date1);
                objParams[20] = new SqlParameter("@P_ADMEXAMTIME_FIRST", time1);
                objParams[21] = new SqlParameter("@P_ADMEXAMDATE_SECOND", date2);
                objParams[22] = new SqlParameter("@P_ADMEXAMTIME_SECOND", time2);

                objParams[23] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                objParams[23].Direction = ParameterDirection.Output;

                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_ADD_ONLINE_ADMISSION_CONFIG", objParams, true);

                if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001")
                    status = Convert.ToInt32(CustomStatus.RecordSaved);
                else
                    status = Convert.ToInt32(CustomStatus.Error);
            }
            catch (Exception ex)
            {
                status = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Common.AddSession-> " + ex.ToString());
            }
            return status;
        }

        public DataSet GetInterViewDetails(int IntakeNo, string StudyLevelNos, string degreeno, string branchno, string InterViewDate, string interviewTime)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = new SqlParameter[6];
                objParams[0] = new SqlParameter("@P_INTAKE", IntakeNo);
                objParams[1] = new SqlParameter("@P_STUDYLEVELNOS", StudyLevelNos);
                objParams[2] = new SqlParameter("@P_DEGREENO", degreeno);
                objParams[3] = new SqlParameter("@P_BRANCHNO", branchno);
                objParams[4] = new SqlParameter("@P_INTERVIEWDATE", InterViewDate);
                objParams[5] = new SqlParameter("@P_INTERVIEWTIME", interviewTime);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_INTERVIEW_SCHEDULE_DETAILS", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMS.IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ConfigController.GetInterViewDetails() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public int InsertInterviewDetails(int IntakeNo, string ProgramNos, string UserNos, string InterviewDate, string InterviewTime, string Venue, string Status, string Remarks, int uano, int CommandType)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[11];
                objParams[0] = new SqlParameter("@P_INTAKENO", IntakeNo);
                objParams[1] = new SqlParameter("@P_PROGRAMNOS", ProgramNos);
                objParams[2] = new SqlParameter("@P_USERNOS", UserNos);
                objParams[3] = new SqlParameter("@P_INTERVIEWDATE", InterviewDate);
                objParams[4] = new SqlParameter("@P_INTERVIEWTIME", InterviewTime);
                objParams[5] = new SqlParameter("@P_VENUE", Venue);
                objParams[6] = new SqlParameter("@P_STATUS", Status);
                objParams[7] = new SqlParameter("@P_REMARKS", Remarks);
                objParams[8] = new SqlParameter("@P_UANO", uano);
                objParams[9] = new SqlParameter("@P_COMMANDTYPE", CommandType);
                objParams[10] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[10].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INSERT_INTERVIEW_DETAILS", objParams, true);

                if (Convert.ToInt32(ret) == 1)
                {
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                }
                else if (Convert.ToInt32(ret) == 2)
                {
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                }
                else
                {
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                }
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CourseController.UpdateOfferedCourse -> " + ex.ToString());
            }

            return retStatus;
        }

        public int AddServiceProvider(int Configno, String ConfigName, string ServiceProviderName, int ServiceNo)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[5];

                objParams[0] = new SqlParameter("@P_CONFIGNO", Configno);
                objParams[1] = new SqlParameter("@P_CONFIGNAME", ConfigName);
                objParams[2] = new SqlParameter("@P_SERVICEPROVIDERNAME", ServiceProviderName);
                objParams[3] = new SqlParameter("@P_SERVICENO", ServiceNo);
                objParams[4] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[4].Direction = ParameterDirection.Output;
                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_INSERT_SERVICE_PROVIDER_NAME", objParams, true);

                if (Convert.ToInt32(ret) == 1)
                {
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                }
                else if (Convert.ToInt32(ret) == 2)
                {
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                }
                else
                {
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                }
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CourseController.UpdateOfferedCourse -> " + ex.ToString());
            }
            return retStatus;
        }

        public int AddEmailServiceProvider(int EMAIL_NO, int ServiceNo, string SMTP_Server, string SMTP_Server_Port, string CKey_UserId, string Email_ID, string Password, int Active, int UaNo)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[10];

                objParams[0] = new SqlParameter("@P_EMIAL_NO", EMAIL_NO);
                objParams[1] = new SqlParameter("@P_SERVICE_NO", ServiceNo);
                objParams[2] = new SqlParameter("@P_SMTP_SERVER", SMTP_Server);
                objParams[3] = new SqlParameter("@P_SMTP_PORT", SMTP_Server_Port);
                objParams[4] = new SqlParameter("@P_CKEY_USERID", CKey_UserId);
                objParams[5] = new SqlParameter("@P_EMAILID", Email_ID);
                objParams[6] = new SqlParameter("@P_PASSWORDS", Password);
                objParams[7] = new SqlParameter("@P_STATUS", Active);
                objParams[8] = new SqlParameter("@P_UANO", UaNo);
                objParams[9] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[9].Direction = ParameterDirection.Output;
                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_EMAIL_SERVICE_PROVIDER_CONFIGURATION", objParams, true);

                if (Convert.ToInt32(ret) == 1)
                {
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                }
                else if (Convert.ToInt32(ret) == 2)
                {
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                }
                else
                {
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                }
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CourseController.UpdateOfferedCourse -> " + ex.ToString());
            }
            return retStatus;
        }

        public int AddSMSServiceProvider(int SMS_NO, int ServiceNo, string SMSAPI, string SMSUserID, string EmailSMS, string PasswordSMS, string SMSParameterI, string SMSParameterII, int Active, int UaNo)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[11];

                objParams[0] = new SqlParameter("@P_SMS_NO", SMS_NO);
                objParams[1] = new SqlParameter("@P_SERVICE_NO", ServiceNo);
                objParams[2] = new SqlParameter("@P_SMSAPI", SMSAPI);
                objParams[3] = new SqlParameter("@P_SMSUSERID", SMSUserID);
                objParams[4] = new SqlParameter("@P_EMAILSMS", EmailSMS);
                objParams[5] = new SqlParameter("@P_PASSWORDSMS", PasswordSMS);
                objParams[6] = new SqlParameter("@P_SMSPARAMETERI", SMSParameterI);
                objParams[7] = new SqlParameter("@P_SMSPARAMETERII", SMSParameterII);
                objParams[8] = new SqlParameter("@P_STATUS", Active);
                objParams[9] = new SqlParameter("@P_UANO", UaNo);
                objParams[10] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[10].Direction = ParameterDirection.Output;
                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_SMS_SERVICE_PROVIDER_CONFIGURATION", objParams, true);

                if (Convert.ToInt32(ret) == 1)
                {
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                }
                else if (Convert.ToInt32(ret) == 2)
                {
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                }
                else
                {
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                }
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CourseController.UpdateOfferedCourse -> " + ex.ToString());
            }
            return retStatus;
        }

        public int AddWhatsappServiceProvider(int WhatsAAP_NO, int ServiceNo, string API_URL, string Token, string MobileNo, string Account_SID, string UserName, int Active, int UaNo)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[10];
                objParams[0] = new SqlParameter("@P_WHATSAAP_NO", WhatsAAP_NO);
                objParams[1] = new SqlParameter("@P_SERVICE_NO", ServiceNo);
                objParams[2] = new SqlParameter("@P_API_URL", API_URL);
                objParams[3] = new SqlParameter("@P_TOKEN", Token);
                objParams[4] = new SqlParameter("@P_MOBILENO", MobileNo);
                objParams[5] = new SqlParameter("@P_USERNAME", UserName);
                objParams[6] = new SqlParameter("@P_ACCOUNT_SID", Account_SID);
                objParams[7] = new SqlParameter("@P_STATUS", Active);
                objParams[8] = new SqlParameter("@P_UANO", UaNo);
                objParams[9] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[9].Direction = ParameterDirection.Output;
                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_WHATSAAP_SERVICE_PROVIDER_CONFIGURATION", objParams, true);

                if (Convert.ToInt32(ret) == 1)
                {
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                }
                else if (Convert.ToInt32(ret) == 2)
                {
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                }
                else
                {
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                }
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CourseController.UpdateOfferedCourse -> " + ex.ToString());
            }
            return retStatus;
        }

        public int AddServiceProvider(int Configno, String ConfigName, string ServiceProviderName, int ServiceNo, string Parameter)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[6];

                objParams[0] = new SqlParameter("@P_CONFIGNO", Configno);
                objParams[1] = new SqlParameter("@P_CONFIGNAME", ConfigName);
                objParams[2] = new SqlParameter("@P_SERVICEPROVIDERNAME", ServiceProviderName);
                objParams[3] = new SqlParameter("@P_SERVICENO", ServiceNo);
                objParams[4] = new SqlParameter("@P_PARAMETER", Parameter);

                objParams[5] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[5].Direction = ParameterDirection.Output;
                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_INSERT_SERVICE_PROVIDER_NAME", objParams, true);

                if (Convert.ToInt32(ret) == 1)
                {
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                }
                else if (Convert.ToInt32(ret) == 2)
                {
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                }
                else
                {
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                }
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CourseController.UpdateOfferedCourse -> " + ex.ToString());
            }
            return retStatus;
        }

        public string DynamicSPCall_IUD(string SP_Name, string SP_Parameters, string Call_Values, DataTable D_T, bool T_F, int OutPut)
        {
            string retStatus = "0";
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                int PrmCnt = 0;

                if (T_F)
                {
                    if (OutPut == 1)
                    {
                        PrmCnt = SP_Parameters.Split(',').Length;
                    }
                    else if (OutPut == 2)
                    {
                        PrmCnt = SP_Parameters.Split(',').Length + 1;
                        SP_Parameters += ",Parameter1";
                        Call_Values += ",0";
                    }
                }
                else
                {
                    PrmCnt = SP_Parameters.Split(',').Length;
                }

                SqlParameter[] objParams = new SqlParameter[PrmCnt];

                for (int i = 0; i < PrmCnt; i++)
                {
                    if (i == 0)
                    {
                        objParams[i] = new SqlParameter(SP_Parameters.Split(',')[i].Trim(), D_T);
                    }
                    else
                    {
                        objParams[i] = new SqlParameter(SP_Parameters.Split(',')[i].Trim(), Call_Values.Split(',')[i].Trim());
                    }
                }

                if (T_F)
                {
                    if (OutPut == 1)
                    {
                        objParams[PrmCnt - 1].Direction = ParameterDirection.Output;
                    }
                    else if (OutPut == 2)
                    {
                        objParams[PrmCnt - 1] = new SqlParameter();
                        objParams[PrmCnt - 1].Direction = ParameterDirection.ReturnValue;
                    }
                }

                retStatus = Convert.ToString(objSQLHelper.ExecuteNonQuerySP(SP_Name, objParams, T_F));
            }
            catch (Exception ex)
            {
                retStatus = "-99";
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetCourseForTeacher-> " + ex.ToString());
            }
            return retStatus;
        }

        public int InsertRecord(int id, string title, string detail, int Session, int status)
        {
            int i = 0;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(connectionString);

                SqlParameter[] obj = new SqlParameter[5];
                obj[0] = new SqlParameter("@INSTRUCTION_ID", id);
                obj[1] = new SqlParameter("@INSTRUCTION_TITLE", title);
                obj[2] = new SqlParameter("@INSTRUCTION_DETAIL", detail);
                //obj[3] = new SqlParameter("@date");
                obj[3] = new SqlParameter("@UA_NO", Session);
                obj[4] = new SqlParameter("@STATUS", status);

                if (objDataAccess.ExecuteNonQuerySP("ACD_INSERT_INSTRUCTION_RECORD", obj, true) != null)
                    return i;
            }
            catch (Exception)
            {
            }
            return i;
        }

        public DataSet BindRecord()
        {
            DataSet ds = null;
            // int i = 0;
            try
            {
                SQLHelper sf = new SQLHelper(connectionString);
                SqlParameter[] obj = null;
                obj = new SqlParameter[0];
                ds = sf.ExecuteDataSetSP("PKG_GET_INSTRUCTION_DATA", obj);
                //if (sf.ExecuteDataSetSP("bind_record", obj) != null)
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
            return ds;
        }

        public DataSet Record_Active()
        {
            DataSet ds = null;
            // int i = 0;
            try
            {
                SQLHelper sf = new SQLHelper(connectionString);
                SqlParameter[] obj = null;
                obj = new SqlParameter[0];
                ds = sf.ExecuteDataSetSP("PKG_GET_ACTIVE_USERINSTRUCTION", obj);
                //if (sf.ExecuteDataSetSP("bind_record", obj) != null)
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
            return ds;
        }

        public int DeleteData(int Id)
        {
            int retStatus = Convert.ToInt32(IITMS.UAIMS.CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[2];
                {
                    objParams[0] = new SqlParameter("@INSTRUCTION_ID", Id);
                    objParams[1] = new SqlParameter("@P_OUT", SqlDbType.Int);
                    objParams[1].Direction = ParameterDirection.Output;
                };
                object ret = objSQLHelper.ExecuteNonQuerySP("[dbo].[ACD_REMOVE_INSTRUCTION]", objParams, false);

                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(IITMS.UAIMS.CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(IITMS.UAIMS.CustomStatus.RecordDeleted);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLogicLayer.BusinessLogic.DeleteData-> " + ex.ToString());
            }
            return retStatus;
        }
    }
}