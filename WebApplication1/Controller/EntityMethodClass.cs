using IITMS;
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

/// <summary>
/// Summary description for EntityMethodClass
/// </summary>
public class EntityMethodClass
{
    //string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["NITPRM"].ConnectionString;

    private string _UAIMS_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

    public DataSet FillDropDown(string TableName, string Column1, string Column2, string wherecondition, string orderby)
    {
        DataSet ds = null;
        try
        {
            SQLHelper objsqlhelper = new SQLHelper(_UAIMS_constr);
            SqlParameter[] objParams = new SqlParameter[5];
            objParams[0] = new SqlParameter("@P_TABLENAME", TableName);
            objParams[1] = new SqlParameter("@P_COLUMNNAME_1", Column1);
            objParams[2] = new SqlParameter("@P_COLUMNNAME_2", Column2);
            if (!wherecondition.Equals(string.Empty))
                objParams[3] = new SqlParameter("@P_WHERECONDITION", wherecondition);
            else
                objParams[3] = new SqlParameter("@P_WHERECONDITION", DBNull.Value);
            if (!orderby.Equals(string.Empty))
                objParams[4] = new SqlParameter("@P_ORDERBY", orderby);
            else
                objParams[4] = new SqlParameter("@P_ORDERBY", DBNull.Value);

            ds = objsqlhelper.ExecuteDataSetSP("PKG_UTILS_SP_DROPDOWN", objParams);
        }
        catch (Exception ex)
        {
            throw new IITMSException("IITMS.Common.FillDropDown-> " + ex.ToString());
        }
        return ds;
    }

    public void DisplayMessage(string Message, Page pg)
    {
        pg.ClientScript.RegisterClientScriptBlock(this.GetType(), "Msg", "<Script language='javascript' type='text/javascript'> alert('" + Message + "'); </Script>");
    }

    public string LookUp(string tablename, string columnname, string wherecondition)
    {
        string ret = string.Empty;

        try
        {
            SQLHelper objsqlhelper = new SQLHelper(_UAIMS_constr);
            SqlParameter[] objParams = new SqlParameter[3];
            objParams[0] = new SqlParameter("@P_TABLENAME", tablename);
            objParams[1] = new SqlParameter("@P_COLUMNNAME", columnname);
            if (!wherecondition.Equals(string.Empty))
                objParams[2] = new SqlParameter("@P_WHERECONDITION", wherecondition);
            else
                objParams[2] = new SqlParameter("@P_WHERECONDITION", DBNull.Value);

            ret = Convert.ToString(objsqlhelper.ExecuteScalarSP("PKG_UTILS_SP_LOOKUP", objParams));
        }
        catch (Exception ex)
        {
            throw new IITMSException("IITMS.Common.lookup-> " + ex.ToString());
        }
        return ret;
    }

    public DataSet GetPublishData(int idno, int semesterNo)
    {
        DataSet ds = null;
        try
        {
            SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
            SqlParameter[] objParams = new SqlParameter[2];
            objParams[0] = new SqlParameter("@P_IDNO", idno);
            objParams[1] = new SqlParameter("@P_SEMESTER", semesterNo);

            ds = objSQLHelper.ExecuteDataSetSP("PKG_EXAM_SEARCH_STUDENT", objParams);
        }
        catch (Exception ex)
        {
            throw new IITMSException("default.CourseController.GetPublishData-> " + ex.ToString());
        }

        return ds;
    }

    public int InsertStudentData(EntityClass objEntityClass, string docno, string docname, string filename, string linkname)
    {
        int retStatus = Convert.ToInt32(CustomStatus.Others);
        try
        {
            SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
            SqlParameter[] objParams = new SqlParameter[27];

            objParams[0] = new SqlParameter("@P_STUDNAME", objEntityClass.StudName);
            objParams[1] = new SqlParameter("@P_STUDROLLNO", objEntityClass.StudRollno);
            objParams[2] = new SqlParameter("@P_STUDENROLLNO", objEntityClass.StudEnrolNO);
            objParams[3] = new SqlParameter("@P_STUD_DEGREE", objEntityClass.StudDegree);
            objParams[4] = new SqlParameter("@P_STUD_BRANCH", objEntityClass.StudBranch);
            objParams[5] = new SqlParameter("@P_STUD_ADDRESS", objEntityClass.StudAddress);
            objParams[6] = new SqlParameter("@P_STUD_MOBILENO", objEntityClass.StudMobileNo);
            objParams[7] = new SqlParameter("@P_STUD_EMAILID", objEntityClass.StudEmailID);
            objParams[8] = new SqlParameter("@P_STUD_PAY_NO", objEntityClass.StudPaymentNo);
            objParams[9] = new SqlParameter("@P_AMOUNT", objEntityClass.Amount);
            objParams[10] = new SqlParameter("@P_DOCNAME", docname);
            objParams[11] = new SqlParameter("@P_DOCNO", docno);
            objParams[12] = new SqlParameter("@P_FILENAME", filename);
            objParams[13] = new SqlParameter("@AADHAAR_NO", objEntityClass.AadhaarNo);
            objParams[14] = new SqlParameter("@CITY", objEntityClass.City);
            objParams[15] = new SqlParameter("@HOUSE_NO", objEntityClass.HouseNo);
            objParams[16] = new SqlParameter("@STATE", objEntityClass.State);
            objParams[17] = new SqlParameter("@P_STUD_PINCODE", objEntityClass.StudPincode);
            objParams[18] = new SqlParameter("@P_DISTRICT", objEntityClass.District);
            objParams[19] = new SqlParameter("@P_LINKNAME", linkname);//-- add linkname
            objParams[20] = new SqlParameter("@P_PROVISIONAL_PAY", objEntityClass.ProvisionalPay);//-- add Provisional Pay  -- 02102018

            objParams[21] = new SqlParameter("@P_EMPSTATUS", objEntityClass.Empstatus);   // add student status details -- 25102018
            objParams[22] = new SqlParameter("@P_COMPNAME", objEntityClass.Compname);
            objParams[23] = new SqlParameter("@P_CMPCTC", objEntityClass.CTC);
            objParams[24] = new SqlParameter("@P_HEIGHERSTUDY", objEntityClass.Heighrstudy);
            objParams[25] = new SqlParameter("@P_INSTITUTENAME", objEntityClass.InstitueName);

            objParams[26] = new SqlParameter("@P_OUT", SqlDbType.Int);
            objParams[26].Direction = ParameterDirection.Output;

            int ret = Convert.ToInt32(objSQLHelper.ExecuteNonQuerySP("PKG_INS_DATA_CONVOCATION", objParams, true));
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

    public int InsertToSqlDB(EntityClass objEntityClass)
    {
        int retStatus = Convert.ToInt32(CustomStatus.Others);
        try
        {
            SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
            SqlParameter[] objParams = null;

            objParams = new SqlParameter[20];
            objParams[0] = new SqlParameter("@P_ROLLNO", objEntityClass.StudRollno);
            objParams[1] = new SqlParameter("@P_ENROLLNO", objEntityClass.StudEnrolNO);
            objParams[2] = new SqlParameter("@P_STUDNAME", objEntityClass.StudName);
            objParams[3] = new SqlParameter("@P_FATHERNAME", objEntityClass.StudFatherName);
            objParams[4] = new SqlParameter("@P_MOTHERNAME", objEntityClass.StudMotherName);
            objParams[5] = new SqlParameter("@P_EMAIL", objEntityClass.StudEmailID);
            objParams[6] = new SqlParameter("@P_STUD_REG_MOBNO", objEntityClass.StudeRegMobileNO);
            objParams[7] = new SqlParameter("@P_PAYMENT_REF_NO", objEntityClass.StudPaymentNo);
            objParams[8] = new SqlParameter("@P_STUD_MOB_NO", objEntityClass.StudMobileNo);
            objParams[9] = new SqlParameter("@P_PAYMENT_STATUS", objEntityClass.StudFeeStatus);
            objParams[10] = new SqlParameter("@P_MAXMARK", objEntityClass.StudMaxMark);
            objParams[11] = new SqlParameter("@P_OBTMARK", objEntityClass.StudOBTMark);
            objParams[12] = new SqlParameter("@P_DIVISION", objEntityClass.StudDivision);
            objParams[13] = new SqlParameter("@P_PASS_SESSION", objEntityClass.StudPassSession);
            objParams[14] = new SqlParameter("@P_DEGREENAME", objEntityClass.StudDegree);
            objParams[15] = new SqlParameter("@P_BRANCHNAME", objEntityClass.StudBranch);
            objParams[16] = new SqlParameter("@P_STUD_TYPE ", objEntityClass.StudType);
            objParams[17] = new SqlParameter("@P_AMOUNT", objEntityClass.StudAmount);
            objParams[18] = new SqlParameter("@P_CNIDNO", objEntityClass.StudCNID);

            objParams[19] = new SqlParameter("@P_OUT", SqlDbType.Int);
            objParams[19].Direction = ParameterDirection.Output;
            int ret = Convert.ToInt32(objSQLHelper.ExecuteNonQuerySP("PKG_TEMP_CONVOCATION_DATA", objParams, true));
            if (ret == 1)
                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
            else
                retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
        }
        catch (Exception ee)
        {
            throw new IITMSException("IITMS.UAIMS.BusinessLogicLayer.BusinessLogic.ImportDataController.InsertToSqlDB-> " + ee.ToString());
        }
        return retStatus;
    }

    public DataSet GetStudentData(string rollno)
    {
        DataSet ds = null;
        try
        {
            SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
            SqlParameter[] objParams = new SqlParameter[1];
            objParams[0] = new SqlParameter("@P_ROLLNO", rollno);
            // objParams[1] = new SqlParameter("@P_SEMESTER", semesterNo);

            ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_STUDENT_INFO", objParams);
        }
        catch (Exception ex)
        {
            throw new IITMSException("default.CourseController.GetPublishData-> " + ex.ToString());
        }

        return ds;
    }

    //---added --for Provisional Degree ------22092017

    #region Provisional Degree

    public DataSet GetProvisionalDegreeStudentData(string rollno)
    {
        DataSet ds = null;

        try
        {
            SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
            SqlParameter[] objParams = new SqlParameter[1];
            objParams[0] = new SqlParameter("@P_ROLLNO", rollno);
            // objParams[1] = new SqlParameter("@P_SEMESTER", semesterNo);

            ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_PROVISIONAL_DEGREE_STUDENT_INFO", objParams);
        }
        catch (Exception ex)
        {
            throw new IITMSException("default.CourseController.GetPublishData-> " + ex.ToString());
        }

        return ds;
    }

    //======
    public DataSet GetProvisionalAprovalStudentData(string rollno)
    {
        DataSet ds = null;
        try
        {
            SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
            SqlParameter[] objParams = new SqlParameter[1];
            objParams[0] = new SqlParameter("@P_ROLLNO", rollno);
            // objParams[1] = new SqlParameter("@P_SEMESTER", semesterNo);

            ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_PROVISIONAL_APPROVAL_STUDENT_INFO", objParams);
        }
        catch (Exception ex)
        {
            throw new IITMSException("default.CourseController.GetPublishData-> " + ex.ToString());
        }

        return ds;
    }

    #endregion Provisional Degree

    ///-- added check activity-- 19112017
    public DataTableReader CheckActivity(int sessionno, int ua_type, int pagelink)
    {
        DataTableReader dtr = null;

        try
        {
            SQLHelper objsqlhelper = new SQLHelper(_UAIMS_constr);
            SqlParameter[] objParams = new SqlParameter[3];
            objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
            objParams[1] = new SqlParameter("@P_UA_TYPE", ua_type);
            objParams[2] = new SqlParameter("@P_PAGE_LINK", pagelink);

            DataSet ds = objsqlhelper.ExecuteDataSetSP("PKG_ACTIVITY_CHECK_ACTIVITY_CONVO", objParams);
            if (ds.Tables.Count > 0)
                dtr = ds.Tables[0].CreateDataReader();
        }
        catch (Exception ex)
        {
            throw new IITMSException("IITMS.ActivityController.CheckActivity-> " + ex.ToString());
        }
        return dtr;
    }

    //-------------------------

    public int InsertProvisionalDegreeStudentData(EntityClass objEntityClass, string docno, string docname, string filename, string linkname)
    {
        int retStatus = Convert.ToInt32(CustomStatus.Others);
        try
        {
            SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
            SqlParameter[] objParams = new SqlParameter[29];

            objParams[0] = new SqlParameter("@P_NAME", objEntityClass.StudName);
            objParams[1] = new SqlParameter("@P_ROLLNO", objEntityClass.StudRollno);
            objParams[2] = new SqlParameter("@P_ENROLLNO", objEntityClass.StudEnrolNO);
            objParams[3] = new SqlParameter("@P_DEGREE", objEntityClass.StudDegree);
            objParams[4] = new SqlParameter("@P_BRANCH", objEntityClass.StudBranch);
            objParams[5] = new SqlParameter("@P_SESSION", objEntityClass.StudSeesion);
            objParams[6] = new SqlParameter("@P_PADDRESS", objEntityClass.StudAddress);
            objParams[7] = new SqlParameter("@P_PPINCODE", objEntityClass.StudPincode);
            objParams[8] = new SqlParameter("@P_PCITY", objEntityClass.Studcity);
            objParams[9] = new SqlParameter("@P_MOBILENO", objEntityClass.StudMobileNo);
            objParams[10] = new SqlParameter("@P_EMAILID", objEntityClass.StudEmailID);

            objParams[11] = new SqlParameter("@P_PAY_TYPE", objEntityClass.StudPaymentTyp);
            objParams[12] = new SqlParameter("@P_AMOUNT", objEntityClass.StudAmount);
            objParams[13] = new SqlParameter("@P_AADHAAR_NO", objEntityClass.StudAadhaarNo);
            objParams[14] = new SqlParameter("@P_CADDRESS", objEntityClass.StudCAddress);
            objParams[15] = new SqlParameter("@P_CPINCODE", objEntityClass.StudCPincode);
            objParams[16] = new SqlParameter("@P_CCITY", objEntityClass.StudCcity);

            objParams[17] = new SqlParameter("@P_STUDNAME_HINDI", objEntityClass.StudNameHindi);  /// --- 14052018  hindi name
            objParams[18] = new SqlParameter("@P_STUDCONTACTNO", objEntityClass.StudContactNo);

            objParams[19] = new SqlParameter("@P_EMPSTATUS", objEntityClass.Empstatus);   // add student status details -- 25102018
            objParams[20] = new SqlParameter("@P_COMPNAME", objEntityClass.Compname);
            objParams[21] = new SqlParameter("@P_CMPCTC", objEntityClass.CTC);
            objParams[22] = new SqlParameter("@P_HEIGHERSTUDY", objEntityClass.Heighrstudy);
            objParams[23] = new SqlParameter("@P_INSTITUTENAME", objEntityClass.InstitueName);

            objParams[24] = new SqlParameter("@P_DOCNAME", docname);
            objParams[25] = new SqlParameter("@P_DOCNO", docno);
            objParams[26] = new SqlParameter("@P_FILENAME", filename);
            objParams[27] = new SqlParameter("@P_LINKNAME", linkname);//-- change tommorow

            objParams[28] = new SqlParameter("@P_OUT", SqlDbType.Int);
            objParams[28].Direction = ParameterDirection.Output;

            int ret = Convert.ToInt32(objSQLHelper.ExecuteNonQuerySP("PKG_INS_DATA_PROVISIONAL_DEGREE", objParams, true));
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

    ///---add for INSERT-- convocation-- DCR  -- 22112017
    public int InsertDCRConvocation(EntityClass objEntityClass)
    {
        int retStatus = Convert.ToInt32(CustomStatus.Others);
        try
        {
            SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
            SqlParameter[] objParams = new SqlParameter[10];

            objParams[0] = new SqlParameter("@P_SESSION_NO", objEntityClass.StudSeesion);
            objParams[1] = new SqlParameter("@P_ROLLNO", objEntityClass.StudRollno);
            objParams[2] = new SqlParameter("@P_ENROLLNO", objEntityClass.StudEnrolNO);
            objParams[3] = new SqlParameter("@P_RECIEPTCODE", objEntityClass.StudReceptCode);
            objParams[4] = new SqlParameter("@P_SEMESTERNO", objEntityClass.StudDivision);
            objParams[5] = new SqlParameter("@P_PAYMENTTYPE", objEntityClass.StudPaymentTyp);
            objParams[6] = new SqlParameter("@P_UA_NO", objEntityClass.StudPaymentNo);
            objParams[7] = new SqlParameter("@P_RECIEPTNO", objEntityClass.StudReceptNo);
            // objParams[8] = new SqlParameter("@P_EXAMAMOUNT", objEntityClass.StudAmount);
            objParams[8] = new SqlParameter("@P_COUNTER_NO", objEntityClass.StudCounter);
            objParams[9] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
            objParams[9].Direction = ParameterDirection.Output;

            int ret = Convert.ToInt32(objSQLHelper.ExecuteNonQuerySP("PKG_CREATE_DCR_FOR_CONVOCATION_STUDENTS", objParams, true));
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

    ///---Added for INSERT --CONVOCATION-- DEMAND--22112017
    public int InsertDemandConvocation(EntityClass objEntityClass)
    {
        int retStatus = Convert.ToInt32(CustomStatus.Others);
        try
        {
            SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
            SqlParameter[] objParams = new SqlParameter[9];

            objParams[0] = new SqlParameter("@P_SESSION_NO", objEntityClass.StudSeesion);
            objParams[1] = new SqlParameter("@P_ROLLNO", objEntityClass.StudRollno);
            objParams[2] = new SqlParameter("@P_ENROLLNO", objEntityClass.StudEnrolNO);
            objParams[3] = new SqlParameter("@P_RECIEPTCODE", objEntityClass.StudReceptCode);
            objParams[4] = new SqlParameter("@P_SEMESTERNO", objEntityClass.StudDivision);
            objParams[5] = new SqlParameter("@P_PAYMENTTYPE", objEntityClass.StudPaymentTyp);
            objParams[6] = new SqlParameter("@P_UA_NO", objEntityClass.StudPaymentNo);
            objParams[7] = new SqlParameter("@P_COUNTER_NO", objEntityClass.StudCounter);

            objParams[8] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
            objParams[8].Direction = ParameterDirection.Output;

            int ret = Convert.ToInt32(objSQLHelper.ExecuteNonQuerySP("PKG_CREATE_DEMAND_FOR_CONVOCATION_STUDENTS", objParams, true));
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

    ///--------------------------------------------

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

    public int InsertMasterConvo(EntityClass objEntityClass)
    {
        int retStatus = Convert.ToInt32(CustomStatus.Others);
        try
        {
            SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
            SqlParameter[] objParams = new SqlParameter[5];

            objParams[0] = new SqlParameter("@P_CNAME", objEntityClass.StudName);
            objParams[1] = new SqlParameter("@P_YEAR", objEntityClass.StudDivision);
            objParams[2] = new SqlParameter("@P_IPADDRESS", objEntityClass.StudReceptNo);
            objParams[3] = new SqlParameter("@P_UANO", objEntityClass.StudPaymentNo);
            objParams[4] = new SqlParameter("@P_OUT", SqlDbType.Int);
            objParams[4].Direction = ParameterDirection.Output;

            int ret = Convert.ToInt32(objSQLHelper.ExecuteNonQuerySP("PKG_INS_CONVO_MASTER", objParams, true));
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
}