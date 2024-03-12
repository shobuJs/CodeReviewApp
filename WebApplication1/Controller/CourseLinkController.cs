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
            /// <summary>
            /// This CourseLinkController is used to control Course_Link table.
            /// </summary>
            public class CourseLinkController
            {
                /// <summary>
                /// ConnectionStrings
                /// </summary>
                private string _UAIMS_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                public CourseLinkController()
                { }

                /// <summary>
                /// This method is used to add new record in Course_Link table.
                /// </summary>
                /// <param name="objCL">objCL is the object of CourseLink class</param>
                /// <returns>Integer CustomStatus - Record Added or Error</returns>
                public int AddCourses(CourseLink objCL)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        //Add New Course
                        objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_CCODES", objCL.CourseCodes);
                        objParams[1] = new SqlParameter("@P_COURSENOS", objCL.CourseNos);
                        objParams[2] = new SqlParameter("@P_SECTIONNO", objCL.SectionNo);
                        objParams[3] = new SqlParameter("@P_SCHEMENO", objCL.SchemeNo);
                        objParams[4] = new SqlParameter("@P_COLLEGE_CODE", objCL.CollegeCode);
                        objParams[5] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[5].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PREREGIST_SP_INS_COURSELINK", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseLinkController.AddCourses-> " + ex.ToString());
                    }

                    return retStatus;
                }

                /// <summary>
                /// This method is used to get all courses.
                /// </summary>
                /// <param name="schemeno">Get all course according to current schemeno</param>
                /// <returns>DataSet</returns>
                public DataSet GetAllCourse(int schemeno, int sectionno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_SCHEMENO", schemeno);
                        objParams[1] = new SqlParameter("@P_SECTIONNO", sectionno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PREREGIST_SP_RET_COURSELINK", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseLinkController.GetAllCourse-> " + ex.ToString());
                    }
                    return ds;
                }
            }
        }//END: BusinessLayer.BusinessLogic
    }//END: UAIMS
}//END: IITMS