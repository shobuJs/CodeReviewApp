//======================================================================================
// PROJECT NAME  : UAIMS
// MODULE NAME   : BUSINESS LOGIC FILE [SLOT MASTER]
// CREATION DATE : 27/12/2011
// CREATED BY    : ASHISH DHAKATE
// MODIFIED DATE :
// MODIFIED DESC :
//======================================================================================

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
            public class SlotController
            {
                private string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                public SlotController()
                {
                    //TimeTable constructor
                }

                /// <summary>
                /// Project Name:ATTENDANCE
                /// Developed by: SWATI GHATE
                /// AIM: To INSERT the Time Slot master entry
                /// </summary>
                public int InsertSlot(SlotMaster objSM, int ActiveStatus)
                {
                    int pkid = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[13];
                        //@P_SRNO
                        objParams[0] = new SqlParameter("@P_SLOTNO", objSM.SLOTNO);
                        objParams[1] = new SqlParameter("@P_SLOTNAME", objSM.SLOTNAME);
                        objParams[2] = new SqlParameter("@P_TIMEFROM", objSM.TIMEFROM);
                        objParams[3] = new SqlParameter("@P_TIMETO", objSM.TIMETO);
                        //objParams[4] = new SqlParameter("@P_DEGREENO", objSM.DEGREENO);
                        objParams[4] = new SqlParameter("@P_DEGREENO", objSM.Degrees); //add by maithili [23-08-2022]
                        objParams[5] = new SqlParameter("@P_SESSIONNO", objSM.SESSIONNO);
                        objParams[6] = new SqlParameter("@P_COLLEGE_CODE", objSM.COLLEGE_CODE);
                        objParams[7] = new SqlParameter("@P_IDNO", objSM.IDNO);
                        objParams[8] = new SqlParameter("@P_SLOT_TYPE", objSM.SlotTypeNo);
                        //objParams[9] = new SqlParameter("@P_COLLEGE_ID", objSM.College_Id);//Added By Dileep Kare 16.04.2021
                        objParams[9] = new SqlParameter("@P_COLLEGE_ID", objSM.College_Ids);//Added By maithili [23-08-2022]
                        objParams[10] = new SqlParameter("@P_SEQUENCENO", objSM.SequenceNo); //Added By Mahesh on Dated-19-05-2021
                        objParams[11] = new SqlParameter("@P_ACTIVE_STATUS", ActiveStatus); // Added By Rishabh B. on 24/01/2022
                        objParams[12] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[12].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INSERT_UPDATE_SLOT", objParams, true);
                        if (ret != null)
                        {
                            if (ret.ToString().Equals("-99"))
                                pkid = -99;
                            else
                                pkid = Convert.ToInt32(ret.ToString());
                        }
                        else
                            pkid = -99;
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.SlotController.InsertClass-> " + ex.ToString());
                    }
                    return pkid;
                }//end

                public DataSet GetSlotDetails(string College_id, string Degreeno, int PageIndex, int PageSize, string SearchText)  //Add by maithili [29-08-2022]
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_COLLEGE_ID", College_id);//ADDED BY DILEEP KARE ON 16.04.2021
                        objParams[1] = new SqlParameter("@P_DEGREENO", Degreeno);//ADDED BY DILEEP KARE ON 16.04.2021
                        objParams[2] = new SqlParameter("@P_PAGEINDEX", PageIndex);//ADDED BY DILEEP KARE ON 24.09.2023
                        objParams[3] = new SqlParameter("@P_PAGESIZE", PageSize);//ADDED BY DILEEP KARE ON 24.09.2023
                        objParams[4] = new SqlParameter("@P_SEARCHTEXT", SearchText);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_SLOT_DETAILS", objParams);
                    }
                    catch (Exception ex)
                    {
                        //return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetSlotDetails-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }//END

                //public DataSet GetSlotDetails(string College_id, string Degreeno)  //Add by maithili [29-08-2022]
                //{
                //    DataSet ds = null;
                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                //        SqlParameter[] objParams = new SqlParameter[2];
                //        objParams[0] = new SqlParameter("@P_COLLEGE_ID", College_id);//ADDED BY DILEEP KARE ON 16.04.2021
                //        objParams[1] = new SqlParameter("@P_DEGREENO", Degreeno);//ADDED BY DILEEP KARE ON 16.04.2021
                //        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_SLOT_DETAILS", objParams);
                //    }
                //    catch (Exception ex)
                //    {
                //        //return ds;
                //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetSlotDetails-> " + ex.ToString());
                //    }
                //    finally
                //    {
                //        ds.Dispose();
                //    }
                //    return ds;
                //}//END

                //123456
                public int DeleteSlot(int IDNo)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_IDNO", IDNo);
                        objParams[1] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[1].Direction = ParameterDirection.Output;

                        object ret = Convert.ToInt32(objSQLHelper.ExecuteNonQuerySP("PKG_ACD_DELETE_SLOT", objParams, true));
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordDeleted);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.DeleteSlot-> " + ex.ToString());
                    }
                    return retStatus;
                }
            }
        }
    }
}