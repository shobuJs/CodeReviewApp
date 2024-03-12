using IITMS.SQLServer.SQLDAL;
using System;
using System.Data;
using System.Data.SqlClient;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessLogic
        {
            public class NCCActivityController
            {
                private string _UAIMS_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                #region Activity Master(NCC/NSS/CLUB)

                //To insert Faculty name and NSS/NCC/CLUB Activity added by Sneha G.on 30/05/2020
                public int InsertFacultytoNccActivity(int Activityno, int Activity_type, int uano)
                {
                    int status = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objDataAccess = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] sqlParams = new SqlParameter[]
                     {
                           new SqlParameter("@P_ACTIVITY_TYPE_NO",Activityno),
                           new SqlParameter("@P_ACTIVITY_TYPE",Activity_type),
                           new SqlParameter("@P_UA_NO",uano),
                           new SqlParameter("@P_OUT", SqlDbType.Int)
                       };
                        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;
                        status = (Int32)objDataAccess.ExecuteNonQuerySP("PKG_ACD_INSERT_FACULTY_IN_ACTIVITY_MASTER", sqlParams, true);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.SessionActivityController.AddSessionActivity() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return status;
                }

                //To Update Faculty name and NSS/NCC/CLUB Activity added by Sneha G.on 30/05/2020
                public int UpdateFacultytoNccActivity(int Activityid, int Activityno, int Activity_type, int uano)
                {
                    int status = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objDataAccess = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] sqlParams = new SqlParameter[]
                     {
                           new SqlParameter("@P_ACTIVITY_ID",Activityid),
                           new SqlParameter("@P_ACTIVITY_TYPE_NO",Activityno),
                           new SqlParameter("@P_ACTIVITY_TYPE",Activity_type),
                           new SqlParameter("@P_UA_NO",uano),
                           new SqlParameter("@P_OUT", SqlDbType.Int)
                       };
                        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;
                        status = (Int32)objDataAccess.ExecuteNonQuerySP("PKG_ACD_UPDATE_FACULTY_IN_ACTIVITY_MASTER", sqlParams, true);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.SessionActivityController.AddSessionActivity() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return status;
                }

                //To Get Faculty Name and NSS/NCC/CLUB Activity added by Sneha G.on 30/05/2020.
                public DataSet GetFacultyNametoNccActivity()
                {
                    DataSet dsCT = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[0];

                        dsCT = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_FACULTY_DATA_ACTIVITYWISE", objParams);
                    }
                    catch (Exception ex)
                    {
                        return dsCT;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.studentcontroller.GetStudentNameAndId-> " + ex.ToString());
                    }

                    return dsCT;
                }

                #endregion Activity Master(NCC/NSS/CLUB)

                #region Student Register for Activity(NCC/NSS/CLUB)

                //To Insert assign student for activity by Faculty added by Sneha G.on 01/06/2020.
                public int AddRegStudDetail(int Activityno, int Activitytype, int RationType, string idno, int uano, string ipaddress)
                {
                    int status = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objDataAccess = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_ACTIVITY_NO",Activityno),
                    new SqlParameter("@P_ACTIVITY_TYPE",Activitytype ),
                    new SqlParameter("@P_NCC_RATION_TYPE",RationType ),
                    new SqlParameter("@P_IDNOS", idno),
                    new SqlParameter("@P_UA_NO", uano),
                    new SqlParameter("@P_IPADDRESS",ipaddress),
                    new SqlParameter("@P_OUT", SqlDbType.Int)
                };
                        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;
                        status = (Int32)objDataAccess.ExecuteNonQuerySP("PKG_ACD_STUD_REG_FOR_NCC", sqlParams, true);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.SessionActivityController.AddSessionActivity() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return status;
                }

                //To Get Student data and NSS/NCC/CLUB Activity added by Sneha G.on 01/06/2020.
                public DataSet GetStudentNametoNccActivity(int activityno, int activity_type)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_ACTIVITY_NO", activityno);
                        objParams[1] = new SqlParameter("@P_ACTIVITY_TYPE", activity_type);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_STUD_DATA_ACTIVITYWISE", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetCourseFor_RevalOrPhotoCopy->" + ex.ToString());
                    }
                    return ds;
                }

                //To update remove date and status.
                public int UpdateRemovedatestatus(int Regid, DateTime removedate)
                {
                    int status = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objDataAccess = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_REG_ID",Regid),
                    new SqlParameter("@P_REMOVE_DATE",removedate),
                    new SqlParameter("@P_OUT", SqlDbType.Int)
                };
                        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;
                        status = (Int32)objDataAccess.ExecuteNonQuerySP("PKG_ACD_UPDATE_STUD_DATA_NCC", sqlParams, true);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.SessionActivityController.AddSessionActivity() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return status;
                }

                #endregion Student Register for Activity(NCC/NSS/CLUB)

                #region Camp Details(NCC/NSS/CLUB)

                //To Get Student Camp Activity details for NSS/NCC/CLUB Activity added by Sneha G.on 03/06/2020.
                public DataSet GetStudDetailforCamp(int uano, int activityno, int activity_type)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_UANO", uano);
                        objParams[1] = new SqlParameter("@P_ACTIVITY_NO", activityno);
                        objParams[2] = new SqlParameter("@P_ACTIVITY_TYPE", activity_type);
                        //objParams[3] = new SqlParameter("@P_NCC_RATION_TYPE", Ration_type);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_STUD_DATA_ACTIVITYWISE_FOR_CAMP", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetCourseFor_RevalOrPhotoCopy->" + ex.ToString());
                    }
                    return ds;
                }

                //To Insert Student Camp Activity details for NSS/NCC/CLUB Activity added by Sneha G.on 03/06/2020.
                public int InsertStudentCampDetails(int Activityno, int Activitytype, int RationType, string campname, string location, decimal duration, int uano, string idno, DateTime fromdate, DateTime todate, string c_details, string filename, string filepath)
                {
                    int status = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objDataAccess = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_ACTIVITY_TYPE_NO",Activityno),
                    new SqlParameter("@P_ACTIVITY_TYPE",Activitytype ),
                    new SqlParameter("@P_NCC_RATION_NO",RationType ),
                    new SqlParameter("@P_CAMP_NAME",campname),
                    new SqlParameter("@P_CAMP_LOCATION",location),
                    new SqlParameter("@P_CAMP_DURATION",duration),
                    new SqlParameter("@P_UANO",uano),
                    new SqlParameter("@P_IDNO",idno),
                    new SqlParameter("@P_CAMP_FROM_DATE",fromdate),
                    new SqlParameter("@P_CAMP_TO_DATE",todate),
                    new SqlParameter("@P_CAMP_DETAILS",c_details),
                    new SqlParameter("@P_ACTIVITY_FILE_NAME",filename),
                    new SqlParameter("@P_ACTIVITY_FILE_PATH",filepath),
                    new SqlParameter("@P_OUT", SqlDbType.Int)
                };
                        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;
                        status = (Int32)objDataAccess.ExecuteNonQuerySP("PKG_INSERT_CAMP_ACTIVITY_DETAILS", sqlParams, true);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.SessionActivityController.AddSessionActivity() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return status;
                }

                //Bind Student Camp Details added by Sneha G.on 04/06/2020.
                public DataSet BindStudCampDetails(int activityno, int activity_type)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_ACTIVITY_TYPE_NO", activityno);
                        objParams[1] = new SqlParameter("@P_ACTIVITY_TYPE", activity_type);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_BIND_STUD_DATA_OF_CAMP_ACTIVITYWISE", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetCourseFor_RevalOrPhotoCopy->" + ex.ToString());
                    }
                    return ds;
                }

                //To Update Student Camp Activity details for NSS/NCC/CLUB Activity added by Sneha G.on 04/06/2020.
                public int UpdateStudentCampDetails(int campno, int Activityno, int Activitytype, int RationType, string campname, string location, decimal duration, DateTime fromdate, DateTime todate, string c_details)
                {
                    int status = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objDataAccess = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_CAMP_NO",campno),
                    new SqlParameter("@P_ACTIVITY_TYPE_NO",Activityno),
                    new SqlParameter("@P_ACTIVITY_TYPE",Activitytype ),
                    new SqlParameter("@P_NCC_RATION_NO",RationType ),
                    new SqlParameter("@P_CAMP_NAME",campname),
                    new SqlParameter("@P_CAMP_LOCATION",location),
                    new SqlParameter("@P_CAMP_DURATION",duration),
                    new SqlParameter("@P_CAMP_FROM_DATE",fromdate),
                    new SqlParameter("@P_CAMP_TO_DATE",todate),
                    new SqlParameter("@P_CAMP_DETAILS",c_details),
                    new SqlParameter("@P_OUT", SqlDbType.Int)
                };
                        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;
                        status = (Int32)objDataAccess.ExecuteNonQuerySP("PKG_UPDATE_CAMP_ACTIVITY_DETAILS", sqlParams, true);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.SessionActivityController.AddSessionActivity() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return status;
                }

                public DataSet GetCertificatesByCampNo(int idno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_GET_CERTIFICATES_BY_CAMP_NO", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetCertificatesByNCCNo-> " + ex.ToString());
                    }

                    return ds;
                }

                #endregion Camp Details(NCC/NSS/CLUB)

                #region Student Register for Activity(NCC/NSS/CLUB)

                //To Insert assign student for activity by Faculty added by Sneha G.on 01/06/2020.
                public int AddRegStudDetail(int Activityno, int Activitytype, int RationType, string idno, int uano, string ipaddress, DateTime adddate)
                {
                    int status = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objDataAccess = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_ACTIVITY_NO",Activityno),
                    new SqlParameter("@P_ACTIVITY_TYPE",Activitytype ),
                    new SqlParameter("@P_NCC_RATION_TYPE",RationType ),
                    new SqlParameter("@P_IDNOS", idno),
                    new SqlParameter("@P_UA_NO", uano),
                    new SqlParameter("@P_IPADDRESS",ipaddress),
                    new SqlParameter("@P_ADD_DATE",adddate),
                    new SqlParameter("@P_OUT", SqlDbType.Int)
                };
                        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;
                        status = (Int32)objDataAccess.ExecuteNonQuerySP("PKG_ACD_STUD_REG_FOR_NCC", sqlParams, true);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.SessionActivityController.AddSessionActivity() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return status;
                }

                //To Get Student Basic Details for NSS/NCC/CLUB Activity added by Sneha G.on 07/07/2020.
                public DataSet GetStudentBasicDetails(int idno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_NCC_STUD_DETAILS", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetCourseFor_RevalOrPhotoCopy->" + ex.ToString());
                    }
                    return ds;
                }

                public int UpdateRemovedatestatus(int Regid, DateTime removedate, string Remark)
                {
                    int status = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objDataAccess = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_REG_ID",Regid),
                    new SqlParameter("@P_REMOVE_DATE",removedate),
                    new SqlParameter("@P_REMARK",Remark),
                    new SqlParameter("@P_OUT", SqlDbType.Int)
                };
                        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;
                        status = (Int32)objDataAccess.ExecuteNonQuerySP("PKG_ACD_UPDATE_STUD_DATA_NCC", sqlParams, true);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.SessionActivityController.AddSessionActivity() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return status;
                }

                #endregion Student Register for Activity(NCC/NSS/CLUB)

                #region Excel Reports For NCC/NSS/CAMP Details ON 16072020

                // ADDED BY NARESH BEERLA ON 16072020

                public DataSet GetActiveStudents_Excel(int Activityno, int branchno, int semester)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_ACTIVITYNO", Activityno);
                        objParams[1] = new SqlParameter("@P_BRANCHNO", branchno);
                        objParams[2] = new SqlParameter("@P_SEMESTERNO", semester);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_STUDENT_NCC_NSS_CLUB_ACTIVE", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetCourseFor_RevalOrPhotoCopy->" + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetLeftStudents_Excel(int Activityno, int branchno, DateTime from, DateTime to)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_ACTIVITYNO", Activityno);
                        objParams[1] = new SqlParameter("@P_BRANCHNO", branchno);
                        objParams[2] = new SqlParameter("@P_FROMDATE", from);
                        objParams[3] = new SqlParameter("@P_TODATE", to);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_STUDENT_NCC_NSS_CLUB_LEFT_STUDENTS", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetCourseFor_RevalOrPhotoCopy->" + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetCampDetailsStudents_Excel(int Activityno, DateTime from, DateTime to)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_ACTIVITYNO", Activityno);
                        objParams[1] = new SqlParameter("@P_FROMDATE", from);
                        objParams[2] = new SqlParameter("@P_TODATE", to);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_CAMP_REPORT_DETAILS", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetCourseFor_RevalOrPhotoCopy->" + ex.ToString());
                    }
                    return ds;
                }

                // ADDED BY NARESH BEERLA ON 16072020

                #endregion Excel Reports For NCC/NSS/CAMP Details ON 16072020
            }
        }
    }
}