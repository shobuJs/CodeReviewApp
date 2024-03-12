using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using System;
using System.Data;
using System.Data.SqlClient;

namespace IITMS.UAIMS.BusinessLayer.BusinessLogic
{
    public class DocumentControllerAcad
    {
        private string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

        public int AddDocument(DocumentAcad objDocument)
        {
            int status = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_DOCUMENT_NAME",objDocument.Documentname),
                    new SqlParameter("@P_DEGREE", objDocument.Degree),
                    new SqlParameter("@P_COLLEGE_CODE", objDocument.CollegeCode),
                    new SqlParameter("@P_PTYPE", objDocument.Ptype),
                    new SqlParameter("@P_DOCUMENTNO", status)
                };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_DOCUMENT_INSERT", sqlParams, true);

                if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001")
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

        public int UpdateDocument(DocumentAcad objDocument)
        {
            int status = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                   new SqlParameter("@P_DOCUMENT_NAME",objDocument.Documentname),
                    new SqlParameter("@P_DEGREE", objDocument.Degree),
                    new SqlParameter("@P_COLLEGE_CODE", objDocument.CollegeCode),
                    new SqlParameter("@P_PTYPE", objDocument.Ptype),
                    new SqlParameter("@P_DOCUMENTNO",objDocument.Documentno)
                };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_DOCUMENT_UPDATE", sqlParams, true);

                if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001")
                    status = Convert.ToInt32(CustomStatus.RecordUpdated);
                else
                    status = Convert.ToInt32(CustomStatus.Error);
            }
            catch (Exception ex)
            {
                status = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.DocumentController.UpdateDocument() --> " + ex.Message + " " + ex.StackTrace);
            }
            return status;
        }

        public DataSet GetAllDocument()
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = new SqlParameter[0];

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_DOCUMENT_GET_ALL", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.DocumentController.GetAllDocument() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public SqlDataReader GetDocumentByNo(int documentNo)
        {
            SqlDataReader dr = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = new SqlParameter[] { new SqlParameter("@P_DOCUMENTNO", documentNo) };

                dr = objSQLHelper.ExecuteReaderSP("PKG_ACAD_DOCUMENT_GET_BY_NO", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.DocumentController.GetDocumentByNo() --> " + ex.Message + " " + ex.StackTrace);
            }
            return dr;
        }

        public DataSet GetDoclistStud(int userno)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_USERNO", userno);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_DOCUMENTENTRY_SEL_stud", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.DocumentController.GetDoclist() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public int AddDetails(DocumentAcad objDocument, int undertaking, int undertaking1, byte[] photo, int idno)
        {
            int status = -99;
            try
            {
                if (!(photo == null))
                {
                    SQLHelper objSQLHelper = new SQLHelper(connectionString);
                    SqlParameter[] sqlParams = new SqlParameter[]
                            {
                                 new SqlParameter("@P_USERNO",objDocument.USERNO),
                                 new SqlParameter("@P_DEGREENO", objDocument.Degree),
                                 new SqlParameter("@P_COLLEGE_ID",objDocument.CollegeCode )  ,
                                 new SqlParameter("@P_BRANCHNO",objDocument.BRANCHNO )  ,
                                 new SqlParameter("@P_PREFERENCE",objDocument.PREFERENCE )  ,
                                 new SqlParameter("@P_UNDERTAKING",undertaking)  ,
                                 new SqlParameter("@P_UNDERTAKING1", undertaking1),
                                 new SqlParameter("@P_PHOTO", photo),
                                 new SqlParameter("@P_IDNO", idno),
                                 new SqlParameter("@P_OUTPUT",status)
                            };
                    sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

                    object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_applied_stud", sqlParams, true);
                    status = (Int32)obj;
                }
            }
            catch (Exception ex)
            {
                status = -99;
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.UpdateChallanDetails() --> " + ex.Message + " " + ex.StackTrace);
            }
            return status;
        }

        public int AddDocStd(DocumentAcad objDocuments)
        {
            int status = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_USERNO",objDocuments.USERNO),
                    new SqlParameter("@P_DOCNO", objDocuments.DOCNO),
                    new SqlParameter("@P_DOCNAME",objDocuments.DOCNAME ),
                    new SqlParameter("@P_PATH", objDocuments.PATH ),
                    new SqlParameter("@P_FILENAME ",objDocuments.FILENAME ),
                  // new SqlParameter("@P_FILENAME ",undertaking )
                };
                //sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_DOCUMENTENTRY_INS_stud", sqlParams, true);

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

        public DataSet getModuleOfferedCourses(int idno)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_IDNO", idno);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_OFFERED_COURSE_LIST", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.DocumentController.GetDoclist() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public int InsertModuleRegistration(int idno, string courseNos, string Ccodes, string CourseNames, string Credits, string Subids, string licno)
        {
            int status = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_IDNO",idno),
                    new SqlParameter("@P_COURSENOS",courseNos),
                    new SqlParameter("@P_CCODES",Ccodes),
                    new SqlParameter("@P_COURSENAMES",CourseNames),
                    new SqlParameter("@P_CREDITS",Credits),
                    new SqlParameter("@P_SUBIDS",Subids),
                    new SqlParameter("@P_LIC_UANO",licno),
                    new SqlParameter("@P_OUTPUT",status)
                };
                //sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INSERT_MODULE_REGISTRATION_DATA", sqlParams, true);

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

        //Added by: Yograj Chaple on 09-03-2022
        public int InsertModuleRegistrationAdmin(int idno, string courseNos, string Ccodes, string CourseNames, string Credits, string Subids, string licno, int sessionno)
        {
            int status = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_IDNO",idno),
                    new SqlParameter("@P_COURSENOS",courseNos),
                    new SqlParameter("@P_CCODES",Ccodes),
                    new SqlParameter("@P_COURSENAMES",CourseNames),
                    new SqlParameter("@P_CREDITS",Credits),
                    new SqlParameter("@P_SUBIDS",Subids),
                    new SqlParameter("@P_LIC_UANO",licno),
                    new SqlParameter("@P_SESSIONNO",sessionno),
                    new SqlParameter("@P_OUTPUT",status)
                };
                //sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INSERT_MODULE_REGISTRATION_DATA_ADMIN", sqlParams, true);

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

        public int AddMultipleDocStd(int idno, int userno, string FileNames, string docnos, string docnames, string path, int Sessionno, byte[] ChallanCopy)
        {
            int status = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[9];
                objParams[0] = new SqlParameter("@P_IDNO", idno);
                objParams[1] = new SqlParameter("@P_USERNO", userno);
                objParams[2] = new SqlParameter("@P_DOCNOS", docnos);
                objParams[3] = new SqlParameter("@P_DOCNAMES", docnames);
                objParams[4] = new SqlParameter("@P_PATH", path);
                objParams[5] = new SqlParameter("@P_FILENAMES", FileNames);
                objParams[6] = new SqlParameter("@P_SESSIONNO", Sessionno);
                objParams[7] = new SqlParameter("@P_STUDENT_PHOTO", ChallanCopy);
                objParams[8] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                objParams[8].Direction = ParameterDirection.Output; object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_DOCUMENTENTRY_INS_MULTIPLE_DOCUMENT", objParams, true); if (obj.ToString() == "1")
                    status = Convert.ToInt32(CustomStatus.RecordSaved);
                else if (obj.ToString() == "2")
                    status = Convert.ToInt32(CustomStatus.RecordUpdated);
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

        public int UpdateDocumentStatus(int userno, string docnos, string docnames, string Status, string Remarks, int ua_no)
        {
            int status = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[7];
                objParams[0] = new SqlParameter("@P_USERNO", userno);
                objParams[1] = new SqlParameter("@P_DOCNOS", docnos);
                objParams[2] = new SqlParameter("@P_DOCNAMES", docnames);
                objParams[3] = new SqlParameter("@P_STATUS", Status);
                objParams[4] = new SqlParameter("@P_REMARKS", Remarks);
                objParams[5] = new SqlParameter("@P_UA_NO", ua_no);
                objParams[6] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                objParams[6].Direction = ParameterDirection.Output;

                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_UPDATE_DOCUMENT_STATUS", objParams, true);

                if (obj.ToString() == "1")
                    status = Convert.ToInt32(CustomStatus.RecordSaved);
                else if (obj.ToString() == "2")
                    status = Convert.ToInt32(CustomStatus.RecordUpdated);
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

        public DataSet GetDoclistByAdmin(int userno)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_USERNO", userno);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_STUDENT_UPLOADED_DOCUMENT", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.DocumentController.GetDoclist() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public DataSet getOfferAcceptanceDetails(int userno, int Command_Type, int CampusNo)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[3];
                objParams[0] = new SqlParameter("@P_USERNO", userno);
                objParams[1] = new SqlParameter("@P_COMMAND_TYPE", Command_Type);
                objParams[2] = new SqlParameter("@P_CAMPUSNO", CampusNo);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_OFFER_ACCEPTANCE_DETAILS", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.DocumentController.GetDoclist() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public int UpdateFinalUserConformation(int Userno, int CampusNo, int WeekDayNo, int ua_no)
        {
            int status = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[5];
                objParams[0] = new SqlParameter("@P_USERNO", Userno);
                objParams[1] = new SqlParameter("@P_CAMPUSNO", CampusNo);
                objParams[2] = new SqlParameter("@P_WEEKDAYNO", WeekDayNo);
                objParams[3] = new SqlParameter("@P_UA_NO", ua_no);
                objParams[4] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                objParams[4].Direction = ParameterDirection.Output;

                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_UPDATE_STUDENT_FINAL_STATUS", objParams, true);

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

        public DataSet getStudentEligiblityDetails(int userno, int admbatch, int degreeno, int branchno, int collegeid)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[5];
                objParams[0] = new SqlParameter("@P_IDNO", userno);
                objParams[1] = new SqlParameter("@P_ADMBATCH", admbatch);
                objParams[2] = new SqlParameter("@P_DEGREENO", degreeno);
                objParams[3] = new SqlParameter("@P_BRANCHNO", branchno);
                objParams[4] = new SqlParameter("@P_COLLEGE_ID", collegeid);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_RULE_FOR_CHECK_ELIGIBLITY_Snehak", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.DocumentController.GetDoclist() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        //Coded By : Mahesh Malve => 16-Jan-2021
        public DataSet GetModuleOfferedCoursesForSemesterRegistration(int idno, int Sessionno, int uatype)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[3];
                objParams[0] = new SqlParameter("@P_IDNO", idno);
                objParams[1] = new SqlParameter("@P_SESSIONNO", Sessionno);
                objParams[2] = new SqlParameter("@P_UA_TYPE", uatype);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_OFFERED_COURSE_LIST_FOR_SEMESTER_REG", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.DocumentControllerAcad.GetModuleOfferedCoursesForSemesterRegistration() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public DataSet GetModuleOfferedCoursesForSemesterRegistration(int idno, int Sessionno, int Semesterno, int commandtype)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[4];
                objParams[0] = new SqlParameter("@P_IDNO", idno);
                objParams[1] = new SqlParameter("@P_SESSIONNO", Sessionno);
                objParams[2] = new SqlParameter("@P_SEMESTERNO", Semesterno);
                objParams[3] = new SqlParameter("@P_COMMAND_TYPE", commandtype);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_DYNAMIC_SECTION", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.DocumentControllerAcad.GetModuleOfferedCoursesForSemesterRegistration() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public int InsertModuleRegistrationHigherSemester(int idno, string courseNos, string Ccodes, string CourseNames, string Credits, string Subids, string licno, string sectionno, int sessionno, string Amounts)
        {
            int status = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_IDNO",idno),
                    new SqlParameter("@P_COURSENOS",courseNos),
                    new SqlParameter("@P_CCODES",Ccodes),
                    new SqlParameter("@P_COURSENAMES",CourseNames),
                    new SqlParameter("@P_CREDITS",Credits),
                    new SqlParameter("@P_SUBIDS",Subids),
                    new SqlParameter("@P_LIC_UANO",licno),
                    new SqlParameter("@P_SECTIONNOS",sectionno),
                    new SqlParameter("@P_SESSIONNO",sessionno),
                    new SqlParameter("@P_AMOUNTS",Amounts),
                    new SqlParameter("@P_OUTPUT",SqlDbType.Int)
                };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;

                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INSERT_MODULE_REGISTRATION_DATA_HIGHER_SEMESTER", sqlParams, true);

                if (obj != null && obj.ToString() == "5")
                    status = Convert.ToInt32(CustomStatus.RecordNotFound);
                else if (obj != null && obj.ToString() != "-99")
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

        //Added by: Yograj Chaple on 09-03-2022
        public DataSet GetModuleOfferedCoursesForSemesterRegistrationAdmin(int idno, int sessionno)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[2];
                objParams[0] = new SqlParameter("@P_IDNO", idno);
                objParams[1] = new SqlParameter("@P_SESSIONNO", sessionno);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_OFFERED_COURSE_LIST_FOR_SEMESTER_REG_ADMIN", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.DocumentControllerAcad.GetModuleOfferedCoursesForSemesterRegistration() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public DataSet GetAllWidrawlStudentDetails(int idno, int srno, int RDselectedvalue, string ToDate, string FromDate, int pageType, int Command_Type, string username)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[8];
                objParams[0] = new SqlParameter("@P_IDNO", idno);
                objParams[1] = new SqlParameter("@P_SRNO", srno);
                objParams[2] = new SqlParameter("@P_RADIO_BUTTON_FILTER", RDselectedvalue);
                objParams[3] = new SqlParameter("@P_TO_DATE_FILTER", ToDate);
                objParams[4] = new SqlParameter("@P_FROM_DATE_FILTER", FromDate);
                objParams[5] = new SqlParameter("@P_PAGE_TYPE", pageType);
                objParams[6] = new SqlParameter("@P_COMMAND_TYPE", Command_Type);
                objParams[7] = new SqlParameter("@P_USERNAME", username);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_WIDRAWL_POSTPONEMENT_REFOUND_DETAILS", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.DocumentControllerAcad.GetModuleOfferedCoursesForSemesterRegistration() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public int UpdateWithdrawlPostponementRefundDetails(int idno, int srno, int IfPossible, int IfAccepted, string Remark, int PageType, int ua_no)
        {
            int status = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[8];
                objParams[0] = new SqlParameter("@P_IDNO", idno);
                objParams[1] = new SqlParameter("@P_SRNO", srno);
                objParams[2] = new SqlParameter("@P_IFPOSSIBLE", IfPossible);
                objParams[3] = new SqlParameter("@P_IFACCEPTED", IfAccepted);
                objParams[4] = new SqlParameter("@P_REMARK", Remark);
                objParams[5] = new SqlParameter("@P_PAGETYPE", PageType);
                objParams[6] = new SqlParameter("@P_UA_NO", ua_no);
                objParams[7] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                objParams[7].Direction = ParameterDirection.Output;

                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_UPDATE_WITHDRAWL_POSTPONEMENT_REFUND_DETAILS", objParams, true);

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

        public DataSet GetUserStatusFlag(int userno, int idno)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[2];
                objParams[0] = new SqlParameter("@P_USERNO", userno);
                objParams[1] = new SqlParameter("@P_IDNO", idno);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_ALL_STATUS_WISE_FLAG_DETAILS", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.DocumentController.GetDoclist() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public DataSet GetCalculateData(string username, int status, int srno, int policy_id)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[4];
                objParams[0] = new SqlParameter("@P_USERNAME", username);
                objParams[1] = new SqlParameter("@P_STATUS_NO", status);
                objParams[2] = new SqlParameter("@P_SRNO", srno);
                objParams[3] = new SqlParameter("@P_POLICY_ID", policy_id);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_CALCULATED_DATA", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.DocumentControllerAcad.GetCalculateData() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public int UpdateWithdrawlFINANCEDetails(int idno, int srno, int IfPossible, int IfAccepted, string Remark, int ua_no, int refundamount, string trans_detail, string calculateamt,
            string des_by_admin, int statusbyfinance, decimal REFUNDDCRAMT, string dcr, decimal total_refund, int SEMESTER, int STATUS)
        {
            int status = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[17];
                objParams[0] = new SqlParameter("@P_IDNO", idno);
                objParams[1] = new SqlParameter("@P_SRNO", srno);
                objParams[2] = new SqlParameter("@P_IFPOSSIBLE", IfPossible);
                objParams[3] = new SqlParameter("@P_IFACCEPTED", IfAccepted);
                objParams[4] = new SqlParameter("@P_REMARK", Remark);
                objParams[5] = new SqlParameter("@P_UA_NO", ua_no);
                objParams[6] = new SqlParameter("@P_REFUND_AMOUNT", refundamount);
                objParams[7] = new SqlParameter("@P_UPLOAD_TRANSACTION_DETAIL", trans_detail);
                objParams[8] = new SqlParameter("@P_CALCULATED_AMOUNT", calculateamt);
                objParams[9] = new SqlParameter("@P_DESCRIPTION_BY_ADMIN", des_by_admin);
                objParams[10] = new SqlParameter("@P_STATS_BY_FINANCE", statusbyfinance);
                objParams[11] = new SqlParameter("@P_DCR_REFUND_AMOUNT", REFUNDDCRAMT);
                objParams[12] = new SqlParameter("@P_DCR_NO", dcr);
                objParams[13] = new SqlParameter("@P_TOTAL_REFUND_AMOUNT", total_refund);
                objParams[14] = new SqlParameter("@P_STATUSNO", STATUS);
                objParams[15] = new SqlParameter("@P_SEMESTERNO", SEMESTER);
                objParams[16] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                objParams[16].Direction = ParameterDirection.Output;

                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_UPDATE_WITHDRAWL_FINANCE_DETAILS", objParams, true);

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

        public int InsertScholorshipAmount(int idno, decimal scholarship_amount, decimal amount, int uano, int sessionno, int commandtype)
        {
            int status = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_IDNO",idno),
                    new SqlParameter("@P_WALLET_AMOUNT",scholarship_amount),
                    new SqlParameter("@P_PAID_AMOUNT",amount),
                    new SqlParameter("@P_UA_NO",uano),
                    new SqlParameter("@P_SESSIONNO",sessionno),
                    new SqlParameter("@P_COMMAND_TYPE",commandtype),
                    new SqlParameter("@P_OUTPUT",status)
                };
                //sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INSERT_SCHOLORSHIP_DETAILS", sqlParams, true);

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

        public DataSet getProrataOfferedCourseList(string idno, int sessionno, string semesterno, string FullCourseCount, int FinalCourseCount, int MaxRegCount, int PaymentOption, string Credits, int MaxModule, int CA_Validity, string ccodes, string moduletype, int commandtype)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[13];
                objParams[0] = new SqlParameter("@P_IDNO", idno);
                objParams[1] = new SqlParameter("@P_SESSIONNO", sessionno);
                objParams[2] = new SqlParameter("@P_SEMESTERNO", semesterno);
                objParams[3] = new SqlParameter("@P_FULLCOURSECOUNT", FullCourseCount);
                objParams[4] = new SqlParameter("@P_FINALCOURSECOUNT", FinalCourseCount);
                objParams[5] = new SqlParameter("@P_MAXREGCOUNT", MaxRegCount);
                objParams[6] = new SqlParameter("@P_PAYMENTOPTION", PaymentOption);
                objParams[7] = new SqlParameter("@P_CREDITS", Credits);
                objParams[8] = new SqlParameter("@P_MAXMODULE", MaxModule);
                objParams[9] = new SqlParameter("@P_CA_VALIDITY_SESSIONNO", CA_Validity);
                objParams[10] = new SqlParameter("@P_COMMAND_TYPE", commandtype);
                objParams[11] = new SqlParameter("@P_CCODES", ccodes);
                objParams[12] = new SqlParameter("@P_MODULETYPE", moduletype);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_PRORATA_OFFERED_COURSE_LIST", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.DocumentControllerAcad.GetModuleOfferedCoursesForSemesterRegistration() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public int InsertUpdateProrataModuleRegistration(int idno, string courseNos, string Ccodes, string CourseNames, string Credits, string amount, string Subids, string ddlModuletype, string semesternos, int sessionno, int uano, string Intermark, string old_courseno)
        {
            int status = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_IDNO",idno),
                    new SqlParameter("@P_COURSENOS",courseNos),
                    new SqlParameter("@P_CCODES",Ccodes),
                    new SqlParameter("@P_COURSENAMES",CourseNames),
                    new SqlParameter("@P_CREDITS",Credits),
                    new SqlParameter("@P_AMOUNT",amount),
                    new SqlParameter("@P_SUBIDS",Subids),
                    new SqlParameter("@P_DDLMODULETYPE",ddlModuletype),
                    new SqlParameter("@P_SEMESTERNOS",semesternos),
                    new SqlParameter("@P_SESSIONNO",sessionno),
                    new SqlParameter("@P_UANO",uano),
                    new SqlParameter("@P_INTERMARK",Intermark),
                    new SqlParameter("@P_OLD_COURSENO",old_courseno),
                    new SqlParameter("@P_OUTPUT",status)
                };
                //sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INSERT_UPDATE_PRORATA_MODULE_REGISTRATION", sqlParams, true);

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

        public int UpdateStudentBranch(int idno, int semesterno, int branchno, int uano, int degreeno, int collegeid, int admbatch, int CAMPUSNO, int WEEKNO, int OLDWEEKNO, int OLDCAMPUSNO)
        {
            int status = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                    {
                                new SqlParameter("@P_IDNO",idno),
                                new SqlParameter("@P_SEMESTERNO",semesterno),
                                new SqlParameter("@P_BRANCHNO",branchno),
                                new SqlParameter("@P_UANO",uano),
                                new SqlParameter("@P_DEGREENO",degreeno),
                                new SqlParameter("@P_COLLEGE_ID",collegeid),
                                new SqlParameter("@P_ADMBATCH",admbatch),
                                new SqlParameter("@P_CAMPUSNO",CAMPUSNO),
                                new SqlParameter("@P_WEEKNO",WEEKNO),
                                new SqlParameter("@P_OLDCAMPUSNO",OLDCAMPUSNO),
                                new SqlParameter("@P_OLDWEEKNO",OLDWEEKNO),
                                new SqlParameter("@P_OUTPUT",status)
                    };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;

                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_UPDATE_STUDENT_BRANCH", sqlParams, true);

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

        public int AddAdditionalMultipleDocStd(int idno, int userno, string FileNames, string docnos, string docnames, string path, string remark)
        {
            int status = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[8];
                objParams[0] = new SqlParameter("@P_IDNO", idno);
                objParams[1] = new SqlParameter("@P_USERNO", userno);
                objParams[2] = new SqlParameter("@P_DOCNOS", docnos);
                objParams[3] = new SqlParameter("@P_DOCNAMES", docnames);
                objParams[4] = new SqlParameter("@P_PATH", path);
                objParams[5] = new SqlParameter("@P_FILENAMES", FileNames);
                objParams[6] = new SqlParameter("@P_REMARK", remark);
                objParams[7] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                objParams[7].Direction = ParameterDirection.Output; object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_DOCUMENTENTRY_INS_ADDITIONAL_MULTIPLE_DOCUMENT", objParams, true); if (obj.ToString() == "1")
                    status = Convert.ToInt32(CustomStatus.RecordSaved);
                else if (obj.ToString() == "2")
                    status = Convert.ToInt32(CustomStatus.RecordUpdated);
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

        public DataSet GetDocStudList(int userno, int intake, int branch, int college_id, int degree)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[5];
                objParams[0] = new SqlParameter("@P_USERNO", userno);
                objParams[1] = new SqlParameter("@P_ADMBATCH", intake);
                objParams[2] = new SqlParameter("@P_BRANCHNO", branch);
                objParams[3] = new SqlParameter("@P_COLLEGE_ID", college_id);
                objParams[4] = new SqlParameter("@P_DEGREENO", degree);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_DOCUMENTENTRY_SEL_STUDNEW", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.DocumentController.GetDoclist() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        //added by aashna 06-09-2022
        public DataSet GetExcessStudentDetails(int idno, int srno)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[2];
                objParams[0] = new SqlParameter("@P_IDNO", idno);
                objParams[1] = new SqlParameter("@P_SRNO", srno);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_EXCESS_DETAILS", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.DocumentControllerAcad.GetModuleOfferedCoursesForSemesterRegistration() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        //ADDED BY AASHNA 02-09-2022
        public DataSet GetModuleOfferedCoursesForBridgingSemesterRegistration(int idno, int Sessionno)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[2];
                objParams[0] = new SqlParameter("@P_IDNO", idno);
                objParams[1] = new SqlParameter("@P_SESSIONNO", Sessionno);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_OFFERED_COURSE_LIST_FOR_SEMESTER_BRIDGING", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.DocumentControllerAcad.GetModuleOfferedCoursesForSemesterRegistration() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        //ADDED BY AASHNNA 03-09-2022
        public int InsertModuleBridgingRegistration(int idno, string courseNos, string Ccodes, string CourseNames, string Credits, string Subids, string licno)
        {
            int status = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_IDNO",idno),
                    new SqlParameter("@P_COURSENOS",courseNos),
                    new SqlParameter("@P_CCODES",Ccodes),
                    new SqlParameter("@P_COURSENAMES",CourseNames),
                    new SqlParameter("@P_CREDITS",Credits),
                    new SqlParameter("@P_SUBIDS",Subids),
                    new SqlParameter("@P_LIC_UANO",licno),
                    new SqlParameter("@P_OUTPUT",status)
                };
                //sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INSERT_MODULE_BRIDGING_REGISTRATION_DATA", sqlParams, true);

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

        //ADDED BY AASHNA 03-09-2022
        public DataSet GetPaidReceiptsInfoByStudId_FOR_BRIDGING_PAYMENT(int studentId)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_IDNO", studentId)
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_GETS_PAID_FEES_DETAILS_FOR_BRIDGING_PAYMENT", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.GetPaidReceiptsInfoByStudId_FORPAYMENT() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        //ADDED BY AASHNA 06-05-2023
        public DataSet GetDoclistByAdmin_ForTransferee(int userno)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_USERNO", userno);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_STUDENT_UPLOADED_DOCUMENT_TRANSFEREE", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.DocumentController.GetDoclist() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        //added by aashna 08-05-2023
        public int UpdateDocumentStatus_Transferee(int userno, string docnos, string docnames, string Status, string Remarks, int ua_no)
        {
            int status = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[7];
                objParams[0] = new SqlParameter("@P_USERNO", userno);
                objParams[1] = new SqlParameter("@P_DOCNOS", docnos);
                objParams[2] = new SqlParameter("@P_DOCNAMES", docnames);
                objParams[3] = new SqlParameter("@P_STATUS", Status);
                objParams[4] = new SqlParameter("@P_REMARKS", Remarks);
                objParams[5] = new SqlParameter("@P_UA_NO", ua_no);
                objParams[6] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                objParams[6].Direction = ParameterDirection.Output;

                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_UPDATE_DOCUMENT_STATUS_TRANSFEREE", objParams, true);

                if (obj.ToString() == "1")
                    status = Convert.ToInt32(CustomStatus.RecordSaved);
                else if (obj.ToString() == "2")
                    status = Convert.ToInt32(CustomStatus.RecordUpdated);
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

        public DataSet GetModuleOfferedCoursesForSemesterRegistrationContinuing(int idno, int Sessionno)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[2];
                objParams[0] = new SqlParameter("@P_IDNO", idno);
                objParams[1] = new SqlParameter("@P_SESSIONNO", Sessionno);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_OFFERED_COURSE_LIST_FOR_SEMESTER_REG_CONTINUING_STUDENT", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.DocumentControllerAcad.GetModuleOfferedCoursesForSemesterRegistration() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public int InsertModuleRegistrationHigherSemesterContinue(int idno, string courseNos, string Ccodes, string CourseNames, string Credits, string Subids, string licno, string sectionno, int sessionno, string Amounts, string SemesterNos, string Regids)
        {
            int status = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_IDNO",idno),
                    new SqlParameter("@P_COURSENOS",courseNos),
                    new SqlParameter("@P_CCODES",Ccodes),
                    new SqlParameter("@P_COURSENAMES",CourseNames),
                    new SqlParameter("@P_CREDITS",Credits),
                    new SqlParameter("@P_SUBIDS",Subids),
                    new SqlParameter("@P_LIC_UANO",licno),
                    new SqlParameter("@P_SECTIONNOS",sectionno),
                    new SqlParameter("@P_SESSIONNO",sessionno),
                    new SqlParameter("@P_AMOUNTS",Amounts),
                    new SqlParameter("@P_SEMESTERNOS",SemesterNos),
                    new SqlParameter("@P_REGIDS",Regids),
                    new SqlParameter("@P_OUTPUT",SqlDbType.Int)
                };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;

                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INSERT_MODULE_REGISTRATION_DATA_HIGHER_SEMESTER_CONTINUING", sqlParams, true);

                if (obj != null && obj.ToString() == "5")
                    status = Convert.ToInt32(CustomStatus.RecordNotFound);
                else if (obj != null && obj.ToString() != "-99")
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

        public int InsertModuleRegistrationHigherSemesterAdmin(int idno, string courseNos, string Ccodes, string CourseNames, string Credits, string Subids, string licno, string sectionno, int sessionno, int uano, string Amounts, string SemesterNos, string regids)
        {
            int status = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_IDNO",idno),
                    new SqlParameter("@P_COURSENOS",courseNos),
                    new SqlParameter("@P_CCODES",Ccodes),
                    new SqlParameter("@P_COURSENAMES",CourseNames),
                    new SqlParameter("@P_CREDITS",Credits),
                    new SqlParameter("@P_SUBIDS",Subids),
                    new SqlParameter("@P_LIC_UANO",licno),
                    new SqlParameter("@P_SECTIONNOS",sectionno),
                    new SqlParameter("@P_SESSIONNO",sessionno),
                    new SqlParameter("@P_UA_NO",uano),
                    new SqlParameter("@P_AMOUNTS",Amounts),
                    new SqlParameter("@P_SEMESTERNOS",SemesterNos),
                    new SqlParameter("@P_REGIDS",regids),
                    new SqlParameter("@P_OUTPUT",SqlDbType.Int)
                };
                //sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;
                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INSERT_MODULE_REGISTRATION_DATA_BY_ADMIN", sqlParams, true);

                if (obj != null && obj.ToString() == "5")
                    status = Convert.ToInt32(CustomStatus.RecordNotFound);
                else if (obj != null && obj.ToString() != "-99")
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

        public int InsertModuleRegistrationHigherSemesterAdmin_Capacity_Check(int idno, string courseNos, string Ccodes, string CourseNames, string Credits, string Subids, string licno, string sectionno, int sessionno, int uano, string Amounts, string SemesterNos, string regids)
        {
            int status = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_IDNO",idno),
                    new SqlParameter("@P_COURSENOS",courseNos),
                    new SqlParameter("@P_CCODES",Ccodes),
                    new SqlParameter("@P_COURSENAMES",CourseNames),
                    new SqlParameter("@P_CREDITS",Credits),
                    new SqlParameter("@P_SUBIDS",Subids),
                    new SqlParameter("@P_LIC_UANO",licno),
                    new SqlParameter("@P_SECTIONNOS",sectionno),
                    new SqlParameter("@P_SESSIONNO",sessionno),
                    new SqlParameter("@P_UA_NO",uano),
                    new SqlParameter("@P_AMOUNTS",Amounts),
                    new SqlParameter("@P_SEMESTERNOS",SemesterNos),
                    new SqlParameter("@P_REGIDS",regids),
                    new SqlParameter("@P_OUTPUT",SqlDbType.Int)
                };
                //sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;
                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INSERT_MODULE_REGISTRATION_DATA_BY_ADMIN_FOR_CAPACITY_FULL_CHECK", sqlParams, true);

                if (obj != null && (obj.ToString() == "5" || obj.ToString() == "3"))
                    status = Convert.ToInt32(CustomStatus.RecordNotFound);
                else if (obj != null && obj.ToString() != "-99")
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

        public DataSet GetModuleOfferedCoursesForSemesterRegistration_By_Admin(int idno, int Sessionno, int Semesterno, int commandtype)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[4];
                objParams[0] = new SqlParameter("@P_IDNO", idno);
                objParams[1] = new SqlParameter("@P_SESSIONNO", Sessionno);
                objParams[2] = new SqlParameter("@P_SEMESTERNO", Semesterno);
                objParams[3] = new SqlParameter("@P_COMMAND_TYPE", commandtype);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_DYNAMIC_SECTION_BY_ADMIN", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.DocumentControllerAcad.GetModuleOfferedCoursesForSemesterRegistration() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }
        //Added By Roshan Patil 01-01-2024
        public int UpdateStudentDocumentStatus(int userno, string docnos, string docnames, string Status, string Remarks, int ua_no)
        {
            int status = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[7];
                objParams[0] = new SqlParameter("@P_USERNO", userno);
                objParams[1] = new SqlParameter("@P_DOCNOS", docnos);
                objParams[2] = new SqlParameter("@P_DOCNAMES", docnames);
                objParams[3] = new SqlParameter("@P_STATUS", Status);
                objParams[4] = new SqlParameter("@P_REMARKS", Remarks);
                objParams[5] = new SqlParameter("@P_UA_NO", ua_no);
                objParams[6] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                objParams[6].Direction = ParameterDirection.Output;

                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_UPDATE_STUDENT_DOCUMENT_STATUS", objParams, true);

                if (obj.ToString() == "1")
                    status = Convert.ToInt32(CustomStatus.RecordSaved);
                else if (obj.ToString() == "2")
                    status = Convert.ToInt32(CustomStatus.RecordUpdated);
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

        public int StudentEnrollementConformation(int Userno, string Password, int Semesterno, int Year, int College_id, int Degreeno, int BranchNo, int AdmBatch, int Ua_No)
        {
            int status = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[10];
                objParams[0] = new SqlParameter("@P_USERNO", Userno);
                objParams[1] = new SqlParameter("@P_PASSWORD", Password);
                objParams[2] = new SqlParameter("@P_SEMESTERNO", Semesterno);
                objParams[3] = new SqlParameter("@P_YEARNO", Year);
                objParams[4] = new SqlParameter("@P_COLLEGE_ID", College_id);
                objParams[5] = new SqlParameter("@P_DEGREENO", Degreeno);
                objParams[6] = new SqlParameter("@P_BRANCHNO", BranchNo);
                objParams[7] = new SqlParameter("@P_ADMBATCH", AdmBatch);
                objParams[8] = new SqlParameter("@P_UA_NO", Ua_No);
                objParams[9] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                objParams[9].Direction = ParameterDirection.Output;

                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_STUD_ONLINE_ADMISSION_CONFIRM_NEW", objParams, true);
                if (obj != null && obj.ToString() == "3")
                    status = Convert.ToInt32(CustomStatus.RecordExist);
                else if (obj != null && obj.ToString() != "-99")
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
        //added by Shahbaz Ahmad 02-01-2024
        /// <summary>
        /// Old Session
        /// </summary>
        /// <param name="idno"></param>
        /// <param name="Sessionno"></param>
        /// <param name="Semesterno"></param>
        /// <param name="commandtype"></param>
        /// <returns></returns>
        public int InsertModuleRegistrationHigherSemesterAdmin_Capacity_Check_OS(int idno, string courseNos, string Ccodes, string CourseNames, string Credits, string Subids, string licno, string sectionno, int sessionno, int uano, string Amounts, string SemesterNos, string regids)
        {
            int status = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_IDNO",idno),
                    new SqlParameter("@P_COURSENOS",courseNos),
                    new SqlParameter("@P_CCODES",Ccodes),
                    new SqlParameter("@P_COURSENAMES",CourseNames),
                    new SqlParameter("@P_CREDITS",Credits),
                    new SqlParameter("@P_SUBIDS",Subids),
                    new SqlParameter("@P_LIC_UANO",licno),
                    new SqlParameter("@P_SECTIONNOS",sectionno),
                    new SqlParameter("@P_SESSIONNO",sessionno),
                    new SqlParameter("@P_UA_NO",uano),
                    new SqlParameter("@P_AMOUNTS",Amounts),
                    new SqlParameter("@P_SEMESTERNOS",SemesterNos),
                    new SqlParameter("@P_REGIDS",regids),
                    new SqlParameter("@P_OUTPUT",SqlDbType.Int)
                };
                //sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;
                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INSERT_MODULE_REGISTRATION_DATA_BY_ADMIN_FOR_CAPACITY_FULL_CHECK_OS", sqlParams, true);

                if (obj != null && (obj.ToString() == "5" || obj.ToString() == "3"))
                    status = Convert.ToInt32(CustomStatus.RecordNotFound);
                else if (obj != null && obj.ToString() != "-99")
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
        /// <summary>
        /// INSERT OLD SESSION
        /// </summary>
        /// <param name="idno"></param>
        /// <param name="Sessionno"></param>
        /// <param name="Semesterno"></param>
        /// <param name="commandtype"></param>
        /// <returns></returns>
        public int InsertModuleRegistrationHigherSemesterAdmin_OS(int idno, string courseNos, string Ccodes, string CourseNames, string Credits, string Subids, string licno, string sectionno, int sessionno, int uano, string Amounts, string SemesterNos, string regids)
        {
            int status = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_IDNO",idno),
                    new SqlParameter("@P_COURSENOS",courseNos),
                    new SqlParameter("@P_CCODES",Ccodes),
                    new SqlParameter("@P_COURSENAMES",CourseNames),
                    new SqlParameter("@P_CREDITS",Credits),
                    new SqlParameter("@P_SUBIDS",Subids),
                    new SqlParameter("@P_LIC_UANO",licno),
                    new SqlParameter("@P_SECTIONNOS",sectionno),
                    new SqlParameter("@P_SESSIONNO",sessionno),
                    new SqlParameter("@P_UA_NO",uano),
                    new SqlParameter("@P_AMOUNTS",Amounts),
                    new SqlParameter("@P_SEMESTERNOS",SemesterNos),
                    new SqlParameter("@P_REGIDS",regids),
                    new SqlParameter("@P_OUTPUT",SqlDbType.Int)
                };
                //sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;
                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INSERT_MODULE_REGISTRATION_DATA_BY_ADMIN_OS", sqlParams, true);

                if (obj != null && obj.ToString() == "5")
                    status = Convert.ToInt32(CustomStatus.RecordNotFound);
                else if (obj != null && obj.ToString() != "-99")
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

        // Added By Vinay Mishra Date 09-01-2024//
        public DataSet getOfferAcceptanceDetailsVin(int userno, int Command_Type, int CampusNo)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[3];
                objParams[0] = new SqlParameter("@P_USERNO", userno);
                objParams[1] = new SqlParameter("@P_COMMAND_TYPE", Command_Type);
                objParams[2] = new SqlParameter("@P_CAMPUSNO", CampusNo);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_OFFER_ACCEPTANCE_DETAILS_VIN", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.DocumentController.getOfferAcceptanceDetailsVin() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }


        // Added By Vinay Mishra Date 09-01-2024//
        public int UpdateDocumentStatus_TransfereeVin(int userno, string docnos, string docnames, string Status, string Remarks, int ua_no)
        {
            int status = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[7];
                objParams[0] = new SqlParameter("@P_USERNO", userno);
                objParams[1] = new SqlParameter("@P_DOCNOS", docnos);
                objParams[2] = new SqlParameter("@P_DOCNAMES", docnames);
                objParams[3] = new SqlParameter("@P_STATUS", Status);
                objParams[4] = new SqlParameter("@P_REMARKS", Remarks);
                objParams[5] = new SqlParameter("@P_UA_NO", ua_no);
                objParams[6] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                objParams[6].Direction = ParameterDirection.Output;

                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_UPDATE_DOCUMENT_STATUS_TRANSFEREE_VIN", objParams, true);

                if (obj.ToString() == "1")
                    status = Convert.ToInt32(CustomStatus.RecordSaved);
                else if (obj.ToString() == "2")
                    status = Convert.ToInt32(CustomStatus.RecordUpdated);
                else
                    status = Convert.ToInt32(CustomStatus.Error);
            }
            catch (Exception ex)
            {
                status = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.DocumentControllerAcad.UpdateDocumentStatus_TransfereeVin() --> " + ex.Message + " " + ex.StackTrace);
            }
            return status;
        }
    }
}