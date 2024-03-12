using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using System;
using System.Data;
using System.Data.SqlClient;

namespace IITMS.UAIMS.BusinessLayer.BusinessLogic
{
    public class DocumentContro
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
    }
}