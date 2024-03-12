using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities;
using System;
using System.Data;
using System.Data.SqlClient;

namespace IITMS.UAIMS.BusinessLogicLayer.BusinessLogic
{
    public class CollegeController
    {
        private string connectionstring = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

        //modified on[28-09-2016]
        public int AddCollege(College objCollege, byte[] photo)
        {
            int status = 0;
            try
            {
                SQLHelper objSqlHelper = new SQLHelper(connectionstring);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[9];
                objParams[0] = new SqlParameter("@P_COLLEGE_ID", objCollege.COLLEGE_ID);
                objParams[1] = new SqlParameter("@P_NAME", objCollege.Name);
                objParams[2] = new SqlParameter("@P_COLLEGE_TYPE", objCollege.Collegetype);
                objParams[3] = new SqlParameter("@P_SHORTNAME", objCollege.Short_Name);
                objParams[4] = new SqlParameter("@P_CODE", objCollege.CollegeCode);
                objParams[5] = new SqlParameter("@P_COLLEGE_ADDRESS", objCollege.College_Address);
                objParams[6] = new SqlParameter("@P_COLLEGE_LOGO", photo);
                objParams[7] = new SqlParameter("@P_ACTIVE", objCollege.Active);    //Added by swapnil thakare on dayted 03/08/2021

                objParams[8] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[8].Direction = ParameterDirection.Output;
                //objParams[6] = (photo != null) ? new SqlParameter("@P_COLLEGE_LOGO", photo) : new SqlParameter("@P_COLLEGE_LOGO", DBNull.Value);

                object obj = objSqlHelper.ExecuteNonQuerySP("PKG_ACAD_INSERT_COLLEGEMASTER_DETAILS", objParams, true);

                if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001")
                    status = Convert.ToInt32(CustomStatus.RecordSaved);
                else
                    status = Convert.ToInt32(CustomStatus.Error);
            }
            catch (Exception ex)
            {
                status = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CollegeController.AddCollege --> " + ex.Message + " " + ex.StackTrace);
            }
            return status;
        }

        //modified[28-09-2016]
        public int UpdateCollege(College objCollege, byte[] photo)
        {
            int status = 0;
            try
            {
                SQLHelper objSqlHelper = new SQLHelper(connectionstring);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[9];
                objParams[0] = new SqlParameter("@P_COLLEGE_ID", objCollege.COLLEGE_ID);
                objParams[1] = new SqlParameter("@P_NAME", objCollege.Name);
                objParams[2] = new SqlParameter("@P_COLLEGE_TYPE", objCollege.Collegetype);
                objParams[3] = new SqlParameter("@P_SHORTNAME", objCollege.Short_Name);
                objParams[4] = new SqlParameter("@P_CODE", objCollege.CollegeCode);
                objParams[5] = new SqlParameter("@P_COLLEGE_ADDRESS", objCollege.College_Address);
                //objParams[6] = (photo != null) ? new SqlParameter("@P_COLLEGE_LOGO", photo) : new SqlParameter("@P_COLLEGE_LOGO", DBNull.Value);
                objParams[6] = new SqlParameter("@P_COLLEGE_LOGO", photo);
                objParams[7] = new SqlParameter("@P_ACTIVE", objCollege.Active);  //Added by swapnil thakare on dayted 03/08/2021
                objParams[8] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[8].Direction = ParameterDirection.Output;
                //objParams[7] = new SqlParameter("@P_COLLEGE_ID", SqlDbType.Int);
                //objParams[7].Direction = ParameterDirection.Output;
                object obj = objSqlHelper.ExecuteNonQuerySP("PKG_ACAD_UPDATE_COLLEGEMASTER_DETAILS", objParams, true);

                if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001")
                    status = Convert.ToInt32(CustomStatus.RecordUpdated);
                else
                    status = Convert.ToInt32(CustomStatus.Error);
            }
            catch (Exception ex)
            {
                status = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CollegeController.UpdateCollege --> " + ex.Message + " " + ex.StackTrace);
            }
            return status;
        }

        public DataSet Getdetails()
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionstring);
                SqlParameter[] objParams = new SqlParameter[0];

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_SELECT_COLLEGE_MASTER_DETAILS", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CollegeController.Getdetails() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        //Added by swapnil thakare on dated 12-07-2021

        public int AddCampusMaster(College objCampus, int UANO)
        {
            int status = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionstring);
                SqlParameter[] sqlParams = new SqlParameter[]

                        //Add New Campus Master
                        {
                         new SqlParameter("@P_CAMPUSNO", objCampus.CampusNo),
                         new SqlParameter("@P_CAMPUSNAME", objCampus.Name),
                         new SqlParameter("@P_ADDRESS", objCampus.Address),
                         new SqlParameter("@P_EMAIL", objCampus.Email),
                         new SqlParameter("@P_MOBILE", objCampus.Mobile),
                         new SqlParameter("@P_CAPACITY", objCampus.Capacity),
                         new SqlParameter("@P_ACTIVE", objCampus.Active),
                         new SqlParameter("@P_CREATEDBY", UANO),
                         new SqlParameter("@P_OUT", objCampus.CampusNo)
                        };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;
                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_INS_CAMPUS", sqlParams, true);

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

        public int deleteCampusRecord(int col_Campusno)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);

            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionstring);

                SqlParameter[] objParams = new SqlParameter[2];
                objParams[0] = new SqlParameter("@P_CAMPUSNO", col_Campusno);

                objParams[1] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[1].Direction = ParameterDirection.Output;

                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_DELETE_CAMPUS_MASTER", objParams, true);

                if (Convert.ToInt32(obj) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);

                if (Convert.ToInt32(obj) != 99)
                {
                    retStatus = Convert.ToInt32(CustomStatus.RecordDeleted);
                }
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentAttendanceController.deleteattendance-> " + ex.ToString());
            }
            return retStatus;
        }

        public int EditCampusRecord(College objCampus, int UANO)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionstring);

                SqlParameter[] objParams = new SqlParameter[9];
                objParams[0] = new SqlParameter("@P_CAMPUSNO", objCampus.CampusNo);
                objParams[1] = new SqlParameter("@P_CAMPUSNAME", objCampus.Name);
                objParams[2] = new SqlParameter("@P_ADDRESS", objCampus.Address);
                objParams[3] = new SqlParameter("@P_EMAIL", objCampus.Email);
                objParams[4] = new SqlParameter("@P_MOBILE", objCampus.Mobile);
                objParams[5] = new SqlParameter("@P_CAPACITY", objCampus.Capacity);
                objParams[6] = new SqlParameter("@P_ACTIVE", objCampus.Active);
                objParams[7] = new SqlParameter("@P_MODIFIEDBY", UANO);
                objParams[8] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[8].Direction = ParameterDirection.Output;

                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_UPDATE_CAMPUS_MASTER", objParams, true);

                if (Convert.ToInt32(obj) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);

                if (Convert.ToInt32(obj) == 2)
                {
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                }
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentAttendanceController.deleteattendance-> " + ex.ToString());
            }
            return retStatus;
        }

        //Added by swapnil thakare on dated 29-10-2021
        public int AddAptitudeCenterMaster(College objAptitude, int UANO)
        {
            int status = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionstring);
                SqlParameter[] sqlParams = new SqlParameter[]

                        //Add New Campus Master
                        {
                         new SqlParameter("@P_CENTERNO", objAptitude.CenterNo),
                         new SqlParameter("@P_APTITUDE_CENTER_NAME", objAptitude.Name),
                         new SqlParameter("@P_APTITUDE_ADDRESS", objAptitude.Address),
                         new SqlParameter("@P_APTITUDE_EMAIL", objAptitude.Email),
                         new SqlParameter("@P_APTITUDE_MOBILE", objAptitude.Mobile),
                         new SqlParameter("@P_APTITUDE_CAPACITY", objAptitude.Capacity),
                         new SqlParameter("@P_ACTIVE", objAptitude.Active),
                         new SqlParameter("@P_CREATEDBY", UANO),
                         new SqlParameter("@P_OUT", objAptitude.CampusNo)
                        };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;
                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_INS_APTITUDE_CENTER", sqlParams, true);

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

        public int EditAptitudeCenterRecord(College objAptitude, int UANO)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionstring);

                SqlParameter[] objParams = new SqlParameter[9];
                objParams[0] = new SqlParameter("@P_CENTERNO", objAptitude.CampusNo);
                objParams[1] = new SqlParameter("@P_APTITUDE_CENTER_NAME", objAptitude.Name);
                objParams[2] = new SqlParameter("@P_APTITUDE_ADDRESS", objAptitude.Address);
                objParams[3] = new SqlParameter("@P_APTITUDE_EMAIL", objAptitude.Email);
                objParams[4] = new SqlParameter("@P_APTITUDE_MOBILE", objAptitude.Mobile);
                objParams[5] = new SqlParameter("@P_APTITUDE_CAPACITY", objAptitude.Capacity);
                objParams[6] = new SqlParameter("@P_ACTIVE", objAptitude.Active);
                objParams[7] = new SqlParameter("@P_MODIFIEDBY", UANO);
                objParams[8] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[8].Direction = ParameterDirection.Output;

                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_UPDATE_APTITUDE_CENTER_MASTER", objParams, true);

                if (Convert.ToInt32(obj) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);

                if (Convert.ToInt32(obj) == 2)
                {
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                }
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentAttendanceController.deleteattendance-> " + ex.ToString());
            }
            return retStatus;
        }

        //END
    }
}