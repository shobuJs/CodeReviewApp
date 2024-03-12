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
            /// This StudentController is used to control Student detail.
            /// </summary>
            ///
            public class StudentController : IDisposable
            {
                /// <summary>
                /// ConnectionString
                /// </summary>
                private string _UAIMS_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                public int UpdateStudentCourseWiseSection(int sessiono, int degreeno, int branchno, int schemeno, int semesterno, int courseno, string studids, string sectionnos, string rollnos, int userno)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[11];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessiono);
                        objParams[1] = new SqlParameter("@P_DEGREENO", degreeno);
                        objParams[2] = new SqlParameter("@P_BRANCHNO", branchno);
                        objParams[3] = new SqlParameter("@P_SCHEMENO", schemeno);
                        objParams[4] = new SqlParameter("@P_SEMESTERNO", semesterno);
                        objParams[5] = new SqlParameter("@P_COURSENO", courseno);
                        objParams[6] = new SqlParameter("@P_STUDIDS", studids);
                        objParams[7] = new SqlParameter("@P_SECTIONNO", sectionnos);
                        objParams[8] = new SqlParameter("@P_ROLL_NO", rollnos);
                        objParams[9] = new SqlParameter("@P_UA_NO", userno);
                        objParams[10] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[10].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_STUDENT_UPDATE_COURSE_WISE_SECTION_FOR_BACKLOG", objParams, false);
                        if (ret != null)
                            if (ret.ToString() != "-99")
                                retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.UpdateStudentCourseWiseSection-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public string UpdateStudent(Student objStudent, StudentAddress objStudAddress, StudentPhoto objStudPhoto, StudentQualExm objStudQExm, string MotherMobile, string MotherOfficeNo, string IndusEmail, int usertype)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        //Update Student
                        objParams = new SqlParameter[187];
                        //First Add Student Parameter
                        objParams[0] = new SqlParameter("@P_IDNO", objStudent.IdNo);
                        objParams[1] = new SqlParameter("@P_REGNO", objStudent.RegNo);
                        objParams[2] = new SqlParameter("@P_ROLLNO", objStudent.RollNo);
                        objParams[3] = new SqlParameter("@P_STUDNAME", objStudent.StudName);

                        objParams[4] = new SqlParameter("@P_STUDNAME_HINDI", objStudent.StudNameHindi);
                        objParams[5] = new SqlParameter("@P_MOTHERNAME", objStudent.MotherName);
                        objParams[6] = new SqlParameter("@P_FATHERNAME", objStudent.FatherName);

                        objParams[7] = new SqlParameter("@P_DOB", objStudent.Dob);
                        objParams[8] = new SqlParameter("@P_AGE", objStudent.Age);
                        objParams[9] = new SqlParameter("@P_BIRTH_PLACE", objStudent.BirthPlace);
                        objParams[10] = new SqlParameter("@P_BIRTH_VILLAGE", objStudent.Birthvillage);
                        objParams[11] = new SqlParameter("@P_BIRTH_TALUKA", objStudent.Birthtaluka);
                        objParams[12] = new SqlParameter("@P_BIRTH_DISTRICT", objStudent.Birthdistrict);
                        objParams[13] = new SqlParameter("@P_BIRTH_STATE", objStudent.Birthdistate);
                        objParams[14] = new SqlParameter("@P_OTHER_LANGUAGE", objStudent.OtherLanguage);
                        objParams[15] = new SqlParameter("@P_BRANCHNO", objStudent.BranchNo);
                        objParams[16] = new SqlParameter("@P_SEX", objStudent.Sex);
                        objParams[17] = new SqlParameter("@P_MTOUNGENO", objStudent.MToungeNo);
                        objParams[18] = new SqlParameter("@P_MARRIED", objStudent.Married);
                        objParams[19] = new SqlParameter("@P_HEIGHT", objStudent.Height);
                        objParams[20] = new SqlParameter("@P_WEIGHT", objStudent.Weight);
                        objParams[21] = new SqlParameter("@P_IDENTI_MARK", objStudent.IdentyMark);
                        objParams[22] = new SqlParameter("@P_BLOODGRPNO", objStudent.BloodGroupNo);
                        objParams[23] = new SqlParameter("@P_CASTE", objStudent.Caste);
                        objParams[24] = new SqlParameter("@P_SUB_CASTE", objStudent.Subcaste);
                        objParams[25] = new SqlParameter("@P_CATEGORYNO", objStudent.CategoryNo);
                        objParams[26] = new SqlParameter("@P_NATIONALITYNO", objStudent.NationalityNo);
                        objParams[27] = new SqlParameter("@P_RELIGIONNO", objStudent.ReligionNo);
                        objParams[28] = new SqlParameter("@P_COUNTRYDOMICILE", objStudent.CountryDomicile);
                        objParams[29] = new SqlParameter("@P_HOSTELER", objStudent.Hosteler);
                        objParams[30] = new SqlParameter("@P_IRREGULAR", objStudent.Irregular);
                        objParams[31] = new SqlParameter("@P_URBAN", objStudent.Urban);
                        objParams[32] = new SqlParameter("@P_YEAR", objStudent.Year);
                        objParams[33] = new SqlParameter("@P_PRO", objStudent.Pro);
                        objParams[34] = new SqlParameter("@P_BATCHNO", objStudent.BatchNo);
                        objParams[35] = new SqlParameter("@P_SEMESTERNO", objStudent.SemesterNo);
                        objParams[36] = new SqlParameter("@P_DEGREENO", objStudent.DegreeNo);
                        objParams[37] = new SqlParameter("@P_SECTIONNO", objStudent.SectionNo);
                        objParams[38] = new SqlParameter("@P_SCHEMENO", objStudent.SchemeNo);
                        objParams[39] = new SqlParameter("@P_LASTROLLNO", objStudent.LastRollNo);
                        objParams[40] = new SqlParameter("@P_ROLL2", objStudent.Roll2);
                        objParams[41] = new SqlParameter("@P_ACC_NO", objStudent.AccNo);
                        objParams[42] = new SqlParameter("@P_VISANO", objStudent.Visano);
                        objParams[43] = new SqlParameter("@P_PASSPORTNO", objStudent.PassportNo);
                        objParams[44] = new SqlParameter("@P_STATENO", objStudent.StateNo);
                        objParams[45] = new SqlParameter("@P_IDTYPE", objStudent.IdType);
                        objParams[46] = new SqlParameter("@P_PTYPE", objStudent.PType);
                        objParams[47] = new SqlParameter("@P_ADMCAN", objStudent.AdmCancel);

                        if (objStudent.AdmDate == DateTime.MinValue)
                            objParams[48] = new SqlParameter("@P_ADMDATE", DBNull.Value);
                        else
                            objParams[48] = new SqlParameter("@P_ADMDATE", objStudent.AdmDate);

                        objParams[49] = new SqlParameter("@P_ADMBATCH", objStudent.AdmBatch);
                        objParams[50] = new SqlParameter("@P_LEAVEDATE", objStudent.LeaveDate);
                        objParams[51] = new SqlParameter("@P_CAN", objStudent.Can);
                        objParams[52] = new SqlParameter("@P_CANDATE", objStudent.CanDate);
                        objParams[53] = new SqlParameter("@P_REMARK", objStudent.Remark);
                        objParams[54] = new SqlParameter("@P_FAC_ADVISOR", objStudent.FacAdvisor);
                        objParams[55] = new SqlParameter("@P_PRJNAME", objStudent.Prjname);
                        objParams[56] = new SqlParameter("@P_FATHERMOBILE", objStudent.FatherMobile);
                        objParams[57] = new SqlParameter("@P_MOTHERMOBILE", MotherMobile);
                        objParams[58] = new SqlParameter("@P_FATHEROFFICENO", objStudent.FatherOfficeNo);
                        objParams[59] = new SqlParameter("@P_MOTHEROFFICENO", MotherOfficeNo);

                        //Student address parameter
                        objParams[60] = new SqlParameter("@P_LADDRESS", objStudAddress.LADDRESS);
                        objParams[61] = new SqlParameter("@P_LCITY", objStudAddress.LCITY);
                        objParams[62] = new SqlParameter("@P_LDISTRICT", objStudAddress.LDISTRICT);
                        objParams[63] = new SqlParameter("@P_LSTATE", objStudAddress.LSTATE);
                        objParams[64] = new SqlParameter("@P_LPINCODE", objStudAddress.LPINCODE);
                        objParams[65] = new SqlParameter("@P_LTELEPHONE", objStudAddress.LTELEPHONE);
                        objParams[66] = new SqlParameter("@P_LMOBILE", objStudAddress.LMOBILE);
                        objParams[67] = new SqlParameter("@P_LEMAIL", objStudAddress.LEMAIL);
                        objParams[68] = new SqlParameter("@P_PADDRESS", objStudAddress.PADDRESS);
                        objParams[69] = new SqlParameter("@P_PCITY", objStudAddress.PCITY);
                        objParams[70] = new SqlParameter("@P_PSTATE", objStudAddress.PSTATE);
                        objParams[71] = new SqlParameter("@P_PDISTRICT", objStudAddress.PDISTRICT);

                        objParams[72] = new SqlParameter("@P_PTELEPHONE", objStudAddress.PTELEPHONE);
                        objParams[73] = new SqlParameter("@P_PMOBILE", objStudAddress.PMOBILE);
                        objParams[74] = new SqlParameter("@P_PPINCODE", objStudAddress.PPINCODE);
                        objParams[75] = new SqlParameter("@P_PEMAIL", objStudAddress.PEMAIL);
                        objParams[76] = new SqlParameter("@P_FATHER_DESIG", objStudAddress.FATHER_DESIG);
                        objParams[77] = new SqlParameter("@P_OCCUPATIONNO", objStudAddress.OCCUPATION);

                        objParams[78] = new SqlParameter("@P_FATHERJOBDETAIL", objStudAddress.FATHERJOBDETAIL);
                        objParams[79] = new SqlParameter("@P_MOTHER_DESIG", objStudAddress.MOTHERDESIGNATION);
                        objParams[80] = new SqlParameter("@P_MOTHER_OCCUPATIONNO", objStudAddress.MOTHEROCCUPATION);
                        objParams[81] = new SqlParameter("@P_MOTHERJOBDETAIL", objStudAddress.MOTHERJOBDETAIL);

                        objParams[82] = new SqlParameter("@P_GUARDIANNAME", objStudAddress.GUARDIANNAME);
                        objParams[83] = new SqlParameter("@P_RELATION_GUARDIAN", objStudAddress.RELATION_GUARDIAN);
                        objParams[84] = new SqlParameter("@P_GADDRESS", objStudAddress.GADDRESS);
                        objParams[85] = new SqlParameter("@P_GOCCUPATIONNAME", objStudAddress.GOCCUPATIONNAME);
                        objParams[86] = new SqlParameter("@P_GPHONE", objStudAddress.GPHONE);
                        objParams[87] = new SqlParameter("@P_GEMAIL", objStudAddress.GEMAIL);
                        objParams[88] = new SqlParameter("@P_RAILWAY_STATION", objStudAddress.RAILWAY_STATION);
                        objParams[89] = new SqlParameter("@P_BUS_STATION", objStudAddress.BUS_STATION);
                        objParams[90] = new SqlParameter("@P_LOCALNAME_STATION", objStudAddress.LOCALNAME_STATION);

                        //Student Last Qualification Exam Parameter
                        objParams[91] = new SqlParameter("@P_QUALIFYNO", objStudQExm.QUALIFYNO);
                        objParams[92] = new SqlParameter("@P_YEAR_OF_EXAM", objStudent.YearOfExam);
                        objParams[93] = new SqlParameter("@P_QEXMROLLNO", objStudent.QexmRollNo);
                        objParams[94] = new SqlParameter("@P_SCHOOL_COLLEGE_NAMEHSSC", objStudQExm.SCHOOL_COLLEGE_NAME);
                        objParams[95] = new SqlParameter("@P_BOARDHSSC", objStudQExm.BOARD);
                        objParams[96] = new SqlParameter("@P_GRADEHSSC", objStudQExm.GRADE);
                        objParams[97] = new SqlParameter("@P_ATTEMPTHSSC", objStudQExm.ATTEMPT);
                        objParams[98] = new SqlParameter("@P_MERIT_NOHSSC", objStudQExm.MERITNO);
                        objParams[99] = new SqlParameter("@P_MARKS_OBTAINEDHSSC", objStudQExm.MARKOBTAINED);
                        objParams[100] = new SqlParameter("@P_OUT_OF_MRKSHSSC", objStudQExm.OUTOFMARK);

                        objParams[101] = new SqlParameter("@P_ALL_INDIA_RANK", objStudQExm.ALLINDIARANK);
                        objParams[102] = new SqlParameter("@P_STATE_RANK", objStudQExm.STATERANK);
                        objParams[103] = new SqlParameter("@P_SCORE", objStudent.Score);
                        objParams[104] = new SqlParameter("@P_PAPER", objStudent.Paper);
                        objParams[105] = new SqlParameter("@P_PAPER_CODE", objStudent.Paper_code);
                        objParams[106] = new SqlParameter("@P_PERHSSC", objStudQExm.PERCENTAGE);
                        objParams[107] = new SqlParameter("@P_PERCENTILEHSSC", objStudQExm.PERCENTILE);
                        objParams[108] = new SqlParameter("@P_HSC_PCM", objStudQExm.HSCPCM);
                        objParams[109] = new SqlParameter("@P_HSC_PCM_MAX", objStudQExm.HSCPCMMAX);
                        objParams[110] = new SqlParameter("@P_HSC_BIO", objStudQExm.HSCBIO);
                        objParams[111] = new SqlParameter("@P_HSC_BIO_MAX", objStudQExm.HSCBIOMAX);
                        objParams[112] = new SqlParameter("@P_HSC_ENG", objStudQExm.ENG);
                        objParams[113] = new SqlParameter("@P_HSC_ENG_MAX", objStudQExm.HSCENGMAX);
                        objParams[114] = new SqlParameter("@P_HSC_MAT", objStudQExm.MATHS);
                        objParams[115] = new SqlParameter("@P_HSC_MAT_MAX", objStudQExm.MATHSMAX);
                        objParams[116] = new SqlParameter("@P_HSC_CHE", objStudQExm.HSCCHE);
                        objParams[117] = new SqlParameter("@P_HSC_CHE_MAX", objStudQExm.HSCCHEMAX1);
                        objParams[118] = new SqlParameter("@P_HSC_PHY", objStudQExm.HSCPHY1);
                        objParams[119] = new SqlParameter("@P_HSC_PHY_MAX", objStudQExm.HSCPHYMAX1);

                        //Student Photo Parameter
                        objParams[120] = new SqlParameter("@P_PHOTOPATH", objStudPhoto.PhotoPath);
                        objParams[121] = new SqlParameter("@P_PHOTOSIZE", objStudPhoto.PhotoSize);

                        if (objStudPhoto.Photo1 == null)
                            objParams[122] = new SqlParameter("@P_PHOTO", DBNull.Value);
                        else
                            objParams[122] = new SqlParameter("@P_PHOTO", objStudPhoto.Photo1);

                        objParams[122].SqlDbType = SqlDbType.Image;

                        objParams[123] = new SqlParameter("@P_EMAILID", objStudent.EmailID);
                        objParams[124] = new SqlParameter("@P_QUALIFYEXAMNAME", objStudent.Qualifyexamname);
                        objParams[125] = new SqlParameter("@P_PERCENTAGE", objStudent.Percentage);
                        objParams[126] = new SqlParameter("@P_YEAR_OF_EXAMHSSC", objStudQExm.YEAR_OF_EXAMHSSC);
                        objParams[127] = new SqlParameter("@P_QEXMROLLNOHSSC", objStudQExm.QEXMROLLNO);
                        objParams[128] = new SqlParameter("@P_PERCENTILE", objStudent.Percentile);
                        objParams[129] = new SqlParameter("@P_YEAR_OF_EXAMSSC", objStudQExm.YearOfExamSsc);
                        objParams[130] = new SqlParameter("@P_SCHOOL_COLLEGE_NAMESSC", objStudQExm.SchoolCollegeNameSsc);
                        objParams[131] = new SqlParameter("@P_BOARDSSC ", objStudQExm.BoardSsc);
                        objParams[132] = new SqlParameter("@P_GRADESSC", objStudQExm.GradeSsc);
                        objParams[133] = new SqlParameter("@P_ATTEMPTSSC", objStudQExm.AttemptSsc);
                        objParams[134] = new SqlParameter("@P_MARKS_OBTAINEDSSC", objStudQExm.MarksObtainedSsc);
                        objParams[135] = new SqlParameter("@P_OUT_OF_MRKSSSC", objStudQExm.OutOfMarksSsc);
                        objParams[136] = new SqlParameter("@P_PERSSC", objStudQExm.PercentageSsc);
                        objParams[137] = new SqlParameter("@P_PERCENTILESSC", objStudQExm.PercentileSsc);
                        objParams[138] = new SqlParameter("@P_QEXMROLLNOSSC", objStudQExm.QEXMROLLNOSSC);
                        objParams[139] = new SqlParameter("@P_ADMQUOTANO", objStudent.ADMQUOTANO);
                        objParams[140] = new SqlParameter("@P_STUDENTMOBILE", objStudent.StudentMobile);
                        objParams[141] = new SqlParameter("@P_UANO", objStudent.Uano);
                        objParams[142] = new SqlParameter("@P_BANKNO", objStudent.BankNo);
                        objParams[143] = new SqlParameter("@P_ENROLLNO", objStudent.EnrollNo);
                        objParams[144] = new SqlParameter("@P_ADMCATEGORYNO", objStudent.AdmCategoryNo);
                        objParams[145] = new SqlParameter("@P_SCHOLORSHIPTYPENO", objStudent.Scholorship);
                        objParams[146] = new SqlParameter("@P_TYPE_OF_PHYSICALLY_HANDICAP", objStudent.Physical_Handicap);
                        objParams[147] = new SqlParameter("@P_LPOSTOFF", objStudAddress.LPOSTOFF);
                        objParams[148] = new SqlParameter("@P_LPOLICESTATION", objStudAddress.LPOLICESTATION);
                        objParams[149] = new SqlParameter("@P_LTEHSIL", objStudAddress.LTEHSIL);
                        objParams[150] = new SqlParameter("@P_PPOSTOFF", objStudAddress.PPOSTOFF);
                        objParams[151] = new SqlParameter("@P_PPOLICEOFF", objStudAddress.PPOLICESTATION);
                        objParams[152] = new SqlParameter("@P_PTEHSIL", objStudAddress.PTEHSIL);
                        objParams[153] = new SqlParameter("@P_ADDHARCARDNO", objStudent.AddharcardNo);
                        objParams[154] = new SqlParameter("@P_ANNUAL_INCOME", objStudAddress.ANNUAL_INCOME);
                        objParams[155] = new SqlParameter("@P_GUARDIANDESIG", objStudAddress.GUARDIANDESIGNATION);
                        objParams[156] = new SqlParameter("@P_CORRES_ADDRESS", objStudent.Corres_address);
                        objParams[157] = new SqlParameter("@P_CORRES_PIN", objStudent.Corres_pin);
                        objParams[158] = new SqlParameter("@P_CORRES_MOB", objStudent.Corres_mob);
                        objParams[159] = new SqlParameter("@P_YEARS_OF_STUDY", objStudent.Yearsofstudey);
                        objParams[160] = new SqlParameter("@P_STATE_OTHERSTATE", objStudent.Stateotherstate);
                        objParams[161] = new SqlParameter("@P_COLLEGE_ADD", objStudent.Colg_address);
                        objParams[162] = new SqlParameter("@P_SPECIALIZATION", objStudent.Specialization);
                        objParams[163] = new SqlParameter("@P_BIRTH_PINCODE", objStudent.BirthPinCode);

                        objParams[164] = new SqlParameter("@P_INDIAN_ORIGIN", objStudent.IndianOrigin);
                        objParams[165] = new SqlParameter("@P_VISA_EXPIRY_DATE", objStudent.VisaExpiryDate);
                        objParams[166] = new SqlParameter("@P_PASSPORT_EXPIRY_DATE", objStudent.PassportExpiryDate);
                        objParams[167] = new SqlParameter("@P_PASSPORT_ISSUE_DATE", objStudent.PassportIssueDate);
                        objParams[168] = new SqlParameter("@P_STAY_PERMIT_DATE", objStudent.StayPermitDate);
                        objParams[169] = new SqlParameter("@P_SCHOL_SCHEME", objStudent.ScholarshipScheme);
                        objParams[170] = new SqlParameter("@P_AGENCY", objStudent.Agency);
                        objParams[171] = new SqlParameter("@P_PASSPORT_ISSUE_PLACE", objStudent.PassportIssuePlace);
                        objParams[172] = new SqlParameter("@P_CITIZENSHIP", objStudent.Citizenship);
                        objParams[173] = new SqlParameter("@P_SSC_MEDIUM", objStudQExm.SSC_medium);
                        objParams[174] = new SqlParameter("@P_HSSC_MEDIUM", objStudQExm.HSSC_medium);
                        objParams[175] = new SqlParameter("@P_STUDMIDDLENAME", objStudent.MiddleName);
                        objParams[176] = new SqlParameter("@P_STUDLASTNAME", objStudent.LastName);
                        objParams[177] = new SqlParameter("@P_FATHERMIDDLENAME ", objStudent.FatherMiddleName);
                        objParams[178] = new SqlParameter("@P_FATHERLASTNAME", objStudent.FatherLastName);
                        objParams[179] = new SqlParameter("@P_FNAME", objStudent.firstName);
                        objParams[180] = new SqlParameter("@P_FATHERFIRSTNAME", objStudent.fatherfirstName);
                        //aayushi
                        objParams[181] = new SqlParameter("@P_MOTHEREMAIL ", objStudent.Motheremail);
                        objParams[182] = new SqlParameter("@P_FATHEREMAIL ", objStudent.Fatheremail);
                        /////////////
                        objParams[183] = new SqlParameter("@P_INDUSEMAIL", IndusEmail);
                        objParams[184] = new SqlParameter("@P_CLAIMID", objStudent.ClaimType);
                        objParams[185] = new SqlParameter("@P_USERTYPE", usertype);
                        objParams[186] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[186].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_STUDENT_SP_UPD_STUDENT", objParams, true);

                        if (objStudent.LastQualifiedExams != null)
                        {
                            foreach (QualifiedExam qualExam in objStudent.LastQualifiedExams)
                            {
                                this.UpdateLastQualExams(qualExam);
                            }
                        }
                        retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        return ret.ToString();
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.UpdateStudent-> " + ex.ToString());
                    }
                }

                public DataSet Get_Intake_Admission_Data(int degreeno, int srcategoryno, string college_jss)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_DEGREENO", degreeno);
                        objParams[1] = new SqlParameter("@P_SRCATEGORYNO", srcategoryno);
                        objParams[2] = new SqlParameter("@P_COLLEGE_JSS", college_jss);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_INTAKE_ADMISION_DATA", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.EmpCreateController.RetrieveEmpDetails-> " + ex.ToString());
                    }
                    return ds;
                }

                public int Insert_update_intake_admission(int intakeid, int degreeno, int branchno, int srcategoryno, string college_jss, string intake)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[7];
                        objParams[0] = new SqlParameter("@P_INTAKEID", intakeid);
                        objParams[1] = new SqlParameter("@P_DEGREENO", degreeno);
                        objParams[2] = new SqlParameter("@P_BRANCHNO", branchno);
                        objParams[3] = new SqlParameter("@P_SRCATEGORYNO", srcategoryno);
                        objParams[4] = new SqlParameter("@P_COLLEGE_JSS", college_jss);
                        objParams[5] = new SqlParameter("@P_INTAKE", intake);
                        objParams[6] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[6].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("pkg_acd_ins_intake_admission", objParams, true);

                        if (Convert.ToInt32(ret) == 1)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        else if (Convert.ToInt32(ret) == 2)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.Insert_Update_StudentDocuments-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetCourseFor_RevalPhotocopyChallenge(int idno, int sessionno, int semesterno, int degreeno, int schemeno)
                {
                    DataSet ds = null;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        objParams[1] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[2] = new SqlParameter("@P_SEMESTERNO", semesterno);
                        objParams[3] = new SqlParameter("@P_DEGREENO", degreeno);
                        objParams[4] = new SqlParameter("@P_SCHEMENO", schemeno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_GET_COURSE_FOR_REVALUATION_PHOTOCOPY_CHALLENGE_NEW", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentDetails->" + ex.ToString());
                    }
                    return ds;
                }

                public int InsAdmitCardLog(int degreeNo, int branchno, string ids, string ipadd, int userid)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);

                        SqlParameter[] objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_DEGREENO", degreeNo);
                        objParams[1] = new SqlParameter("@P_BRANCHNO", branchno);

                        objParams[2] = new SqlParameter("@P_IDNOS", ids);

                        objParams[3] = new SqlParameter("@P_IPADDRSS", ipadd);

                        objParams[4] = new SqlParameter("@P_USER_ID", userid);

                        object ret = objSQLHelper.ExecuteDataSetSP("PKG_ADMITCARD_LOG", objParams);
                        if (ret != null)
                            if (ret.ToString() != "-99")
                                retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.InsGradeTranscriptLog-> " + ex.ToString());
                    }

                    return retStatus;
                }

                public int InsAdmitCanLog(int semesterNo, int sessionno, int idno, string ipadd, int userid, string remark)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);

                        SqlParameter[] objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_SEMESTERNO", semesterNo);
                        objParams[1] = new SqlParameter("@P_sessionNO", sessionno);

                        objParams[2] = new SqlParameter("@P_IDNO", idno);

                        objParams[3] = new SqlParameter("@P_IPADDRESS", ipadd);

                        objParams[4] = new SqlParameter("@P_Ua_no", userid);
                        objParams[5] = new SqlParameter("@P_REMARK", remark);
                        object ret = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_INS_ADM_CAN STATUS", objParams);
                        if (ret != null)
                            if (ret.ToString() != "-99")
                                retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.InsGradeTranscriptLog-> " + ex.ToString());
                    }

                    return retStatus;
                }

                //ADDED

                public DataSet GetStudentListForAdmitCardForSTudent(int sessionno, int idno)
                {
                    DataSet ds = null;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];

                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        objParams[1] = new SqlParameter("@P_SESSIONNO", sessionno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STUDENT_SEARCH_ADMIT_CARD_BY_STUDENT", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.EmpCreateController.GetStudentListForIdentityCard-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetStudentListForAdmitCard(int degreeno, int branchno, int semesterno, int prev_status, int sessionno, int colg)
                {
                    DataSet ds = null;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_DEGREENO", degreeno);
                        objParams[1] = new SqlParameter("@P_BRANCHNO", branchno);
                        objParams[2] = new SqlParameter("@P_SEMESTERNO", semesterno);
                        objParams[3] = new SqlParameter("@P_PREV_STATUS", prev_status);
                        objParams[4] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[5] = new SqlParameter("@P_COLLEGE_ID", colg);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STUDENT_SEARCH_ADMIT_CARD", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.EmpCreateController.GetStudentListForIdentityCard-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetStudentStrengthDetails(int SESSIONNO, string GROUPCOLUMNS)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", SESSIONNO);
                        objParams[1] = new SqlParameter("@P_GROUPCOLUMNS", GROUPCOLUMNS);
                        ds = objSQLHelper.ExecuteDataSetSP("PR_STUDENT_POSITION_SUMMARY", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentStrengthDetails() -->" + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetStudentInfoDocumentListByEnrollNo(string ENROLLNO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objDataAccess = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_ENROLLNO ", ENROLLNO)
                };
                        ds = objDataAccess.ExecuteDataSetSP("PKG_STUDENT_DOCUMENT_LIST_GET_STUDENT_INFO", sqlParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.GetStudentInfoDocumentListByEnrollNo() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }

                public int InsertDocumenttype(int studid, string docid, string chkOriCopy)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_IDNO", studid);
                        objParams[1] = new SqlParameter("@P_DOCUMENTNO", docid);
                        objParams[2] = new SqlParameter("@P_ORICOPY", chkOriCopy);

                        objParams[3] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                        objParams[3].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INSERT_DOCUMENT", objParams, true);

                        if (Convert.ToInt32(ret) == 1)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        else if (Convert.ToInt32(ret) == 2)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ACADEMIC.ANSWERSHEET.StudentController-> " + ex.ToString());
                    }

                    return retStatus;
                }

                public int Insert_Update_StudentDocuments(int idno, int hiddtudocno, int chkDocuments, string extension, string contentType, byte[] document)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[7];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        objParams[1] = new SqlParameter("@P_STU_DOC_NO", hiddtudocno);
                        objParams[2] = new SqlParameter("@P_CHK", chkDocuments);
                        objParams[3] = new SqlParameter("@P_EXTENSION", extension);
                        objParams[4] = new SqlParameter("@P_CONTENTTYPE", contentType);
                        objParams[5] = new SqlParameter("@P_FILEDATA", document);
                        objParams[6] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[6].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INSERT_UPDATE_STUDENT_FILE_UPLOAD_DOCUMENT", objParams, true);

                        if (Convert.ToInt32(ret) == 1)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        else if (Convert.ToInt32(ret) == 2)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.Insert_Update_StudentDocuments-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet RetrieveStudentDetails(string search, string category)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_SEARCHSTRING", search);
                        objParams[1] = new SqlParameter("@P_SEARCH", category);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STUDENT_SP_SEARCH_STUDENT", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.EmpCreateController.RetrieveEmpDetails-> " + ex.ToString());
                    }
                    return ds;
                }

                public int GetStudentFeeStatus(int degreeno, int batchno, int paytypeno)
                {
                    int ret = 0;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_DEGREENO", degreeno);
                        objParams[1] = new SqlParameter("@P_BATCHNO", batchno);
                        objParams[2] = new SqlParameter("@P_PAYTYPENO", paytypeno);

                        ret = Convert.ToInt32(objSQLHelper.ExecuteScalarSP("PKG_STUDENT_STDFEE", objParams));
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentFeeStatus->" + ex.ToString());
                    }
                    return ret;
                }

                public bool GetStudentRegNoStatus(bool autogenerate)
                {
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[0];

                        autogenerate = Convert.ToBoolean(objSQLHelper.ExecuteScalarSP("PKG_STUDENT_SP_REGNO_STUDENT", objParams));
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentRegNo->" + ex.ToString());
                    }
                    return autogenerate;
                }

                public SqlDataReader GetAllStudentAdmitted(DateTime startdate, DateTime enddate, string ugpgot)
                {
                    SqlDataReader dr = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_STARTDATE", startdate);
                        objParams[1] = new SqlParameter("@P_ENDDATE", enddate);
                        objParams[2] = new SqlParameter("@P_UGPGOT", ugpgot);

                        dr = objSQLHelper.ExecuteReaderSP("PKG_ACAD_ALL_BRANCH_ALLYRTOTAL", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetAllStudentAdmitted-> " + ex.ToString());
                    }

                    return dr;
                }

                public SqlDataReader GetFirstYrStudentAdmitted(int categoryno, int branchno, string shortname, DateTime startdate, DateTime enddate, string ugpgot)
                {
                    SqlDataReader dr = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_CATEGORYNO", categoryno);
                        objParams[1] = new SqlParameter("@P_BRANCHNO", branchno);
                        objParams[2] = new SqlParameter("@P_SHORTNAME", shortname);
                        objParams[3] = new SqlParameter("@P_STARTDATE", startdate);
                        objParams[4] = new SqlParameter("@P_ENDDATE", enddate);

                        dr = objSQLHelper.ExecuteReaderSP("PKG_ACAD_ALL_BRANCH_FIRSTYRTOTAL", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetFirstYrStudentAdmitted-> " + ex.ToString());
                    }

                    return dr;
                }

                public int GetTotalNoStudents(int sessionno, int schemeno, int coursecode)
                {
                    int retTot = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_SCHEMENo", schemeno);
                        objParams[2] = new SqlParameter("@P_COURSECODE", coursecode);
                        objParams[3] = new SqlParameter("@P_CNT", SqlDbType.Int);
                        objParams[3].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteScalarSP("PKG_STUDENT_SP_RET_TOTSTUDENTS", objParams);
                        //if (objParams[3].Value != null)
                        retTot = Convert.ToInt32(objParams[3].Value);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetTotalNoStudents-> " + ex.ToString());
                    }
                    return retTot;
                }

                public DataSet GetStudentsForScheme(int batchno, int branchno, int semno, int flag, int branchindex, int semindex)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);

                        SqlParameter[] objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_ADMBATCHNO", batchno);
                        objParams[1] = new SqlParameter("@P_BRANCHNO", branchno);
                        objParams[2] = new SqlParameter("@P_SEMNO", semno);
                        objParams[3] = new SqlParameter("@P_FLAG", flag);
                        objParams[4] = new SqlParameter("@P_BRANCHINDEX", branchindex);
                        objParams[5] = new SqlParameter("@P_SEMINDEX", semindex);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STUDENT_SP_RET_STUDENT_FORSCHEME", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentsForScheme-> " + ex.ToString());
                    }

                    return ds;
                }

                public int UpdateSchemes(Student_Acd objStudent)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_SCHEMENO", objStudent.SchemeNo);
                        objParams[1] = new SqlParameter("@P_IDNO", objStudent.StudId);
                        objParams[2] = new SqlParameter("@P_SEM", objStudent.Sem);
                        objParams[3] = new SqlParameter("@P_FLAG", SqlDbType.Int);
                        objParams[3].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_STUDENT_SP_UPD_STUDENTSCHEME", objParams, true);
                        if (ret != null)
                            retStatus = Convert.ToInt32(ret);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.UpdateSchemes-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetStudentsForTeacherAllotmentOld(int session, int scheme, int courseno, string fromrollno, string torollno, string rollnos, int subid, int batch)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);

                        SqlParameter[] objParams = new SqlParameter[8];
                        objParams[0] = new SqlParameter("@P_SESSION", session);
                        objParams[1] = new SqlParameter("@P_SCHEME", scheme);
                        objParams[2] = new SqlParameter("@P_COURSENO", courseno);
                        objParams[3] = new SqlParameter("@P_FROMROLLNO", fromrollno);
                        objParams[4] = new SqlParameter("@P_TOROLLNO", torollno);
                        objParams[5] = new SqlParameter("@P_ROLLNOS", rollnos);
                        objParams[6] = new SqlParameter("@P_SUBID", subid);
                        objParams[7] = new SqlParameter("@P_BATCH", batch);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STUDENT_SP_RET_TEACHERALLOTMENT", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentsForTeacherAllotment-> " + ex.ToString());
                    }

                    return ds;
                }

                public DataSet GetStudentsForTeacherAllotment(int sessionno, int schemeno, int courseno, int sectionno, int semesterno, int thpr, int batchno, int orderby)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);

                        SqlParameter[] objParams = new SqlParameter[8];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_SCHEMENO", schemeno);
                        objParams[2] = new SqlParameter("@P_COURSENO", courseno);
                        objParams[3] = new SqlParameter("@P_SECTIONNO", sectionno);
                        objParams[4] = new SqlParameter("@P_SEMESTERNO", semesterno);
                        objParams[5] = new SqlParameter("@P_THPR", thpr);
                        objParams[6] = new SqlParameter("@P_BATCHNO", batchno);
                        objParams[7] = new SqlParameter("@P_ORDERBY", orderby);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STUDENT_SP_RET_TEACHERALLOTMENT", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentsForTeacherAllotment-> " + ex.ToString());
                    }

                    return ds;
                }

                public int UpdateStudent_TeachAllot(Student_Acd objStudent)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[9];

                        objParams[0] = new SqlParameter("@P_SESSIONNO", objStudent.SessionNo);
                        objParams[1] = new SqlParameter("@P_SCHEMENO", objStudent.SchemeNo);
                        objParams[2] = new SqlParameter("@P_SEMESTERNO", objStudent.Sem);
                        objParams[3] = new SqlParameter("@P_SECTIONNO", objStudent.Sectionno);
                        objParams[4] = new SqlParameter("@P_COURSENO", objStudent.CourseNo);
                        objParams[5] = new SqlParameter("@P_UA_NO", objStudent.UA_No);
                        objParams[6] = new SqlParameter("@P_STUDID", objStudent.StudId);
                        objParams[7] = new SqlParameter("@P_TH_PR", objStudent.Th_Pr);
                        objParams[8] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[8].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_STUDENT_SP_UPD_BYFACULTY", objParams, false);
                        if (ret != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.UpdateStudent_TeachAllot-> " + ex.ToString());
                    }
                    return retStatus;
                }

                /// <summary>
                /// This method is used to get student.
                /// </summary>
                /// <param name="facno">Gets student as per this facno</param>
                /// <param name="batch">Gets student as per this batch</param>
                /// <param name="branch">Gets student as per this branch</param>
                /// <param name="sem">Gets student as per this sem</param>
                /// <param name="sectionno">Gets student as per this sectionno</param>
                /// <param name="flag">Gets student as per this flag</param>
                /// <param name="rb">Gets student as per this rb</param>
                /// <returns>DataSet</returns>
                public DataSet GetStudentForFaculty(int facno, int branchno, int sem, int degreeno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_FAC", facno);
                        objParams[1] = new SqlParameter("@P_BRANCHNO", branchno);
                        objParams[2] = new SqlParameter("@P_SEM", sem);
                        objParams[3] = new SqlParameter("@P_DEGREENO", degreeno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STUDENT_SP_RET_STUDENT_FOR_FA", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentForFaculty-> " + ex.ToString());
                    }
                    return ds;
                }

                public int UpdateStudent_FacultAllot(Student objStudent)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[3];

                        objParams[0] = new SqlParameter("@P_STUDID", objStudent.StudId);
                        objParams[1] = new SqlParameter("@P_FACULTYADVISOR", objStudent.FacAdvisor);
                        objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[2].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_STUDENT_SP_UPD_BYFACULTY_ADVISOR", objParams, false);
                        if (ret != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.UpdateStudent_TeachAllot-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetStudentForCOEByScheme(int sessionno, int schemeno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_SCHEMENO", schemeno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PREREGIST_SP_RET_ALL_STUDENTS_BYCOE", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentForCOEByScheme-> " + ex.ToString());
                    }
                    return ds;
                }

                private void UpdateLastQualExams(QualifiedExam qualExam)
                {
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[]
                        {
                            new SqlParameter("@P_IDNO", qualExam.Idno),
                            new SqlParameter("@P_QUALIFYNO", qualExam.Qualifyno),
                            new SqlParameter("@P_YEAR_OF_EXAMHSSC", qualExam.Year_of_exam),
                            new SqlParameter("@P_SCHOOL_COLLEGE_NAME", qualExam.School_college_name),
                            new SqlParameter("@P_BOARD", qualExam.Board),
                            new SqlParameter("@P_GRADE", qualExam.Grade),
                            new SqlParameter("@P_ATTEMPT", qualExam.Attempt),
                            new SqlParameter("@P_MARKS_OBTAINED", qualExam.MarksObtained),
                            new SqlParameter("@P_OUT_OF_MRKS", qualExam.Out_of_marks),
                            new SqlParameter("@P_PER", qualExam.Per),
                            new SqlParameter("@P_PERCENTILE", qualExam.Percentile),
                            new SqlParameter("@P_RES_TOPIC", qualExam.Res_topic),
                            new SqlParameter("@P_SUPERVISOR_NAME1", qualExam.Supervisor_name1),
                            new SqlParameter("@P_SUPERVISOR_NAME2", qualExam.Supervisor_name2),
                            new SqlParameter("@P_COLLEGECODE",qualExam.College_code),
                            new SqlParameter("@P_COLLEGE_ADD",qualExam.College_address),
                            new SqlParameter("@P_QUAL_MEDIUM",qualExam.Qual_medium),
                             new SqlParameter("@P_QEXMROLLNOOTH",qualExam.Qexmrollno),
                            new SqlParameter("@P_STQEXNO", qualExam.Stqexno)
                           };
                        objParams[objParams.Length - 1].Direction = ParameterDirection.Output;
                        objSQLHelper.ExecuteNonQuerySP("pkg_STUDENT_UPD_LAST_QUALEXM", objParams, true);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.UpdateLastQualExams --> " + ex.ToString());
                    }
                }

                public DataSet GetAllQualifyExamDetails(int idno)
                {
                    DataSet dsCT = null;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);

                        dsCT = objSQLHelper.ExecuteDataSetSP("PKG_STUDENT_GET_QUALIFYEXAM", objParams);
                    }
                    catch (Exception ex)
                    {
                        return dsCT;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.studentcontroller.GetAllQualifyExamDetails-> " + ex.ToString());
                    }

                    return dsCT;
                }

                public int UpdateStudent_Detained(Student objStudent)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_STUDID", objStudent.StudId);
                        objParams[1] = new SqlParameter("@P_STATUS", objStudent.FacAdvisor);
                        objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[2].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_STUDENT_SP_UPD_STUDENT_SUBJECT_STATUS", objParams, false);
                        if (ret != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.UpdateStudent_Detained-> " + ex.ToString());
                    }
                    return retStatus;
                }

                //public DataSet GetStudentformeritList(int sessionnno, int degreeno, int Qualifyno, int cutoffno, int uano, int Commandtype, string Branchnos, int filterNo)
                //{
                //    DataSet ds = null;
                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                //        SqlParameter[] objParams = null;
                //        objParams = new SqlParameter[8];
                //        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionnno);
                //        objParams[1] = new SqlParameter("@P_DEGREENO", degreeno);
                //        objParams[2] = new SqlParameter("@P_QUALIFYNO", Qualifyno);
                //        objParams[3] = new SqlParameter("@P_CUTOFFMARKS", cutoffno);
                //        objParams[4] = new SqlParameter("@P_UA_NO", uano);
                //        objParams[5] = new SqlParameter("@P_COMMAND_TYPE", Commandtype);
                //        objParams[6] = new SqlParameter("@P_BRANCHNOS", Branchnos);
                //        objParams[7] = new SqlParameter("@P_FILTERNO", filterNo);
                //        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_REPORT_GENERATE_MERIT_LIST", objParams);
                //    }
                //    catch (Exception ex)
                //    {
                //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentDocVerification() -->" + ex.ToString());
                //    }
                //    return ds;
                //}

                public DataSet GetStudentformeritList(int sessionnno, int degreeno, int Qualifyno, int cutoffno, int uano, int Commandtype, string Branchnos, int filterNo, int Tansfre_fresher)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[9];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionnno);
                        objParams[1] = new SqlParameter("@P_DEGREENO", degreeno);
                        objParams[2] = new SqlParameter("@P_QUALIFYNO", Qualifyno);
                        objParams[3] = new SqlParameter("@P_CUTOFFMARKS", cutoffno);
                        objParams[4] = new SqlParameter("@P_UA_NO", uano);
                        objParams[5] = new SqlParameter("@P_COMMAND_TYPE", Commandtype);
                        objParams[6] = new SqlParameter("@P_BRANCHNOS", Branchnos);
                        objParams[7] = new SqlParameter("@P_FILTERNO", filterNo);
                        objParams[8] = new SqlParameter("@P_TANSFRE_FRESHER", Tansfre_fresher);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_REPORT_GENERATE_MERIT_LIST", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentDocVerification() -->" + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetStudentsByScheme(int admbatch, int schemeno, int degreeno, int schemetype, int semno, int college_id, int sectionno, int Regtype, int orderby)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);

                        SqlParameter[] objParams = new SqlParameter[9];
                        objParams[0] = new SqlParameter("@P_ADMBATCH", admbatch);
                        objParams[1] = new SqlParameter("@P_SCHEMENO", schemeno);
                        objParams[2] = new SqlParameter("@P_DEGREENO", degreeno);
                        objParams[3] = new SqlParameter("@P_SCHEMETYPE", schemetype);
                        objParams[4] = new SqlParameter("@P_SEM", semno);
                        objParams[5] = new SqlParameter("@P_COLLEGEID", college_id);
                        objParams[6] = new SqlParameter("@P_SECTIONNO", sectionno);
                        objParams[7] = new SqlParameter("@P_REG_TYPE", Regtype);
                        objParams[8] = new SqlParameter("@P_ORDERBY", orderby);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STUDENT_SP_RET_STUDENT_BY_SCHEME1", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentsForScheme-> " + ex.ToString());
                    }

                    return ds;
                }

                public DataSet GetStudentsBySchemeAllot(int collegeno, int admbatch, int branchno, int degreeno, int schemetype, int semno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);

                        SqlParameter[] objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_COLLEGEID", collegeno);
                        objParams[1] = new SqlParameter("@P_BRANCHNO", branchno);
                        objParams[2] = new SqlParameter("@P_ADMBATCH", admbatch);
                        objParams[3] = new SqlParameter("@P_DEGREENO", degreeno);
                        objParams[4] = new SqlParameter("@P_SCHEMETYPE", schemetype);
                        objParams[5] = new SqlParameter("@P_SEM", semno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STUDENT_SP_RET_STUDENT_BY_SCHEME_ALLOT", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentsForScheme-> " + ex.ToString());
                    }

                    return ds;
                }

                public DataSet GetStudentNameAndId()
                {
                    DataSet dsCT = null;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[0];

                        dsCT = objSQLHelper.ExecuteDataSetSP("PKG_STUDENT_DROPDOWN", objParams);
                    }
                    catch (Exception ex)
                    {
                        return dsCT;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.studentcontroller.GetStudentNameAndId-> " + ex.ToString());
                    }

                    return dsCT;
                }

                public int UpdateStudentSeatNo(string studids, string rollnos, int sessionno)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_STUDID", studids);
                        objParams[1] = new SqlParameter("@P_ROLLNOS", rollnos);
                        objParams[2] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[3] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[3].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_STUDENT_SP_UPD_ROLL_SEAT_ALLOTMENT", objParams, false);
                        if (ret != null)
                            if (ret.ToString() != "-99")
                                retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.UpdateStudentSection-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int bulkUpdSVETStudentAdmStatus(Student objStudent, int sessionNo, string remark)
                {
                    string Message = string.Empty;
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_IDNO", objStudent.IdNo);
                        objParams[1] = new SqlParameter("@P_SEMESTERNO", objStudent.SemesterNo);
                        objParams[2] = new SqlParameter("@P_UA_NO", objStudent.Uano);
                        objParams[3] = new SqlParameter("@P_SESSIONNO", sessionNo);
                        objParams[4] = new SqlParameter("@P_REMARK", remark);
                        objParams[5] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[5].Direction = ParameterDirection.Output;

                        object status = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_UPD_ADMISSION_STATUS", objParams, true);
                        if (status != null)
                        {
                            if (status.ToString().Equals("-99"))
                                Message = "Transaction Failed!";
                            else
                                status = Convert.ToInt32(CustomStatus.RecordUpdated);
                        }
                        return Convert.ToInt32(status);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.UpdateStudent-> " + ex.ToString());
                    }
                }

                public DataSet GetCancelCourseExcel(int sessionno, int college_id)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);

                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_COLLEGE_ID", college_id);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_COURSE_CANCELATION_EXCEL", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentsForScheme-> " + ex.ToString());
                    }

                    return ds;
                }

                public int GetStudentIDByRegNo(string regno)
                {
                    int idno = 0;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_REGNO", regno);

                        idno = Convert.ToInt32(objSQLHelper.ExecuteScalarSP("PKG_STUDENT_SP_RET_STUDID_BY_REGNO", objParams));
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.studentcontroller.GetStudentIDByRegNo-> " + ex.ToString());
                    }

                    return idno;
                }

                public DataSet GetStudentDetailsExamNew(int idno, int sessionno)
                {
                    DataSet ds = null;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        objParams[1] = new SqlParameter("@P_SESSIONNO", sessionno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STUDENT_SP_RET_STUDENT_BYID_FOR_EXAM", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentDetailsExamNew->" + ex.ToString());
                    }
                    return ds;
                }

                public int ChangeStudentSemester(Student objStudent)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);

                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_IDNO", objStudent.IdNo);
                        objParams[1] = new SqlParameter("@P_SEMNO", objStudent.SemesterNo);
                        objParams[2] = new SqlParameter("@P_YEAR", objStudent.Year);

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_STUDENT_SP_CHANGE_SEM", objParams, false);
                        if (ret != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.ChangeStudentSemester-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetStudentListForIdentityCard(int degreeno, int branchno, int semesterno)
                {
                    DataSet ds = null;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_DEGREENO", degreeno);
                        objParams[1] = new SqlParameter("@P_BRANCHNO", branchno);
                        objParams[2] = new SqlParameter("@P_SEMESTERNO", semesterno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STUDENT_SEARCH_IDENTITY_CARD", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.EmpCreateController.GetStudentListForIdentityCard-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetStudentSubjectsOffered(int sessionno, int schemeno, int semesterno, int sectionno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);

                        SqlParameter[] objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_SCHEMENO", schemeno);
                        objParams[2] = new SqlParameter("@P_SEMESTERNO", semesterno);
                        objParams[3] = new SqlParameter("@P_SECTIONNO", sectionno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PREREGIST_SP_RET_ALL_STUDENTS_BYCOE", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentSubjectsOffered-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetStudentDegreeFill()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[0];

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_SP_ALL_DEGREE", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentDegreeFill->" + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetMaleFemaleTotalOnDegree(string degreeno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);

                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_DEGREENO", degreeno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_REPORT_STU_STRENGTH_MF", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetMaleFemaleTotalOnDegree-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetStudentsByScheme(int schemeno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);

                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_SCHEMENO", schemeno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STUDENT_SP_RET_STUD_BY_SCHEME", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentsByScheme-> " + ex.ToString());
                    }
                    return ds;
                }

                // add method 21/08/2009
                public DataSet GetStudentSearchForHostelBonafideCert(int hostelSessionNo, int admBatchNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSqlHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_HOSTEL_SESSION_NO", hostelSessionNo);
                        objParams[1] = new SqlParameter("@P_HOSTEL_NO", admBatchNo);
                        ds = objSqlHelper.ExecuteDataSetSP("PKG_HOSTEL_STUDENT_SEARCH_BONAFIDE_CERTIFICATE", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentSearchForHostelBonafideCert-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetStudentSearchForHostelIdentityCard(int hostelSessionNo, int hostelNo, int blockNo, int floorNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSqlHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_HOSTEL_SESSION_NO", hostelSessionNo);
                        objParams[1] = new SqlParameter("@P_HOSTEL_NO", hostelNo);
                        objParams[2] = new SqlParameter("@P_BLOCK_NO", blockNo);
                        objParams[3] = new SqlParameter("@P_FLOOR_NO", floorNo);
                        ds = objSqlHelper.ExecuteDataSetSP("PKG_HOSTEL_STUDENT_SEARCH_IDENTITY_CARD", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetSTudentSearchForHostelIdentityCard-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetStudentsRegNo(int branchno, int admbatch, int idtype)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);

                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_BRANCHNO", branchno);
                        objParams[1] = new SqlParameter("@P_ADMBATCH", admbatch);
                        objParams[2] = new SqlParameter("@P_IDTYPE", idtype);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_STU_UPDATION_REGNO", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentsRegNo-> " + ex.ToString());
                    }

                    return ds;
                }

                public DataSet GetStudentsForBatchAllotment(int sessionNo, int collegeNo, int degreeNo, int branchNo, int schemeNo, int semesterNo, int courseNo, int sectionNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);

                        SqlParameter[] objParams = new SqlParameter[08];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionNo);
                        objParams[1] = new SqlParameter("@P_COLLEGEID", collegeNo);
                        objParams[2] = new SqlParameter("@P_DEGREENO", degreeNo);
                        objParams[3] = new SqlParameter("@P_BRANCHNO", branchNo);
                        objParams[4] = new SqlParameter("@P_SCHEMENO", schemeNo);
                        objParams[5] = new SqlParameter("@P_SEMESTERNO", semesterNo);
                        objParams[6] = new SqlParameter("@P_COURSENO", courseNo);
                        objParams[7] = new SqlParameter("@P_SECTIONNO", sectionNo);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STUDENT_SP_RET_BATCH_ALLOTMENT", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentsForTeacherAllotment-> " + ex.ToString());
                    }

                    return ds;
                }

                public DataSet GetStudentsByAdmBatch(int admbatch, int degree, int branch, int sem)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);

                        SqlParameter[] objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_ADMBATCH", admbatch);
                        objParams[1] = new SqlParameter("@P_DEGREEno", degree);
                        objParams[2] = new SqlParameter("@P_BRANCHno", branch);
                        objParams[3] = new SqlParameter("@P_SEM", sem);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STUD_SP_RET_STUDENTS_BY_ADMBATCH", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentsByAdmBatch-> " + ex.ToString());
                    }
                    return ds;
                }

                public int UpdateStudent_BatchAllot(Student_Acd objStudent)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[10];
                        objParams[0] = new SqlParameter("@P_STUDID", objStudent.StudId);
                        objParams[1] = new SqlParameter("@P_SESSIONNO", objStudent.SessionNo);
                        objParams[2] = new SqlParameter("@P_SCHEMENO", objStudent.SchemeNo);
                        objParams[3] = new SqlParameter("@P_SEMESTERNO", objStudent.SemesterNo);
                        objParams[4] = new SqlParameter("@P_COURSENO", objStudent.CourseNo);
                        objParams[5] = new SqlParameter("@P_SECTIONNO", objStudent.Sectionno);
                        objParams[6] = new SqlParameter("@P_SUBID", objStudent.ThBatchNo);
                        objParams[7] = new SqlParameter("@P_BATCHNO", objStudent.BatchNo);
                        objParams[8] = new SqlParameter("@P_ATTTYPE", objStudent.Pract_Theory);
                        objParams[9] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[9].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_STUDENT_SP_UPD_BATCH_ALLOTMENT", objParams, false);
                        if (ret != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.UpdateStudent_BatchAllot-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int UpdateStudentRegNumber(string regno, string enrollnos2)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_IDNOS", regno);
                        objParams[1] = new SqlParameter("@P_REGNO", enrollnos2);
                        // objParams[2] = new SqlParameter("@P_ROLLNOS", rollnos);
                        objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[2].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_STUDENT_SP_UPD_REGNO1", objParams, false);
                        if (ret != null)
                            if (ret.ToString() != "-99")
                                retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.UpdateStudentSection-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int UpdateStudentRegNo(string regno, string enrollnos)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_IDNOS", regno);
                        objParams[1] = new SqlParameter("@P_ENROLLNOS", enrollnos);
                        objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[2].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_STUDENT_SP_UPD_REGNO", objParams, false);
                        if (ret != null)
                            if (ret.ToString() != "-99")
                                retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.UpdateStudentSection-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetRegisterdCourses(int idno, int sessionno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        objParams[1] = new SqlParameter("@P_SESSIONNO", sessionno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STUDENT_SP_RET_COURSE_REGISTERED", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentRegisterdCourses->" + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetExemptedCourses(int idno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STUD_SP_RET_EXEM_COURSES", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetExemptedCourses->" + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetFeesInfo(int studentId)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objDataAccess = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] sqlParams = new SqlParameter[]
                        {
                            new SqlParameter("@P_IDNO", studentId)
                        };
                        ds = objDataAccess.ExecuteDataSetSP("PKG_STUD_SP_RER_PAID_FEES", sqlParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.StudentController.GetFeesInfo() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }

                public DataSet GetStudentExamSubjectsRegistered(int sessionno, int schemeno, int semesterno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);

                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_SCHEMENO", schemeno);
                        objParams[2] = new SqlParameter("@P_SEMESTERNO", semesterno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STUDENT_GET_EXAM_REGISTERED", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentSubjectsOffered-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetStudentSearchForHostelBonafideCert(int hostelSessionNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSqlHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_HOSTEL_SESSION_NO", hostelSessionNo);
                        ds = objSqlHelper.ExecuteDataSetSP("PKG_HOSTEL_STUDENT_SEARCH_BONAFIDE_CERTIFICATE", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentSearchForHostelBonafideCert-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet RetrieveStudentInformation(string search, string category)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_SEARCHSTRING", search);
                        objParams[1] = new SqlParameter("@P_SEARCH", category);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_SP_STUDENT_INFORMATION_SEARCH", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.EmpCreateController.RetrieveEmpDetails-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetStudentBatchAllotment(int sessionNo, int schemeNo, int courseNo, int subId, int batchNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSqlHlper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionNo);
                        objParams[1] = new SqlParameter("@P_SCHEMENO", schemeNo);
                        objParams[2] = new SqlParameter("@P_COURSENO", courseNo);
                        objParams[3] = new SqlParameter("@P_SUBID", subId);
                        objParams[4] = new SqlParameter("@P_BATCHNO", batchNo);
                        ds = objSqlHlper.ExecuteDataSetSP("PKG_REPORT_STUDENT_BATCH_ALLOTMENT", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentBatchAllotment-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet RetrieveStudentFeedbackInformation(int tokenNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_TOKENNO", tokenNo);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_REPORT_STUDENT_FEEDBACK", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.RetrieveStudentFeedbackInformation-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetStudentsForBulkHallTicketPrint(int session, int scheme, int semesterNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);

                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_SESSION", session);
                        objParams[1] = new SqlParameter("@P_SCHEMENO", scheme);
                        objParams[2] = new SqlParameter("@P_SEMESTERNO", semesterNo);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STUDENT_SP_RET_BULK_HALL_TICKET_PRINT", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentsForBatchAllotment-> " + ex.ToString());
                    }

                    return ds;
                }

                public int UpdateClassAward(int courseNo)
                {
                    Student objStudent = new Student();
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);

                        //Update Class Award
                        SqlParameter[] objParams = new SqlParameter[1];

                        //First Add Student Parameter
                        objParams[0] = new SqlParameter("@P_COURSENO", courseNo);

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_STUDENT_UPD_CLASS_AWARD", objParams, false);
                        if (ret != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        return retStatus;
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.UpdateClassAward->" + ex.ToString());
                    }
                }

                public DataSet RetrieveStudentCourseDetailForClassAward(int schemeNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_SCHEMENO", schemeNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_STUDENT_CLASS_AWARD", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.RetrieveStudentCourseDetailForClassAward->" + ex.ToString());
                    }
                    return ds;
                }

                public int AddExemSubject(int idno, string regno, int schemeno, int courseno, string ccode, int credit, int sessionno)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[]
                        {
                            new SqlParameter("@P_IDNO", idno),
                            new SqlParameter("@P_REGNO", regno),
                            new SqlParameter("@P_SCHEMENO", schemeno),
                            new SqlParameter("@P_COURSENO", courseno),
                            new SqlParameter("@P_CRS_CD", ccode),
                            new SqlParameter("@P_CREDIT", credit),
                            new SqlParameter("@P_SESSIONNO",sessionno)
                        };

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_STUDENT_SP_INS_EXEM_SUBJECTS", objParams, false);
                        if (ret != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.UpdateLastQualExams --> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetStudentsForUpdateBulkPhotoUpload(int admbatch, int college_id, int degreeno, int branchNo, int semesterno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);

                        SqlParameter[] objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_ADMBATCH", admbatch);
                        objParams[1] = new SqlParameter("@P_COLLEGE_ID", college_id);
                        objParams[2] = new SqlParameter("@P_DEGREENO", degreeno);
                        objParams[3] = new SqlParameter("@P_BRANCHNO", branchNo);
                        objParams[4] = new SqlParameter("@P_SEMESTERNO", semesterno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_SHOW_PHOTO", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentsForUpdateBulkPhotoUpload-> " + ex.ToString());
                    }

                    return ds;
                }

                public int UpdateStudentPhoto(int idno, byte[] photo)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        if (!(photo == null))
                        {
                            SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                            SqlParameter[] objParams = new SqlParameter[2];
                            objParams[0] = new SqlParameter("@P_IDNO", idno);
                            //objParams[1] = new SqlParameter("@P_PHOTO",DBNull.Value);
                            //else
                            objParams[1] = new SqlParameter("@P_PHOTO", photo);

                            if (objSQLHelper.ExecuteNonQuerySP("PKG_ACD_UPDATE_PHOTO", objParams, false) != null)
                                retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        }
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.UpdateStudentPhoto->" + ex.ToString());
                    }

                    return retStatus;
                }

                public DataSet GetStudentBlankMarksheet(string sessionNo, string deptNo, string semesterNo, string controllsheetNo, string examNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);

                        SqlParameter[] objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionNo);
                        objParams[1] = new SqlParameter("@P_DEPTNO", deptNo);
                        objParams[2] = new SqlParameter("@P_SEMESTER_NO", semesterNo);
                        objParams[3] = new SqlParameter("@P_CONTROLSHEET_NO", controllsheetNo);
                        objParams[4] = new SqlParameter("@P_EXAMNO", examNo);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_BLANKMARKSHEET_REPORT", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentSubjectsOffered-> " + ex.ToString());
                    }
                    return ds;
                }

                public int UpdateStudentSection(string studids, string rollnos, string sectionnos, int semesterno, int UANO, string sectionname, string ipaddress)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[8];
                        objParams[0] = new SqlParameter("@P_STUDID", studids);
                        objParams[1] = new SqlParameter("@P_SECTIONNO", sectionnos);
                        objParams[2] = new SqlParameter("@P_ROLLNOS", rollnos);
                        objParams[3] = new SqlParameter("@P_SEMESTERNO", semesterno);
                        objParams[4] = new SqlParameter("@P_UA_NO", UANO);
                        objParams[5] = new SqlParameter("@P_SECTION_NAME", sectionname);
                        objParams[6] = new SqlParameter("@P_IPADDRESS", ipaddress);

                        objParams[7] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[7].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_STUDENT_SP_UPD_SECTION_ALLOTMENT1", objParams, false);
                        if (ret != null)
                            if (ret.ToString() != "-99")
                                retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.UpdateStudentSection-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetAcademicBatchWiseStudents(int acdbatchNo, int branchNo, int semesterNo)
                {
                    DataSet ds = null;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_ACDBATCHNO", acdbatchNo);
                        objParams[1] = new SqlParameter("@P_BRANCHNO", branchNo);
                        objParams[2] = new SqlParameter("@P_SEMESTERNO", semesterNo);

                        ds = objSQLHelper.ExecuteDataSetSP("ACD_ACDBATCH_BRANCH_SEMESTER_STUDENTS", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.studentcontroller.GetAllQualifyExamDetails-> " + ex.ToString());
                    }

                    return ds;
                }

                public DataSet GetExamSeatNumbers(int sessionNo, int branchNo, int semesterNo, int prev_status)
                {
                    DataSet ds = null;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionNo);
                        objParams[1] = new SqlParameter("@P_BRANCHNO", branchNo);
                        objParams[2] = new SqlParameter("@P_SEMNO", semesterNo);
                        objParams[3] = new SqlParameter("@P_PREV_STATUS", prev_status);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_EXAM_SEAT_NUMBERS", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.studentcontroller.GetAllQualifyExamDetails-> " + ex.ToString());
                    }

                    return ds;
                }

                public int GenerateExamSeatNumber(int sessionNo, int branchNo, int semesterNo, int prev_status)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionNo);
                        objParams[1] = new SqlParameter("@P_BRANCHNO", branchNo);
                        objParams[2] = new SqlParameter("@P_SEMNO", semesterNo);
                        objParams[3] = new SqlParameter("@P_PREV_STATUS", prev_status);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_EXAM_SEATNO_ALLOTMENT", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.UpdateStudentPhoto->" + ex.ToString());
                    }

                    return retStatus;
                }

                public DataSet GetCoursewiseStudentsCount(int sessionNo, int schemeNo, int CourseNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);

                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionNo);
                        objParams[1] = new SqlParameter("@P_SCHEMENO", schemeNo);
                        objParams[2] = new SqlParameter("@P_COURSENO", CourseNo);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_COURSEWISE_STUDENT_COUNT", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetCoursewiseStudentsCount-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetStudentsForRandomNo(int sessionNo, int college_id, int degreeno, int branchno, int schemeno, int semesterno, int CourseNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);

                        SqlParameter[] objParams = new SqlParameter[7];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionNo);
                        objParams[1] = new SqlParameter("@P_COLLEGE_ID", college_id);
                        objParams[2] = new SqlParameter("@P_DEGREENO", degreeno);
                        objParams[3] = new SqlParameter("@P_BRANCHNO", branchno);
                        objParams[4] = new SqlParameter("@P_SCHEMENO", schemeno);
                        objParams[5] = new SqlParameter("@P_SEMESTERNO", semesterno);
                        objParams[6] = new SqlParameter("@P_COURSENO", CourseNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_STUDENTS_RANDOM_GENERATION", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetCoursewiseStudentsCount-> " + ex.ToString());
                    }
                    return ds;
                }

                // student decoding number genereation

                public int GenerateDecodeNumber(int sessionNo, int branchNo, int DigitsNo)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionNo);
                        objParams[1] = new SqlParameter("@P_BRANCHNO", branchNo);
                        objParams[2] = new SqlParameter("@P_DIGIT", DigitsNo);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_DECODENO_RANDOM", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.UpdateStudentPhoto->" + ex.ToString());
                    }

                    return retStatus;
                }

                public DataSet GetDecodeNumber(int sessionNo, int branchNo, int DigitsNo)
                {
                    DataSet ds = null;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionNo);
                        objParams[1] = new SqlParameter("@P_BRANCHNO", branchNo);
                        objParams[2] = new SqlParameter("@P_DIGIT", DigitsNo);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_EXAM_SEAT_NUMBERS", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.studentcontroller.GetAllQualifyExamDetails-> " + ex.ToString());
                    }

                    return ds;
                }

                public int UpdateLockDecodeNo(int sessionNo)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionNo);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_DECODE_NUMBER_LOCK", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.UpdateLockDecodeNo->" + ex.ToString());
                    }

                    return retStatus;
                }

                public int UpdateRandomNumber(string idnos, int sessionNo, int courseno, int schemeno, int semesterno)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_IDNO", idnos);
                        objParams[1] = new SqlParameter("@P_SESSIONNO", sessionNo);
                        objParams[2] = new SqlParameter("@P_COURSENO", courseno);
                        objParams[3] = new SqlParameter("@P_SCHEMENO", schemeno);
                        objParams[4] = new SqlParameter("@P_SEMESTERNO", semesterno);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_DECODE_NUMBER_RANDOM", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.UpdateRandomNumber->" + ex.ToString());
                    }

                    return retStatus;
                }

                public DataSet ViewGenerateDecodeNumber(int sessionNo, int semesterNo, int CourseNo)
                {
                    DataSet ds = null;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionNo);
                        objParams[1] = new SqlParameter("@P_COURSENO", CourseNo);
                        objParams[2] = new SqlParameter("@P_SEMESTERNO", semesterNo);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_GET_DECODE_NUMBERS", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.studentcontroller.GetAllQualifyExamDetails-> " + ex.ToString());
                    }

                    return ds;
                }

                // student decoding number genereation
                public int GenerateDecodeNumber(int sessionNo, int branchNo, int courseNo, int DigitsNo, string ipAddress, int userId, string collegeCode)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[7];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionNo);
                        objParams[1] = new SqlParameter("@P_BRANCHNO", branchNo);
                        objParams[2] = new SqlParameter("@P_COURSENO", courseNo);
                        objParams[3] = new SqlParameter("@P_DIGIT", DigitsNo);
                        objParams[4] = new SqlParameter("@P_IP_ADDRESS", ipAddress);
                        objParams[5] = new SqlParameter("@P_USER_ID", userId);
                        objParams[6] = new SqlParameter("@P_COLLEGE_CODE", collegeCode);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_DECODENO_RANDOM", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.UpdateStudentPhoto->" + ex.ToString());
                    }

                    return retStatus;
                }

                // Provision
                public DataSet GetProvisionalAdmission(int admBatchNo, int branchNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);

                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_ADMBATCH", admBatchNo);
                        objParams[1] = new SqlParameter("@P_BRANCHNO", branchNo);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_GET_PROVISION_ADMISSION_LIST", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetCoursewiseStudentsCount-> " + ex.ToString());
                    }
                    return ds;
                }

                public int AddStudentDocuments(int idno, int docno, string col_code)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        objParams[1] = new SqlParameter("@P_DOCUMENTNO", docno);
                        objParams[2] = new SqlParameter("@P_COLLEGE_CODE", col_code);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_SP_ADD_STUD_DOC_LIST", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.AddStudentDocuments->" + ex.ToString());
                    }

                    return retStatus;
                }

                public int UpdateStudentBatch(string studids, int batchno, int sessionno, int degreeno, int semesterno)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_STUDID", studids);
                        objParams[1] = new SqlParameter("@P_BATCHNO", batchno);
                        objParams[2] = new SqlParameter("@P_DEGREENO", degreeno);
                        objParams[3] = new SqlParameter("@P_SEMESTERNO", semesterno);
                        objParams[4] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[5] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[5].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_STUDENT_SP_BATCH_ALLOTMENT", objParams, false);
                        if (ret != null)
                            if (ret.ToString() != "-99")
                                retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.UpdateStudentSection-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet Getstudent(long idno, int sessionno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);

                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        objParams[1] = new SqlParameter("@P_SESSIONNO", sessionno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACADEMIC_GETSTUDENT", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetDetainedList-> " + ex.ToString());
                    }
                    return ds;
                }

                public int UpDateStudentStatus(int IDNO, int SESSIONNO)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);

                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_IDNO", IDNO);
                        objParams[1] = new SqlParameter("@P_SESSIONNO", SESSIONNO);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ACADEMIC_UPDATE_STUDENT_STATUS", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.UpDateStudent-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataTableReader GetStudentDetails(int idno)
                {
                    DataTableReader dtr = null;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);

                        dtr = objSQLHelper.ExecuteDataSetSP("PKG_STUDENT_SP_RET_STUDENT_BYID", objParams).Tables[0].CreateDataReader();
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentDetails->" + ex.ToString());
                    }
                    return dtr;
                }

                public int PromotStudentSemRtm(Student objStudent, int sessionNo, string enrollno, string ipadd, string recNo, string recDate, string recAmount, int promotion)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        //Update Student semester Rtm
                        objParams = new SqlParameter[15];
                        objParams[0] = new SqlParameter("@P_IDNO", objStudent.IdNo);
                        objParams[1] = new SqlParameter("@P_ENROLLMENTNO", enrollno);
                        objParams[2] = new SqlParameter("@P_SECTIONNO", objStudent.SectionNo);
                        objParams[3] = new SqlParameter("@P_SEMESTERNO", objStudent.SemesterNo);
                        objParams[4] = new SqlParameter("@P_ROLLNO", objStudent.RollNo);
                        objParams[5] = new SqlParameter("@P_RECEIPTNO", recNo);
                        objParams[6] = new SqlParameter("@P_RECEIPT_DATE", recDate);
                        objParams[7] = new SqlParameter("@P_RECEIPT_AMOUNT", recAmount);
                        objParams[8] = new SqlParameter("@P_UA_NO", objStudent.Uano);
                        objParams[9] = new SqlParameter("@P_IPADDRESS", ipadd);
                        objParams[10] = new SqlParameter("@P_CREATE_DATE", objStudent.Dob);
                        objParams[11] = new SqlParameter("@P_COLLEGE_CODE", objStudent.CollegeCode);
                        objParams[12] = new SqlParameter("@P_BRANCHNO", objStudent.BranchNo);
                        objParams[13] = new SqlParameter("@P_SESSIONNO", sessionNo);
                        objParams[14] = new SqlParameter("@P_PROMOTIONNO", promotion);
                        objParams[14].Direction = ParameterDirection.InputOutput;

                        int status = (Int32)objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_INS_SEM_PROMOTION", objParams, true);
                        if (status != -99)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.Error);
                        return retStatus;
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.UpdateStudent-> " + ex.ToString());
                    }
                }

                public int GenerateClassRollNumber(Student objStud, int rollnofrom, int rollnoto)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[7];
                        objParams[0] = new SqlParameter("@P_DEGREENO", objStud.DegreeNo);
                        objParams[1] = new SqlParameter("@P_BRANCHNO", objStud.BranchNo);
                        objParams[2] = new SqlParameter("@P_SEMESTERNO", objStud.SemesterNo);
                        objParams[3] = new SqlParameter("@P_SECTIONO", objStud.SectionNo);
                        objParams[4] = new SqlParameter("@P_MF", objStud.Sex);
                        objParams[5] = new SqlParameter("@P_ROLLNOFROM", rollnofrom);
                        objParams[6] = new SqlParameter("@P_ROLLNOTO", rollnoto);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_ROLLNO_GENERATE1", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.UpdateStudentPhoto->" + ex.ToString());
                    }

                    return retStatus;
                }

                public DataSet GetStudentListForRollNo(Student objStud)
                {
                    DataSet ds = null;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_DEGREENO", objStud.DegreeNo);
                        objParams[1] = new SqlParameter("@P_BRANCHNO", objStud.BranchNo);
                        objParams[2] = new SqlParameter("@P_SEMESTERNO", objStud.SemesterNo);
                        objParams[3] = new SqlParameter("@P_SECTIONNO", objStud.SectionNo);
                        objParams[4] = new SqlParameter("@P_SEX", objStud.Sex);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STUDENT_SEARCH_FOR_ROLLNO_GEN", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.EmpCreateController.GetStudentListForIdentityCard-> " + ex.ToString());
                    }
                    return ds;
                }

                public int AddProspectusSaleStudent(Student objStudent)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[]
                        {
                            new SqlParameter("@P_SESSIONNO", objStudent.SessionNo),
                            new SqlParameter("@P_DEGREENO", objStudent.DegreeNo),
                            new SqlParameter("@P_BRANCHNO", objStudent.BranchNo),
                            new SqlParameter("@P_ADMISSION_BATCH", objStudent.AdmBatch),
                            new SqlParameter("@P_STUDENT_NAME",objStudent.StudName),
                            new SqlParameter("@P_SALE_DATE",objStudent.SaleDate),
                            new SqlParameter("@P_PRINT_DATE", objStudent.PrintDate),
                            new SqlParameter("@P_PROS_AMT", objStudent.Amount),
                            new SqlParameter("@P_SERIAL_NO", objStudent.SerialNo),
                            new SqlParameter("@P_PROS_CANCEL", objStudent.Can),
                            new SqlParameter("@P_UA_NO", objStudent.Uano),
                            new SqlParameter("@P_IPADDRESS", objStudent.IPADDRESS),
                            new SqlParameter("@P_COLLEGE_CODE", objStudent.CollegeCode),
                            new SqlParameter("@P_REC_NO", objStudent.ReciptNo),
                            new SqlParameter("@P_OUT", SqlDbType.Int)
                         };
                        objParams[objParams.Length - 1].Direction = ParameterDirection.Output;
                        //objSQLHelper.ExecuteNonQuerySP("PKG_SALE_PROSPECTUS_INSERT", objParams, false);

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_SALE_PROSPECTUS_INSERT", objParams, true);
                        retStatus = Convert.ToInt32(ret);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.AddProspectusSaleStudent --> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetStudentsExamNo_RollNo(int admbatch, int degreeNo, int branchno, int semesterno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);

                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_ADMBATCH", admbatch);
                        objParams[1] = new SqlParameter("@P_DEGREENO", degreeNo);
                        objParams[2] = new SqlParameter("@P_BRANCHNO", branchno);
                        objParams[3] = new SqlParameter("@P_SEMESTERNO", admbatch);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_GET_STUDENT_ALLOTMENT_EXAMNO_REPORT", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentsExamNo-> " + ex.ToString());
                    }

                    return ds;
                }

                public DataSet GetStudentsRegNo_RollNo(int degreeNo, int branchno, int admbatch)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);

                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_DEGREENO", degreeNo);
                        objParams[1] = new SqlParameter("@P_BRANCHNO", branchno);
                        objParams[2] = new SqlParameter("@P_ADMBATCH", admbatch);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_GET_STUDENT_ALLOTMENT_ROLLNO_REPORT", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentsRegNo-> " + ex.ToString());
                    }

                    return ds;
                }

                public DataSet GetStudentsRegNo_RollNo_Year(int degreeNo, int branchno, int admbatch)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);

                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_DEGREENO", degreeNo);
                        objParams[1] = new SqlParameter("@P_BRANCHNO", branchno);
                        objParams[2] = new SqlParameter("@P_ADMBATCH", admbatch);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_GET_STUDENT_ALLOTMENT_ROLLNO_REPORT_YEAR", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentsRegNo-> " + ex.ToString());
                    }

                    return ds;
                }

                // student GenerateBulkRollNo number genereation

                public int GenerateBulkRollNo(int branchNo, int admBatch)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_BRANCHNO", branchNo);
                        objParams[1] = new SqlParameter("@P_ADMBATCH", admBatch);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_UPDATE_REGNO", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GenerateBulkRollNo->" + ex.ToString());
                    }

                    return retStatus;
                }

                public string GetGenRegistrationNumber(int admBatch, int branchNo)
                {
                    string generatedRegNo = string.Empty;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[3];
                        //objParams[0] = new SqlParameter("@P_STATUS", status);
                        objParams[0] = new SqlParameter("@P_ADMBATCH", admBatch);
                        objParams[1] = new SqlParameter("@P_BRANCHNO", branchNo);
                        objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[2].Direction = ParameterDirection.Output;

                        generatedRegNo = (objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_REGNO_GENERATE_NEW", objParams, true)).ToString();
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetRegistrationNumber-> " + ex.ToString());
                    }
                    return generatedRegNo;
                }

                public DataSet RetrieveStudentCurrentRegDetails(int idno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_GET_REGISTRATION_DETAILS_BY_ID", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.RetrieveStudentCurrentRegDetails-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet RetrieveStudentFeesDetails(int idno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_GET_FEES_DETAILS_BY_ID", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.RetrieveStudentFeesDetails-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet RetrieveStudentAttendanceDetails(int sessionno, int schemeno, int semesterno, int idno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_SCHEMENO", schemeno);
                        objParams[2] = new SqlParameter("@P_SEMESTERNO", semesterno);
                        objParams[3] = new SqlParameter("@P_IDNO", idno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_REPORT_STUDENT_ATTENDANCE", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.RetrieveStudentCurrentRegDetails-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet RetrieveStudentCertificateDetails(int idno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_GET_CERT_ISUUED_DETAILS_BY_ID", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.RetrieveStudentCertificateDetails-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetAllRemarkDetails(int idno)
                {
                    DataSet ds = null;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STUDENT_GET_REMARK", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.studentcontroller.GetAllQualifyExamDetails-> " + ex.ToString());
                    }

                    return ds;
                }

                public DataSet GetStudentRefunds(int idno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_STUDENT_REFUND_AMOUNT", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentRefunds-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetCoursewiseStudentsCount(int sessionNo, int schemeNo, int CourseNo, int semesterNo, int schemetype, int sectionNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);

                        SqlParameter[] objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionNo);
                        objParams[1] = new SqlParameter("@P_SCHEMENO", schemeNo);
                        objParams[2] = new SqlParameter("@P_COURSENO", CourseNo);
                        objParams[3] = new SqlParameter("@P_SEMESTERNO", semesterNo);
                        objParams[4] = new SqlParameter("@P_SCHEMETYPE", schemetype);
                        objParams[5] = new SqlParameter("@P_SECTIONNO", sectionNo);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_COURSEWISE_STUDENT_COUNT_FOR_REPORT", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentController.GetCoursewiseStudentsCount-> " + ex.ToString());
                    }
                    return ds;
                }

                public string AddStudentTempData(Student objStudent, StudentAddress objStudAddress, int hostel)
                {
                    string retStatus = string.Empty;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        //Add New student
                        objParams = new SqlParameter[43];
                        objParams[0] = new SqlParameter("@P_NAME", objStudent.StudName);
                        objParams[1] = new SqlParameter("@P_GATE_YEAR", objStudent.GATE_YEAR);
                        objParams[2] = new SqlParameter("@P_GATE_REG", objStudent.GATE_REG);
                        objParams[3] = new SqlParameter("@P_GATE_SCORE", objStudent.GATE_SCORE);
                        objParams[4] = new SqlParameter("@P_GATE_PAPER", objStudent.GATE_PAPER);
                        objParams[5] = new SqlParameter("@P_DOB", objStudent.Dob);
                        objParams[6] = new SqlParameter("@P_GENDER", objStudent.Sex);
                        objParams[7] = new SqlParameter("@P_MOBILE", objStudent.StudentMobile);
                        objParams[8] = new SqlParameter("@P_APPLICANT_CATEGORYNO", objStudent.CategoryNo);
                        objParams[9] = new SqlParameter("@P_ALLOTTED_CATEGORYNO", objStudent.AdmCategoryNo);
                        objParams[10] = new SqlParameter("@P_DEGREENO", objStudent.DegreeNo);
                        objParams[11] = new SqlParameter("@P_BRANCHNO", objStudent.BranchNo);
                        objParams[12] = new SqlParameter("@P_ROLL_NO", objStudent.RollNo);
                        objParams[13] = new SqlParameter("@P_AIR_OVERALL", objStudent.ALLINDIARANK);
                        objParams[14] = new SqlParameter("@P_YEAR_OF_EXAM", objStudent.YearOfExam);
                        objParams[15] = new SqlParameter("@P_QUOTA", objStudent.ADMQUOTANO);
                        objParams[16] = new SqlParameter("@P_PH", objStudent.PH);
                        objParams[17] = new SqlParameter("@P_FATHERS_NAME", objStudent.FatherName);
                        objParams[18] = new SqlParameter("@P_MOTHERS_NAME", objStudent.MotherName);
                        objParams[19] = new SqlParameter("@P_RELIGIONNO", objStudent.ReligionNo);
                        objParams[20] = new SqlParameter("@P_MARITAL_STATUS", objStudent.Married);
                        objParams[21] = new SqlParameter("@P_NATIONALITYNO", objStudent.NationalityNo);
                        objParams[22] = new SqlParameter("@P_EMAIL", objStudAddress.PEMAIL);
                        objParams[23] = new SqlParameter("@P_BLOOD_GROUPNO", objStudent.BloodGroupNo);
                        objParams[24] = new SqlParameter("@P_PERMANANT_ADDRESS", objStudent.PAddress);
                        objParams[25] = new SqlParameter("@P_CITY", objStudent.PCity);
                        objParams[26] = new SqlParameter("@P_STATENO", objStudent.StateNo);
                        objParams[27] = new SqlParameter("@P_PIN_CODE", objStudent.PPinCode);
                        objParams[28] = new SqlParameter("@P_CONTACT_NUMBER", objStudent.PMobile);
                        objParams[29] = new SqlParameter("@P_POSTAL_ADDRESS", objStudAddress.LADDRESS);
                        objParams[30] = new SqlParameter("@P_LOCAL_CITY", objStudAddress.LCITY);
                        objParams[31] = new SqlParameter("@P_LOCAL_STATE", objStudAddress.LSTATE);
                        objParams[32] = new SqlParameter("@P_GUARDIAN_PHONE", objStudAddress.LMOBILE);
                        objParams[33] = new SqlParameter("@P_PARENT_PHONE", objStudent.PMobile);
                        objParams[34] = new SqlParameter("@P_PARENT_EMAIL", objStudAddress.GEMAIL);
                        objParams[35] = new SqlParameter("@P_ADMBACTH", objStudent.AdmBatch);
                        objParams[36] = new SqlParameter("@P_DOCUMENTS", objStudent.DOCUMENTS);
                        objParams[37] = new SqlParameter("@P_PAYTYPENO", objStudent.PayTypeNO);
                        objParams[38] = new SqlParameter("@P_REMARK", objStudent.Remark);
                        objParams[39] = new SqlParameter("@P_HOSTEL", hostel);
                        objParams[40] = new SqlParameter("@P_SCORE", objStudent.Score);
                        objParams[41] = new SqlParameter("@P_CSAB_AMT", objStudent.CsabAmount);
                        objParams[42] = new SqlParameter("@P_OUT", SqlDbType.NVarChar, 40);
                        objParams[42].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_STUDENT_DATA_ENTRY", objParams, true);

                        //if (ret.ToString().Equals("-99"))
                        //    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        //else
                        //    retStatus = Convert.ToInt32(ret);
                        retStatus = ret.ToString();
                    }
                    catch (Exception ex)
                    {
                        retStatus = "0";
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.AddStudentTempData-> " + ex.ToString());
                    }

                    return retStatus;
                }

                public DataTableReader GetStudentDetailsForCheck(int idno)
                {
                    DataTableReader dtr = null;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);

                        dtr = objSQLHelper.ExecuteDataSetSP("PKG_STUDENT_SP_RET_STUDENT_BYID_FOR_CHECK", objParams).Tables[0].CreateDataReader();
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentDetails->" + ex.ToString());
                    }
                    return dtr;
                }

                public DataSet GetStudentsDocuments(int idno, string documentno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        objParams[1] = new SqlParameter("@P_DOCUMNETNO", documentno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_STUDENT_DOCUMENTS_LIST", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.RetrieveStudentFeesDetails-> " + ex.ToString());
                    }
                    return ds;
                }

                //added by sandeep on 10-02-2018
                public DataSet RetrieveStudentCurrentResultFORGRADE(int idno, int SemesterNo, int SessionNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        objParams[1] = new SqlParameter("@P_SEMESTERNO", SemesterNo);
                        objParams[2] = new SqlParameter("@P_SESSIONNO", SessionNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_STUD_CURRENT_RESULT_FOR_GRADE", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.RetrieveStudentCurrentRegDetails-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet RetrieveStudentSemesterNumberResult(int idno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_STUD_CURRENT_SEMESTER__RESULT", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.RetrieveStudentCurrentRegDetails-> " + ex.ToString());
                    }
                    return ds;
                }

                //end
                public DataSet RetrieveStudentClassTestMarks(int idno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_GET_MARKS_DETAILS_BY_ID", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.RetrieveStudentCurrentRegDetails-> " + ex.ToString());
                    }
                    return ds;
                }

                public int UpdateStudSign(Student objstud)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        //UpdateFaculty Reference
                        objParams = new SqlParameter[2];

                        objParams[0] = new SqlParameter("@P_IDNO", objstud.IdNo);
                        objParams[1] = new SqlParameter("@P_STUD_SIGN", objstud.StudSign);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_SP_UPD_STUDENT_SIGN", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.UpdateStudentSign-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int UpdateStudPhoto(Student objstud)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        //UpdateFaculty Reference
                        objParams = new SqlParameter[2];

                        objParams[0] = new SqlParameter("@P_IDNO", objstud.IdNo);
                        objParams[1] = new SqlParameter("@P_STUD_PHOTO", objstud.StudPhoto);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_SP_UPD_STUDENT_PHOTO", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.UpdateStudentPhoto-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetStudentDetailsExam(int idno)
                {
                    DataSet ds = null;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STUDENT_SP_RET_STUDENT_BYID_FOR_EXAM", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentDetails->" + ex.ToString());
                    }
                    return ds;
                }

                public DataSet RetrieveStudentCurrentRegDetails(int sessionno, int idno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_IDNO", idno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_GET_EXAM_DETAILS_BY_ID", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.RetrieveStudentCurrentRegDetails-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetStudentFailExamSubjects(int idno, int sessionno, int semesterno)
                {
                    DataSet ds = null;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        objParams[1] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[2] = new SqlParameter("@P_SEMESTERNO", semesterno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_GET_FAIL_STUDENT_LIST", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentDetails->" + ex.ToString());
                    }
                    return ds;
                }

                // methohd user to show appered subject for current term
                public DataSet GetStudentFailExamSubjects_For_Revalution(int idno, int sessionno, int semesterno)
                {
                    DataSet ds = null;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        objParams[1] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[2] = new SqlParameter("@P_SEMESTERNO", semesterno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_GET_FAIL_STUDENT_LIST_FOR_REVALUATION", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentDetails->" + ex.ToString());
                    }
                    return ds;
                }

                public int UpdateStudentCategory(string studids, string categorys)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_STUDID", studids);
                        objParams[1] = new SqlParameter("@P_CATEGORYNO", categorys);
                        objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[2].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_STUDENT_SP_UPD_CATEGORY_ALLOTMENT", objParams, false);
                        if (ret != null)
                            if (ret.ToString() != "-99")
                                retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.UpdateStudentCategory-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet RetrieveStudentrRegisteredBackExamList(int sessionno, int idno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_IDNO", idno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_REGIST_SP_REPORT_EXAM_REGISTERED_BACK_SUBJECTS", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.RetrieveStudentCurrentRegDetails-> " + ex.ToString());
                    }
                    return ds;
                }

                public int UpdateStudentExamRegister(int idno, int sessionno, string dcrno)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        //UpdateFaculty Reference
                        objParams = new SqlParameter[3];

                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        objParams[1] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[2] = new SqlParameter("@P_DCRNOS", dcrno);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_REGIST_SP_UPDATE_EXAM_REGISTRATION", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.UpdateRegisteredCourse-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int UpdateRegisteredCourse(int idno, int sessionno, int courseno, int semesterno)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        //UpdateFaculty Reference
                        objParams = new SqlParameter[4];

                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        objParams[1] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[2] = new SqlParameter("@P_COURSENO", courseno);
                        objParams[3] = new SqlParameter("@P_SEMESTERNO", semesterno);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ACD_UPDATE_EXAM_REGISTERED_COURSES", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.UpdateRegisteredCourse-> " + ex.ToString());
                    }
                    return retStatus;
                }

                //METHOD FOR DETAINING THE STUDENTS
                public int UpdateDetention(int sessionNo, string idNos, int SCHEMENO, int semesterNo, string provDetentions, string finalDetentions, string collageCode, int DETAINED_BY, string REMARKS, int UA_NO, string STATUS)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[11];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionNo);
                        objParams[1] = new SqlParameter("@P_IDNO", idNos);
                        objParams[2] = new SqlParameter("@P_SCHEMENO", SCHEMENO);
                        objParams[3] = new SqlParameter("@P_SEMESTERNO", semesterNo);
                        objParams[4] = new SqlParameter("@P_PROV_DETAIN", provDetentions);
                        objParams[5] = new SqlParameter("@P_FINAL_DETAIN", finalDetentions);
                        objParams[6] = new SqlParameter("@P_COLLEGE_CODE", collageCode);
                        objParams[7] = new SqlParameter("@P_DETAINED_BY", DETAINED_BY);
                        objParams[8] = new SqlParameter("@P_REMARKS", REMARKS);
                        objParams[9] = new SqlParameter("@P_UA_NO", UA_NO);
                        objParams[10] = new SqlParameter("@P_STATUS", STATUS);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_INS_DETENTION_NEW", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentController.updateDetention->" + ex.ToString());
                    }
                    return retStatus;
                }

                /// <summary>
                /// This controller is used to Get Detained Student List
                /// Page : DetentionAndCancelation.aspx
                /// </summary>
                /// <param name="sesionno"></param>
                /// <param name="degreeno"></param>
                /// <param name="branchno"></param>
                /// <param name="semno"></param>
                /// <param name="DetainStatus"></param>
                /// <returns></returns>

                public DataSet GetDetainedList(int sesionno, int degreeno, int branchno, int semno, string DetainStatus)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);

                        SqlParameter[] objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sesionno);
                        objParams[1] = new SqlParameter("@P_DEGREENO", degreeno);
                        objParams[2] = new SqlParameter("@P_SEMESTERNO", semno);
                        objParams[3] = new SqlParameter("@P_BRANCHNO", branchno);
                        objParams[4] = new SqlParameter("@P_DETAIN_STATUS", DetainStatus);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_STUDENT_DETENTION_LIST", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentController.GetDetainedList-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetStudentDetained(int idno, int sessionno, int semesterno)
                {
                    DataSet ds = null;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        objParams[1] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[2] = new SqlParameter("@P_SEMESTERNO", semesterno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_EXM_REGISTRATIONFORIMPROVEMENT", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentDetained->" + ex.ToString());
                    }
                    return ds;
                }

                public int DeleteCourseRegistered(int idno, int sessionno, int semesterno)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        objParams[1] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[2] = new SqlParameter("@P_SEMESTERNO", semesterno);
                        objParams[3] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[3].Direction = ParameterDirection.Output;

                        object ret = Convert.ToInt32(objSQLHelper.ExecuteNonQuerySP("PKG_ACD_DELETE_COURSE_REGISTERED", objParams, true));
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordDeleted);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.DeleteCourseRegistered-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataTableReader GetProStudentDetails(int idno)
                {
                    DataTableReader dtr = null;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);

                        dtr = objSQLHelper.ExecuteDataSetSP("PKG_STUDENT_SP_RET_PRO_STUDENT", objParams).Tables[0].CreateDataReader();
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentDetails->" + ex.ToString());
                    }
                    return dtr;
                }

                //Retrive Student Details
                public DataTableReader GetCopyCaseStudentDetails(int idno, int sessionno, int semesterno)
                {
                    DataTableReader dtr = null;
                    try
                    {
                        SQLHelper objSH = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        objParams[1] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[2] = new SqlParameter("@P_SEMESTERNO", semesterno);
                        dtr = objSH.ExecuteDataSetSP("PKG_STUDENT_GET_COPY_CASE_DETAILS", objParams).Tables[0].CreateDataReader();
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentController.GetCopyCaseStudentDetails()-> " + ex.ToString());
                    }
                    return dtr;
                }

                // Add Copy Case
                public int AddCopyCase(Student_Acd objStud)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[]
                        {
                             new SqlParameter("@P_SESSIONNO",objStud.SessionNo),
                             new SqlParameter("@P_IDNO",objStud.IdNo),
                             new SqlParameter("@P_SEATNO",objStud.Seatno),
                             new SqlParameter("@P_SEMESTERNO",objStud.SemesterNo),
                             new SqlParameter("@P_SCHEMENO",objStud.SchemeNo),
                             new SqlParameter("@P_DEGREENO",objStud.DegreeNo),
                             new SqlParameter("@P_SECTIONNO",objStud.Sectionno ),
                             new SqlParameter("@P_STATUS",objStud.Status),
                             new SqlParameter("@P_PUNISHMENT",objStud.Punishment),
                             new SqlParameter("@P_PUNISHMENTNO",objStud.PUNISHMENTNO),
                             new SqlParameter("@P_OUT",SqlDbType.Int)
                        };
                        objParams[objParams.Length - 1].Direction = ParameterDirection.Output;
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_STUDENT_INS_COPY_CASE_DETAILS", objParams, false) != null)

                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentController.AddCopyCase --> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetStudentDetailsForConsolidated(int sessionNo, int schemeNo, int semesterno, int examtype, int absorption_status, int sectionNo)
                {
                    DataSet ds = null;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionNo);
                        objParams[1] = new SqlParameter("@P_SCHEMENO", schemeNo);
                        objParams[2] = new SqlParameter("@P_SEMESTERNO", semesterno);
                        objParams[3] = new SqlParameter("@P_EXAMTYPE", examtype);
                        objParams[4] = new SqlParameter("@P_ABSORPTION_STATUS", absorption_status);
                        objParams[5] = new SqlParameter("@P_SECTIONNO", sectionNo);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_GET_GAZETE_RECORD", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentController.GetStudentDetails()-> " + ex.ToString());
                    }
                    return ds;
                }

                //*//

                //Retrive Student Details
                public DataTableReader GetWithHeldStudentDetails(int idno, int sessionno)
                {
                    DataTableReader dtr = null;
                    try
                    {
                        SQLHelper objSH = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        objParams[1] = new SqlParameter("@P_SESSIONNO", sessionno);

                        dtr = objSH.ExecuteDataSetSP("PKG_PREREG_RET_STUDDETAILS", objParams).Tables[0].CreateDataReader();
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.WithHeldEntry.GetWithHeldStudentDetails-> " + ex.ToString());
                    }
                    return dtr;
                }

                //Add WithHeld Deatils
                public int AddWithHeld(Student_Acd objStud)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[]
                        {
                             new SqlParameter("@P_SESSIONNO",objStud.SessionNo),
                             new SqlParameter("@P_IDNO",objStud.IdNo),
                             new SqlParameter("@P_SEATNO",objStud.Seatno),
                             new SqlParameter("@P_SEMESTERNO",objStud.SemesterNo),
                             new SqlParameter("@P_SCHEMENO",objStud.SchemeNo),
                             new SqlParameter("@P_REMARK ",objStud.Remark),
                             new SqlParameter("@P_STATUS ",objStud.Status),
                             new SqlParameter("@P_Wfor  ",objStud.wfor),
                             new SqlParameter("@P_OUT",SqlDbType.Int)
                        };
                        objParams[objParams.Length - 1].Direction = ParameterDirection.Output;
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_STUDENT_INS_WITHHELD_DETAILS", objParams, false) != null)

                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentController.AddWithHeld --> " + ex.ToString());
                    }
                    return retStatus;
                }

                //Update The WithHeld Entry Status
                public int UpadteWithHeldEntryStatus(Student_Acd objStud)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] sqlParams = new SqlParameter[]
                        {
                        //update

                             new SqlParameter("@P_SESSIONNO",objStud.SessionNo),
                             new SqlParameter("@P_IDNO",objStud.IdNo),
                             new SqlParameter("@P_SEATNO",objStud.Seatno),
                             new SqlParameter("@P_SEMESTERNO",objStud.SemesterNo),
                             new SqlParameter("@P_SCHEMENO",objStud.SchemeNo),
                             new SqlParameter("@P_REMARK ",objStud.Remark),
                             new SqlParameter("@P_STATUS ",objStud.Status)
                        };

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_UPD_WITHHELD_DETAILS", sqlParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessEntities.StudentController.UpadteWithHeldEntryStatus() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return retStatus;
                }

                public SqlDataReader GetWithHeldData(int idno)
                {
                    SqlDataReader dr = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        dr = objSQLHelper.ExecuteReaderSP("PKG_GETALL_WITHHELD_DETAILS", objParams);
                    }
                    catch (Exception ex)
                    {
                        return dr;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.SessionController.GetSingleSession-> " + ex.ToString());
                    }
                    return dr;
                }

                //Get WithHeld Details Info By Id
                public SqlDataReader GetWithHeldDetailsById(int idno, int sessionno)
                {
                    SqlDataReader dr = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        objParams[1] = new SqlParameter("@P_SESSIONNO", sessionno);

                        dr = objSQLHelper.ExecuteReaderSP("PKG_RET_WITHHELD_DETAILS_BYID", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessEntities.StudentController.GetWithHeldDetailsById() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return dr;
                }

                //Add Checkers
                public int AddCheckersDetail(Student objStudent)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        //Add
                        objParams = new SqlParameter[13];
                        objParams[0] = new SqlParameter("@P_CHECKERNO1", objStudent.CollatorNo1);
                        objParams[1] = new SqlParameter("@P_CHECKERNO2", objStudent.CollatorNo2);
                        objParams[2] = new SqlParameter("@P_SESSIONNO", objStudent.SessionNo);
                        objParams[3] = new SqlParameter("@P_DEGREENO", objStudent.DegreeNo);
                        objParams[4] = new SqlParameter("@P_BRANCHNO", objStudent.BranchNo);
                        objParams[5] = new SqlParameter("@P_SCHEMENO", objStudent.SchemeNo);
                        objParams[6] = new SqlParameter("@P_SEMESTERNO", objStudent.SemesterNo);
                        objParams[7] = new SqlParameter("@P_CHECKERNAME1", objStudent.CheckerName1);
                        objParams[8] = new SqlParameter("@P_CHECKERNAME2", objStudent.CheckerName2);
                        objParams[9] = new SqlParameter("@P_COLLATORNO1", objStudent.CollatorNo1);
                        objParams[10] = new SqlParameter("@P_COLLATORNO2", objStudent.CollatorNo2);
                        objParams[11] = new SqlParameter("@P_COLLEGE_CODE", objStudent.CollegeCode);
                        objParams[12] = new SqlParameter("@P_CHECKERDETAILNO", SqlDbType.Int);
                        objParams[12].Direction = ParameterDirection.Output;

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_CHECK_SP_INS_CHECKER", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.AddCheckersDetail-> " + ex.ToString());
                    }

                    return retStatus;
                }

                //Get Checkers
                public DataSet GetChekers(int sessionno, int degreeno, int branchno, int semesterno, int schemeno)
                {
                    DataSet ds = null;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_DEGREENO", degreeno);
                        objParams[2] = new SqlParameter("@P_BRANCHNO", branchno);
                        objParams[3] = new SqlParameter("@P_SEMESTERNO", semesterno);
                        objParams[4] = new SqlParameter("@P_SCHEMENO", schemeno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_GET_CHECKER_DETAILS", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetChekers-> " + ex.ToString());
                    }
                    return ds;
                }

                //Update phd student
                public string UpdatePHDStudent(Student objStudent)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    //int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        //Update Student
                        objParams = new SqlParameter[21];
                        //First Add Student Parameter
                        objParams[0] = new SqlParameter("@P_IDNO", objStudent.IdNo);
                        objParams[1] = new SqlParameter("@P_ENROLLNO", objStudent.EnrollNo);
                        objParams[2] = new SqlParameter("@P_STUDNAME", objStudent.StudName);
                        objParams[3] = new SqlParameter("@P_FATHERNAME", objStudent.FatherName);

                        objParams[4] = new SqlParameter("@P_ADMDATE", objStudent.AdmDate);
                        objParams[5] = new SqlParameter("@P_BRANCHNO", objStudent.BranchNo);
                        objParams[6] = new SqlParameter("@P_PHDSUPERVISORNO", objStudent.PhdSupervisorNo);
                        objParams[7] = new SqlParameter("@P_PHDCOSUPERVISORNO1", objStudent.PhdCoSupervisorNo1);

                        objParams[8] = new SqlParameter("@P_SUPERVISORNO", objStudent.SupervisorNo);
                        objParams[9] = new SqlParameter("@P_SUPERVISORMEMBERNO", objStudent.SupervisormemberNo);
                        objParams[10] = new SqlParameter("@P_JOINTSUPERVISORNO", objStudent.JoinsupervisorNo);
                        objParams[11] = new SqlParameter("@P_JOINSUPERVISORMEMBERNO", objStudent.JoinsupervisormemberNo);
                        objParams[12] = new SqlParameter("@P_INSTITUTEFACULTYNO", objStudent.InstitutefacultyNo);
                        objParams[13] = new SqlParameter("@P_INSTITUTEFACMEMBERNO", objStudent.InstitutefacmemberNo);
                        objParams[14] = new SqlParameter("@P_DRCNO", objStudent.DrcNo);
                        objParams[15] = new SqlParameter("@P_DRCMEMBERNO", objStudent.DrcmemberNo);
                        objParams[16] = new SqlParameter("@P_ADMBATCH", objStudent.AdmBatch);
                        objParams[17] = new SqlParameter("@P_PHDSTATUSCAT", objStudent.Phdstatuscat);
                        objParams[18] = new SqlParameter("@P_COLLEGE_CODE", objStudent.CollegeCode);
                        objParams[19] = new SqlParameter("@P_CREDITS", objStudent.Credits);
                        objParams[20] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[20].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PHD_STUD_SP_UPD_STUDENT", objParams, true);

                        if (objStudent.LastQualifiedExams != null)
                        {
                            foreach (QualifiedExam qualExam in objStudent.LastQualifiedExams)
                            {
                                this.UpdateLastQualExams(qualExam);
                            }
                        }
                        retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        return ret.ToString();
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.UpdateStudent-> " + ex.ToString());
                    }
                }

                //Get phd student Details
                public DataTableReader GetStudentPHDDetails(int idno)
                {
                    DataTableReader dtr = null;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);

                        dtr = objSQLHelper.ExecuteDataSetSP("PKG_PHD_STUDENT_SP_RET_STUDENT_BYID", objParams).Tables[0].CreateDataReader();
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentDetails->" + ex.ToString());
                    }
                    return dtr;
                }

                //Update Drc status
                public string UpdateDRCStatus(Student objStudent)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        //Update drc
                        objParams = new SqlParameter[2];

                        objParams[0] = new SqlParameter("@P_IDNO", objStudent.IdNo);
                        objParams[1] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[1].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_PHD_DRC_UPDATE", objParams, true);

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

                //show registered confirm backlog subject
                public DataSet RetrieveStudentrConfirmedBackExamList(int sessionno, int idno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_IDNO", idno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_EXAM_REGISTERED_BACK_SUBJECTS_BY_STUDID", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.RetrieveStudentCurrentRegDetails-> " + ex.ToString());
                    }
                    return ds;
                }

                //Add branch change info
                public DataSet GetStudentDetailsForPublishresult(int sessionNo, int schemeNo, int semesterno)
                {
                    DataSet ds = null;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionNo);
                        objParams[1] = new SqlParameter("@P_SCHEMENO", schemeNo);
                        objParams[2] = new SqlParameter("@P_SEMESTERNO", semesterno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_GET_STUDENT_FOR_PUBLISHRESULT", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentDetailsForPublishresult()-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetStudentApplyBranch(int idno)
                {
                    DataSet ds = null;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_BRANCHSLIDING_ELIGIBILITY_CRITERIA", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentApplyBranch->" + ex.ToString());
                    }
                    return ds;
                }

                //update confirm document
                public int AddStudentDocumentsconfirm(int idno, int docno, string col_code)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        objParams[1] = new SqlParameter("@P_DOCUMENTNO", docno);
                        objParams[2] = new SqlParameter("@P_COLLEGE_CODE", col_code);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_SP_ADD_STUD_DOC_LIST_CONFIRM", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentController.AddStudentDocuments->" + ex.ToString());
                    }

                    return retStatus;
                }

                //Delete  document
                public int DeleteStudentDocument(int idno, int docno, string col_code)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        objParams[1] = new SqlParameter("@P_DOCUMENTNO", docno);
                        objParams[2] = new SqlParameter("@P_COLLEGE_CODE", col_code);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_SP_DELETE_STUD_DOC_LIST", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentController.AddStudentDocuments->" + ex.ToString());
                    }

                    return retStatus;
                }

                public DataSet GetStudentDocListConfirm(int idno, int degreeno, int paymentType)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        objParams[1] = new SqlParameter("@P_DEGREENO", degreeno);
                        objParams[2] = new SqlParameter("@P_PTYPE", paymentType);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_STUD_DOCUMENT_LIST_CONFIRM", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentController.GetStudentDocListConfirm-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataTableReader GetRegNoForBranchChange(int degreeNo, int branchNo, int admBatch, string enrollno)
                {
                    DataTableReader dtr = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_DEGREENO", degreeNo);
                        objParams[1] = new SqlParameter("@P_BRANCHNO", branchNo);
                        objParams[2] = new SqlParameter("@P_ADMBATCH", admBatch);
                        objParams[3] = new SqlParameter("@P_ENROLLNO", enrollno);

                        dtr = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_STU_REGNO", objParams).Tables[0].CreateDataReader();
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.GetStudentDetails-> " + ex.ToString());
                    }
                    return dtr;
                }

                public DataSet GetStudentListForIssueCertBona(int idNo)
                {
                    DataSet ds = null;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STUDENT_LIST_CERT_BONAFIDE", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CertificateMasterController.GetStudentListForIssueCert-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetCancelCourseData(int idno, int sessionno, int semesterno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        objParams[1] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[2] = new SqlParameter("@P_SEMESTERNO", semesterno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GETALL_CANCEL_COURSE_DETAILS", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentController.GetCancelCourseData() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }

                public int UpdateCancelCourse(Student_Acd objStud)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[]
                        {
                             new SqlParameter("@P_SESSIONNO",objStud.SessionNo),
                             new SqlParameter("@P_IDNO",objStud.IdNo),
                             new SqlParameter("@P_ROLLNO",objStud.RollNo),
                             new SqlParameter("@P_SEMESTERNO",objStud.SemesterNo),
                             new SqlParameter("@P_SCHEMENO",objStud.SchemeNo),
                             new SqlParameter("@P_COURSENOS",objStud.Coursenos),
                             new SqlParameter("@P_UA_NO",objStud.UA_No),
                             new SqlParameter("@P_REASON",objStud.Reason),
                             new SqlParameter("@P_IPADDRESS",objStud.Ipaddress),
                             new SqlParameter("@P_OUT",SqlDbType.Int)
                        };
                        objParams[objParams.Length - 1].Direction = ParameterDirection.Output;
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_CANCEL_COURSE", objParams, false) != null)

                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentController.AddCopyCase --> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int UpdateStudentname(int idno, string name, string StudEmail, string StudMobile, string StudIndusEmail, string FMob, string FEmail, string MMob, string MEmail)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[]
                        {
                             new SqlParameter("@P_IDNO",idno),
                             new SqlParameter("@P_name",name),
                             new SqlParameter("@P_StudEmail",StudEmail),
                             new SqlParameter("@P_StudMobile",StudMobile),
                             new SqlParameter("@P_StudIndusEmail",StudIndusEmail),
                             new SqlParameter("@P_FMob",FMob),
                             new SqlParameter("@P_FEmail",FEmail),
                             new SqlParameter("@P_MMob",MMob),
                             new SqlParameter("@P_MEmail",MEmail),
                             new SqlParameter("@P_OUT",SqlDbType.Int)
                        };
                        objParams[objParams.Length - 1].Direction = ParameterDirection.Output;
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_UPDATE_NAME", objParams, false) != null)

                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentController.AddCopyCase --> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int InsertOperatorAllot(Student_Acd objStudent, int OP)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[7];

                        objParams[0] = new SqlParameter("@P_SESSIONNO", objStudent.SessionNo);
                        objParams[1] = new SqlParameter("@P_COLLEGE_ID", objStudent.COLLEGE_ID);
                        objParams[2] = new SqlParameter("@P_COURSENO", objStudent.CourseNo);
                        objParams[3] = new SqlParameter("@P_UA_NO", objStudent.UA_No);
                        objParams[4] = new SqlParameter("@P_OPERATOR", OP);
                        objParams[5] = new SqlParameter("@P_INTEXT", objStudent.INTEXT);
                        objParams[6] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[6].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_STUDENT_SP_INST_OPERATOR", objParams, false);
                        if (ret != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.UpdateStudent_TeachAllot-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int UpdateOperatorAllot(Student_Acd objStudent, int OP)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[7];

                        objParams[0] = new SqlParameter("@P_SESSIONNO", objStudent.SessionNo);
                        objParams[1] = new SqlParameter("@P_COLLEGE_ID", objStudent.COLLEGE_ID);
                        objParams[2] = new SqlParameter("@P_COURSENO", objStudent.CourseNo);
                        objParams[3] = new SqlParameter("@P_UA_NO", objStudent.UA_No);
                        objParams[4] = new SqlParameter("@P_OPERATOR", OP);
                        objParams[5] = new SqlParameter("@P_INTEXT", objStudent.INTEXT);
                        objParams[6] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[6].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_STUDENT_SP_upd_OPERATOR", objParams, false);
                        if (ret != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.UpdateStudent_TeachAllot-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int UpdateStudentSelection(int sessionno, string usernos, string selected, string SelectedMeritNo, string WaitingMeritNo)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_USERNOS", usernos);
                        objParams[2] = new SqlParameter("@P_SELECTED", selected);
                        objParams[3] = new SqlParameter("@P_SELECTEDMERITNO", SelectedMeritNo);
                        objParams[4] = new SqlParameter("@P_WAITINGNO", WaitingMeritNo);
                        objParams[5] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[5].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_STUDENT_SP_UPD_SELECTED", objParams, false);
                        if (ret != null)
                            if (ret.ToString() != "-99")
                                retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.UpdateStudentSelection-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetStudentsBySchemeBACK(int admbatch, int schemeno, int degreeno, int schemetype, int semno, int college_id)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);

                        SqlParameter[] objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_ADMBATCH", admbatch);
                        objParams[1] = new SqlParameter("@P_SCHEMENO", schemeno);
                        objParams[2] = new SqlParameter("@P_DEGREENO", degreeno);
                        objParams[3] = new SqlParameter("@P_SCHEMETYPE", schemetype);
                        objParams[4] = new SqlParameter("@P_SEM", semno);
                        objParams[5] = new SqlParameter("@P_COLLEGEID", college_id);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STUDENT_SP_RET_STUDENT_BY_SCHEME1_BACK", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentsForScheme-> " + ex.ToString());
                    }

                    return ds;
                }

                public SqlDataReader GetStudentDetailsBySelection(int sessionno, int degreeno, int branchno, int selected)
                {
                    SqlDataReader dtr = null;
                    try
                    {
                        SQLHelper objSH = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_DEGREENO", degreeno);
                        objParams[2] = new SqlParameter("@P_BRANCHNO", branchno);
                        objParams[3] = new SqlParameter("@P_SELECTED", selected);

                        dtr = objSH.ExecuteReaderSP("PKG_ACD_SELECTED_STUDENTS_OFFER_LATTER", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.GetStudentDetails-> " + ex.ToString());
                    }
                    return dtr;
                }

                public SqlDataReader GetUserDetails(int ua_no)
                {
                    SqlDataReader dtr = null;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_UANO", ua_no);
                        dtr = objSQLHelper.ExecuteReaderSP("PKG_STUDENT_TEACHER_ON_GET_USER", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentDetails->" + ex.ToString());
                    }
                    return dtr;
                }

                public DataSet RetrieveStudentDetailsForRegistration(string search, string category, string val)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_SEARCHSTRING", search);
                        objParams[1] = new SqlParameter("@P_SEARCH", category);
                        objParams[2] = new SqlParameter("@P_ADMBATCH", val);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STUDENT_SP_SEARCH_STUDENT_FOR_REGISTRATION", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.EmpCreateController.RetrieveEmpDetails-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetStudentDetailsForRegistration(int idno)
                {
                    DataSet ds = null;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        //objParams[1] = new SqlParameter("@P_ADMBATCH", admbatch);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_STUDENT_DETAILSBY_NAME", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentDetailsForRegistration-> " + ex.ToString());
                    }
                    return ds;
                }

                public string UpdateNewRegistrationStudentDetails(Student objStudent, StudentQualExm objStudQExm, CertificateMaster objCertMaster)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    //int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        //Update Student
                        objParams = new SqlParameter[21];
                        //First Add Student Parameter
                        objParams[0] = new SqlParameter("@P_IDNO", objStudent.IdNo);
                        objParams[1] = new SqlParameter("@P_STUDNAME", objStudent.StudName);
                        objParams[2] = new SqlParameter("@P_MOTHERNAME", objStudent.MotherName);
                        objParams[3] = new SqlParameter("@P_FATHERNAME", objStudent.FatherName);
                        objParams[4] = new SqlParameter("@P_SEX", objStudent.Sex);
                        objParams[5] = new SqlParameter("@P_DOB", objStudent.Dob);
                        objParams[6] = new SqlParameter("@P_SSCROLLNO", objStudQExm.SSCRollNo);
                        //objParams[7] = new SqlParameter("@P_SSCYEAR", objStudQExm.SSCYear);
                        objParams[7] = new SqlParameter("@P_BOARD", objStudQExm.BOARD);
                        objParams[8] = new SqlParameter("@P_LASTCOLLEGE", objStudQExm.SCHOOL_COLLEGE_NAME);
                        objParams[9] = new SqlParameter("@P_LASTROLLNO", objStudQExm.QEXMROLLNO);
                        objParams[10] = new SqlParameter("@P_LASTYEAR", objStudQExm.YEAR_OF_EXAMHSSC);
                        objParams[11] = new SqlParameter("@P_STUDMOBILE", objStudent.StudentMobile);
                        objParams[12] = new SqlParameter("@P_EMAIL", objStudent.EmailID);
                        objParams[13] = new SqlParameter("@P_STATEOFDOMECIAL", objStudent.Stateof_domecial);
                        objParams[14] = new SqlParameter("@P_NATIONALITYNO", objStudent.NationalityNo);
                        objParams[15] = new SqlParameter("@P_MIGRATIONSTATUS", objCertMaster.MigrationStatus);
                        objParams[16] = new SqlParameter("@P_ENROLLNO", objStudent.EnrollNo);
                        objParams[17] = new SqlParameter("@P_REGSTATUS", objCertMaster.RegStatus);
                        objParams[18] = new SqlParameter("@P_CARDNO", objCertMaster.CardNo);
                        objParams[19] = new SqlParameter("@P_ADMBATCH", objStudent.AdmBatch);
                        objParams[20] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[20].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_GENERATE_REGNO_AND_SAVE_STUDENT_DETAILS", objParams, true);

                        if (objStudent.LastQualifiedExams != null)
                        {
                            foreach (QualifiedExam qualExam in objStudent.LastQualifiedExams)
                            {
                                this.UpdateLastQualExams(qualExam);
                            }
                        }
                        retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        return ret.ToString();
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.UpdateNewRegistrationStudentDetails-> " + ex.ToString());
                    }
                }

                public DataSet RetrieveStudentDetailsRegistrationNoAllotted(string search, string category)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_SEARCHSTRING", search);
                        objParams[1] = new SqlParameter("@P_SEARCH", category);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_SP_STUDENT_SEARCH_BY_ENROLLNO", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.EmpCreateController.RetrieveEmpDetails-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetStudentDetailsForMigration(int idno)
                {
                    DataSet ds = null;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        //objParams[1] = new SqlParameter("@P_ADMBATCH", admbatch);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_STUDENT_DETAILS_FOR_MIGRATION", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentDetailsForRegistration-> " + ex.ToString());
                    }
                    return ds;
                }

                public string UpdateMigrationStudentDetails(Student objStudent, StudentQualExm objStudQExm, CertificateMaster objCertMaster)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        //Update Student
                        objParams = new SqlParameter[13];
                        //First Add Student Parameter
                        objParams[0] = new SqlParameter("@P_IDNO", objStudent.IdNo);
                        objParams[1] = new SqlParameter("@P_STUDNAME", objStudent.StudName);
                        objParams[2] = new SqlParameter("@P_FATHERNAME", objStudent.FatherName);
                        objParams[3] = new SqlParameter("@P_LASTCOLLEGE", objStudQExm.SCHOOL_COLLEGE_NAME);
                        objParams[4] = new SqlParameter("@P_LASTROLLNO", objStudQExm.QEXMROLLNO);
                        objParams[5] = new SqlParameter("@P_LASTYEAR", objStudQExm.YEAR_OF_EXAMHSSC);
                        objParams[6] = new SqlParameter("@P_STUDMOBILE", objStudent.StudentMobile);
                        objParams[7] = new SqlParameter("@P_EMAIL", objStudent.EmailID);
                        objParams[8] = new SqlParameter("@P_PADDRESS", objStudent.PAddress);
                        objParams[9] = new SqlParameter("@P_MIGRATION_ORG_DUPLICT", objCertMaster.Migration_org_duplct);
                        objParams[10] = new SqlParameter("@P_CARDNO", objCertMaster.CardNo);
                        objParams[11] = new SqlParameter("@P_ADMBATCH", objStudent.AdmBatch);
                        objParams[12] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[12].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_SAVE_MIGRATION_DETAILS", objParams, true);

                        if (objStudent.LastQualifiedExams != null)
                        {
                            foreach (QualifiedExam qualExam in objStudent.LastQualifiedExams)
                            {
                                this.UpdateLastQualExams(qualExam);
                            }
                        }
                        retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        return ret.ToString();
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.UpdateMigrationStudentDetails-> " + ex.ToString());
                    }
                }

                public DataSet GetCourseFor_Reval(int idno, int sessionno, int semesterno, int select)
                {
                    DataSet ds = null;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        objParams[1] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[2] = new SqlParameter("@P_SEMESTERNO", semesterno);
                        objParams[3] = new SqlParameter("@P_OPERATION_FLAG ", select);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_COURSE_FOR_REVALUATION", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentDetails->" + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetEligibleStudents(int ADMBAT, int DEGREENO, int QULIFYNO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@ADMBAT", ADMBAT);
                        objParams[1] = new SqlParameter("@DEGREENO", DEGREENO);
                        objParams[2] = new SqlParameter("@QULIFYNO", QULIFYNO);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_USER_ELIGIBLE_STUDENT_LIST", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetEligibleStudents-> " + ex.ToString());
                    }
                    return ds;
                }

                public string UpdateApplicantBranchPref(int SESSIONNO, int USERNO, string branchpref, string ipaddress, string appid, int idno)
                {
                    string retStatus = string.Empty;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[7];

                        objParams[0] = new SqlParameter("@P_SESSIONNO", SESSIONNO);
                        objParams[1] = new SqlParameter("@P_USERNO", USERNO);

                        objParams[2] = new SqlParameter("@P_BRANCHPREF", branchpref);
                        objParams[3] = new SqlParameter("@P_IPADDRESS", ipaddress);
                        objParams[4] = new SqlParameter("@P_APPLICATIONID", appid);
                        objParams[5] = new SqlParameter("@P_IDNO", idno);
                        objParams[6] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                        objParams[6].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_MODIFY_NEWUSER_BRANCHPREF", objParams, true);
                        retStatus = ret.ToString();

                        return retStatus;
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int AllotStudentBranch(int sessionno, string usernos, string ipaddress, int DEGREENO, int QUALIFYNO)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[]
                        {
                        new SqlParameter("@P_SESSIONNO", sessionno),
                        new SqlParameter("@P_USERNO", usernos),
                        new SqlParameter("@P_IPADDRESS", ipaddress),
                        new SqlParameter("@P_DEGREENO", DEGREENO),
                        new SqlParameter("@P_QUALIFYNO", QUALIFYNO),
                        new SqlParameter("@P_OUT", SqlDbType.Int)
                        };

                        objParams[objParams.Length - 1].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_ALLOT_BRANCHPREF_BULK", objParams, true);

                        if (ret != null && ret.ToString() != "-99")

                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.AllotStudentBranch-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetBranchAllotementList(int SESSIONNO, int DEGREENO, int QUALIFYNO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", SESSIONNO);
                        objParams[1] = new SqlParameter("@P_DEGREENO", DEGREENO);
                        objParams[2] = new SqlParameter("@P_QUALIFYNO", QUALIFYNO);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_STUD_BRANCHPREF_STATUS", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetBranchAllotementList() -->" + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetStudentDetailsRtm(int idno)
                {
                    DataSet ds = null;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_GET_SEM_PROMOTION", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentDetails->" + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetStudenRegList_DateWise(int ADMBATCH)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_ADMBATCH", ADMBATCH);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_REPORT_REGISTRATION_COUNT_DATEWISE", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentList_DateWise() -->" + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetConfirmWaitingbothStudents(int ADMBATCH, int STATUS, int DEGREE, int ENTERANCE, int ROUND)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_ADMBATCH", ADMBATCH);
                        objParams[1] = new SqlParameter("@P_STATUS", STATUS);
                        objParams[2] = new SqlParameter("@P_DEGREE", DEGREE);
                        objParams[3] = new SqlParameter("@P_ENTERANCE", ENTERANCE);
                        objParams[4] = new SqlParameter("@P_ROUND", ROUND);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_USER_SEAT_CONFIRM_WAITING", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetEligibleStudents-> " + ex.ToString());
                    }
                    return ds;
                }

                public int InsertJoiningDetails(int joined, int userno, int ADMBATCH, string document_list)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_JOINED", joined);
                        objParams[1] = new SqlParameter("@P_USERNO", userno);
                        objParams[2] = new SqlParameter("@P_ADMBATCH", ADMBATCH);
                        objParams[3] = new SqlParameter("@P_DOCUMENT_LIST", document_list);
                        objParams[4] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                        objParams[4].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_INSERT_APPLICANT_JOINING_DETAILS", objParams, true);
                        retStatus = Convert.ToInt32(ret);
                        return retStatus;
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.InsertJoiningDetails() -->" + ex.ToString());
                    }
                }

                //added by reena
                public DataSet GetAdmittedStudents(int admbatch, int degreeno, int semesterno)
                {
                    DataSet ds = null;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_ADMBATCH", admbatch);
                        objParams[1] = new SqlParameter("@P_DEGREENO", degreeno);
                        objParams[2] = new SqlParameter("@P_SEMESTERNO", semesterno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_ADM_CONFIRM_STUDENT", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.EmpCreateController.GetStudentListForIdentityCard-> " + ex.ToString());
                    }
                    return ds;
                }

                public int UpdateAdmStatus(string idno)
                {
                    //DataSet ds = null;
                    int retStatus = Convert.ToInt32(IITMS.UAIMS.CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];
                        {
                            objParams[0] = new SqlParameter("@P_IDNO", idno);
                            objParams[1] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                            objParams[1].Direction = ParameterDirection.Output;
                        };
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_UPD_STUDENT_ADM_STATUS", objParams, false);

                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(IITMS.UAIMS.CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(IITMS.UAIMS.CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic-> " + ex.Message + " " + ex.StackTrace);
                    }
                    return retStatus;
                }

                //FOR BRANCH PREFRENCE ALLOTMENT ROUND-2

                public DataSet GetEligibleStudents_ForRountTwo(int ADMBAT, int DEGREENO, int QULIFYNO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@ADMBAT", ADMBAT);
                        objParams[1] = new SqlParameter("@DEGREENO", DEGREENO);
                        objParams[2] = new SqlParameter("@QULIFYNO", QULIFYNO);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_USER_ELIGIBLE_STUDENT_LIST_RND2", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetEligibleStudents-> " + ex.ToString());
                    }
                    return ds;
                }

                public int AddStudentApplication(int idno, int Sessionno, string COM_CODE, int AppId, string Reason, int AppStatus, string txtcorrname, string txtcorrfatname, string txtcorrmotname, string mailaddress, int noofcopies)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[]
                        {
                             new SqlParameter("@P_IDNO",idno),
                             new SqlParameter("@P_SESSIONNO",Sessionno),
                             new SqlParameter("@P_COM_CODE",COM_CODE),
                             new SqlParameter("@P_APPID",AppId),
                             new SqlParameter("@P_REASON",Reason),
                             new SqlParameter("@P_APPSTATUS",AppStatus),
                             new SqlParameter("@P_CORRSTUDNAME",txtcorrname),
                             new SqlParameter("@P_CORRFATNAME",txtcorrfatname),
                             new SqlParameter("@P_CORRMOTNAME",txtcorrmotname),
                             new SqlParameter("@P_MAILINGADDRESS",mailaddress),
                             new SqlParameter("@P_NOOFCOPIES",noofcopies),
                             new SqlParameter("@P_OUT",SqlDbType.Int)
                        };
                        objParams[objParams.Length - 1].Direction = ParameterDirection.Output;
                        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_ADD_STUDENT_APPLICATION_LIST", objParams, true);

                        if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001")
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.Error);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentController.AddStudentApplication --> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetStudentApplicationsForModfication(int APPIDADMIN)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_APPIDADMIN", APPIDADMIN);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_STUDENT_APPLICATION_FOR_MODIFICATION", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ElectionController.GetApplicationsForElePost() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }

                public int Insert_Payment_StudentApplicationForm(int idno, int sessionno, int SEMESTERNO, string ORDER_ID, int PAYMENTTYPE, int APNO, string REGUGLAR_COURSE_AMOUNT,
            string BACKLOG_COURSE_AMOUNT, string SUPPLY_COURSE_AMOUNT, string MAKEUP_COURSE_AMOUNT, string PaperSeeingFee, string ReadressalFee, string NameCorrectionFee,
            string DuplicategradeCardFee, string OfficailTranscriptFee, string ConsolidatedgcswFee, string ProvisionalDegreeFee, string LATE_FEES_AMOUNT, string amount,
            string COM_CODE1)//int dmno, , string AMOUNT,Int64 customerRef, string APTRANSACTIONID)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSqlHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] param = new SqlParameter[]
                        {
                            new SqlParameter("@P_IDNO", idno),
                            new SqlParameter("@P_SESSIONNO", sessionno),
                            new SqlParameter("@P_SEMESTERNO", SEMESTERNO),
                            new SqlParameter("@P_ORDER_ID", ORDER_ID),
                            new SqlParameter("@P_PAYMENTTYPE", PAYMENTTYPE),
                            new SqlParameter("@P_APNO", APNO),
                            new SqlParameter("@P_COM_CODE", COM_CODE1),
                            new SqlParameter("@P_REGUGLAR_COURSE_AMOUNT", REGUGLAR_COURSE_AMOUNT),
                            new SqlParameter("@P_BACKLOG_COURSE_AMOUNT", BACKLOG_COURSE_AMOUNT),
                            new SqlParameter("@P_SUPPLY_COURSE_AMOUNT", SUPPLY_COURSE_AMOUNT),
                            new SqlParameter("@P_MAKEUP_COURSE_AMOUNT", MAKEUP_COURSE_AMOUNT),
                            new SqlParameter("@P_PAPER_SEEING_AMOUNT", PaperSeeingFee),
                            new SqlParameter("@P_READRESSAL_AMOUNT", ReadressalFee),
                            new SqlParameter("@P_NAME_CORRECTION_AMOUNT", NameCorrectionFee),
                            new SqlParameter("@P_DUPLICATEGRADECARD_AMOUNT", DuplicategradeCardFee),
                            new SqlParameter("@P_OFFICIALTRANSCRIPT_AMOUNT", OfficailTranscriptFee),
                            new SqlParameter("@P_CONSOLIDATEDGRADECSW_AMOUNT", ConsolidatedgcswFee),
                            new SqlParameter("@P_PROVISIONALDEGREE_AMOUNT", ProvisionalDegreeFee),
                            new SqlParameter("@P_LATE_FEES_AMOUNT", LATE_FEES_AMOUNT),
                            new SqlParameter("@P_TOTAL_AMOUNT", amount),

                            new SqlParameter("@P_OUTPUT", SqlDbType.Int)
                        };
                        param[param.Length - 1].Direction = ParameterDirection.Output;
                        object ret = objSqlHelper.ExecuteNonQuerySP("PR_INS_ONLINE_PAYMENT_FOR_STUDENT_APPLICATION", param, true);

                        if (ret != null && ret.ToString() != "-99")
                            retStatus = Convert.ToInt32(ret);
                        else
                            retStatus = -99;
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.UpdatePaymenttypeofStudents-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int StudentApplicationFormApprove(int apidno, int idno, int approve_status, string Remark, int UserNo)
                {
                    int status = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[]
                {
                    new SqlParameter("@P_APP_TRANS_ID", idno) ,
                    new SqlParameter("@P_IDNO", idno) ,
                    new SqlParameter("@P_APP_STATUS", approve_status) ,
                      new SqlParameter("@P_REMARK", Remark) ,
                      new SqlParameter("@P_USER_NO", UserNo) ,
                    new SqlParameter("@P_OUT", status)
                };
                        objParams[objParams.Length - 1].Direction = ParameterDirection.InputOutput;

                        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_STUDENT_APPLICATION_FORM_APPROVAL", objParams, true);

                        if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001")
                            status = Convert.ToInt32(CustomStatus.RecordSaved);
                        else
                            status = Convert.ToInt32(CustomStatus.Error);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.BatchController.GetBatchByNo() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return status;
                }

                public DataSet GetStudentRecieptInformation(int idno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GETS_PAID_FEES_DETAILS_FOR_STUDENT_APPLICATION_REGISRATION", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentDetails->" + ex.ToString());
                    }
                    return ds;
                }

                //GET STUDENT LIST FOR DOCUMENTS VERIFICATION
                public DataSet GetStudents_ForDocumentsVerification(string USERNAME)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_USERNAME", USERNAME);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_GET_STUDENT_FOR_DOCS_VERIFICATION", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetEligibleStudents-> " + ex.ToString());
                    }
                    return ds;
                }

                //METHOD TO GET THE STUDENTS LIST FOR DETAIN[27-09-2016]
                public DataSet GetStudentListForDetention(int SESSIONNO, int SCHEMENO, int SEMESTERNO, int DETAINBY)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);

                        SqlParameter[] objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", SESSIONNO);
                        objParams[1] = new SqlParameter("@P_SCHEMENO", SCHEMENO);
                        objParams[2] = new SqlParameter("@P_SEMESTERNO", SEMESTERNO);
                        objParams[3] = new SqlParameter("@P_DETAINBY", DETAINBY);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_GET_STUDENT_LIST_FOR_DETENTION", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentController.GetDetainedList-> " + ex.ToString());
                    }
                    return ds;
                }

                public int AddCopyCaseForExam(Student_Acd objStud, string offcourse)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[]
                        {
                             new SqlParameter("@P_SESSIONNO",objStud.SessionNo),
                             new SqlParameter("@P_IDNO",objStud.IdNo),
                             new SqlParameter("@P_SEATNO",objStud.Seatno),
                             new SqlParameter("@P_SEMESTERNO",objStud.SemesterNo),
                             new SqlParameter("@P_SCHEMENO",objStud.SchemeNo),
                             new SqlParameter("@P_DEGREENO",objStud.DegreeNo),
                             new SqlParameter("@P_SECTIONNO",objStud.Sectionno ),
                             new SqlParameter("@P_STATUS",objStud.Status),
                             new SqlParameter("@P_COURSENO",objStud.CourseNo),
                             new SqlParameter("@P_PUNISHMENTNO",objStud.PUNISHMENTNO),
                              new SqlParameter("@P_JUNIORNAME",objStud.Juniorname),
                             new SqlParameter("@P_SENIORNAME",objStud.Seniorname),
                             new SqlParameter("@P_UFMDETAIL",objStud.Ufmdetails),
                             new SqlParameter("@P_REMARKS",objStud.Remarks),
                             new SqlParameter("@P_OUT",SqlDbType.Int)
                        };
                        objParams[objParams.Length - 1].Direction = ParameterDirection.Output;
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_STUDENT_PUNISHMENET_APPLIED_STUD_LIST", objParams, false) != null)

                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentController.AddCopyCase --> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int UpdateStudentAddressDetails(Student objStudent, StudentAddress objStudAddress, int usertype)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        //Update Student Local Address
                        objParams = new SqlParameter[29];
                        objParams[0] = new SqlParameter("@P_IDNO", objStudent.IdNo);
                        objParams[1] = new SqlParameter("@P_LADDRESS", objStudAddress.LADDRESS);
                        objParams[2] = new SqlParameter("@P_LCITY", objStudAddress.LCITY);
                        objParams[3] = new SqlParameter("@P_LSTATE", objStudAddress.LSTATE);
                        objParams[4] = new SqlParameter("@P_LDISTRICT", objStudAddress.LDISTRICT);
                        objParams[5] = new SqlParameter("@P_LPINCODE", objStudAddress.LPINCODE);
                        objParams[6] = new SqlParameter("@P_LTELEPHONE", objStudAddress.LTELEPHONE);
                        objParams[7] = new SqlParameter("@P_LMOBILE", objStudAddress.LMOBILE);
                        objParams[8] = new SqlParameter("@P_LPOSTOFF", objStudAddress.LPOSTOFF);
                        objParams[9] = new SqlParameter("@P_LPOLICESTATION", objStudAddress.LPOLICESTATION);
                        //Update Student Permenant Address
                        objParams[10] = new SqlParameter("@P_PADDRESS", objStudAddress.PADDRESS);
                        objParams[11] = new SqlParameter("@P_PCITY", objStudAddress.PCITY);
                        objParams[12] = new SqlParameter("@P_PSTATE", objStudAddress.PSTATE);
                        objParams[13] = new SqlParameter("@P_PDISTRICT", objStudAddress.PDISTRICT);
                        objParams[14] = new SqlParameter("@P_PPINCODE", objStudAddress.PPINCODE);
                        objParams[15] = new SqlParameter("@P_PTELEPHONE", objStudAddress.PTELEPHONE);
                        objParams[16] = new SqlParameter("@P_PMOBILE", objStudAddress.PMOBILE);
                        objParams[17] = new SqlParameter("@P_LTEHSIL", objStudAddress.LTEHSIL);
                        objParams[18] = new SqlParameter("@P_PPOSTOFF", objStudAddress.PPOSTOFF);
                        objParams[19] = new SqlParameter("@P_PPOLICEOFF", objStudAddress.PPOLICESTATION);
                        //Update Student Guardian Address
                        objParams[20] = new SqlParameter("@P_GADDRESS", objStudAddress.GADDRESS);
                        objParams[21] = new SqlParameter("@P_GUARDIANNAME", objStudAddress.GUARDIANNAME);
                        objParams[22] = new SqlParameter("@P_GPHONE", objStudAddress.GPHONE);
                        objParams[23] = new SqlParameter("@P_ANNUAL_INCOME", objStudAddress.ANNUAL_INCOME);
                        objParams[24] = new SqlParameter("@P_RELATION_GUARDIAN", objStudAddress.RELATION_GUARDIAN);
                        objParams[25] = new SqlParameter("@P_GOCCUPATIONNAME", objStudAddress.GOCCUPATIONNAME);
                        objParams[26] = new SqlParameter("@P_GUARDIANDESIG", objStudAddress.GUARDIANDESIGNATION);
                        objParams[27] = new SqlParameter("@P_USER_TYPE ", usertype);
                        objParams[28] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[28].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_STUDENT_SP_UPD_STUD_ADDRESS_DETAILS", objParams, true);

                        if (Convert.ToInt32(ret) == 1)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.Error);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.UpdateStudentAddressDetails-> " + ex.ToString());
                    }

                    return retStatus;
                }

                public int UpdateStudentAdmissionDetails(Student objStudent, int usertype, int userno)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        //Update Student Local Address
                        objParams = new SqlParameter[17];
                        objParams[0] = new SqlParameter("@P_IDNO", objStudent.IdNo);
                        if (objStudent.AdmDate == DateTime.MinValue)
                            objParams[1] = new SqlParameter("@P_ADMDATE", DBNull.Value);
                        else
                            objParams[1] = new SqlParameter("@P_ADMDATE", objStudent.AdmDate);
                        objParams[2] = new SqlParameter("@P_COLLEGE_ID", objStudent.College_ID);
                        objParams[3] = new SqlParameter("@P_DEGREENO", objStudent.DegreeNo);
                        objParams[4] = new SqlParameter("@P_BRANCHNO", objStudent.BranchNo);
                        objParams[5] = new SqlParameter("@P_YEAR", objStudent.Year);
                        objParams[6] = new SqlParameter("@P_SEMESTERNO", objStudent.SemesterNo);
                        objParams[7] = new SqlParameter("@ADMCATEGORYNO", objStudent.AdmCategoryNo);
                        objParams[8] = new SqlParameter("@P_BATCHNO", objStudent.BatchNo);
                        objParams[9] = new SqlParameter("@P_USER_TYPE", usertype);
                        objParams[10] = new SqlParameter("@P_CLAIM_TYPE", objStudent.ClaimType);
                        objParams[11] = new SqlParameter("@ENROLLNO", objStudent.EnrollNo);     //Added Shrikant Ramekar 21 feb 19
                        objParams[12] = new SqlParameter("@CATEGORYNO", objStudent.CategoryNo);
                        objParams[13] = new SqlParameter("@COLLEGE_JSS", objStudent.CollegeJss);  //  objS.CollegeCode = Session["colcode"].ToString();
                        //objParams[13] = new SqlParameter("@COLLEGE_CODE", objStudent.CollegeCode);
                        objParams[14] = new SqlParameter("@PTYPE", objStudent.PType);
                        objParams[15] = new SqlParameter("@P_UANO", userno);
                        objParams[16] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[16].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_STUDENT_SP_UPD_STUD_ADMISSION_DETAILS", objParams, true);

                        if (Convert.ToInt32(ret) == 1)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.Error);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.UpdateStudentAddressDetails-> " + ex.ToString());
                    }

                    return retStatus;
                }

                public int UpdateStudentDASAInformation(Student objStudent, int usertype)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        //Update Student Local Address
                        objParams = new SqlParameter[12];
                        objParams[0] = new SqlParameter("@P_IDNO", objStudent.IdNo);
                        objParams[1] = new SqlParameter("@P_VISA_EXPIRY_DATE", objStudent.VisaExpiryDate);
                        objParams[2] = new SqlParameter("@P_PASSPORT_EXPIRY_DATE", objStudent.PassportExpiryDate);
                        objParams[3] = new SqlParameter("@P_PASSPORT_ISSUE_DATE", objStudent.PassportIssueDate);
                        objParams[4] = new SqlParameter("@P_STAY_PERMIT_DATE", objStudent.StayPermitDate);
                        objParams[5] = new SqlParameter("@P_INDIAN_ORIGIN", objStudent.IndianOrigin);
                        objParams[6] = new SqlParameter("@P_SCHOL_SCHEME", objStudent.ScholarshipScheme);
                        objParams[7] = new SqlParameter("@P_AGENCY", objStudent.Agency);
                        objParams[8] = new SqlParameter("@P_PASSPORT_ISSUE_PLACE", objStudent.PassportIssuePlace);
                        objParams[9] = new SqlParameter("@P_CITIZENSHIP", objStudent.Citizenship);
                        objParams[10] = new SqlParameter("@P_USER_TYPE ", usertype);
                        objParams[11] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[11].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_STUDENT_SP_UPD_STUD_DASA_INFORMATION", objParams, true);

                        if (Convert.ToInt32(ret) == 1)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.Error);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.UpdateStudentDASAInformation-> " + ex.ToString());
                    }

                    return retStatus;
                }

                public int UpdateStudentOtherInformation(Student objStudent, int usertype)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        //Update Student Local Address
                        objParams = new SqlParameter[18];
                        objParams[0] = new SqlParameter("@P_IDNO", objStudent.IdNo);
                        objParams[1] = new SqlParameter("@P_BIRTH_PLACE", objStudent.BirthPlace);
                        objParams[2] = new SqlParameter("@P_MTOUNGENO", objStudent.MToungeNo);
                        objParams[3] = new SqlParameter("@P_OTHER_LANGUAGE", objStudent.OtherLanguage);
                        objParams[4] = new SqlParameter("@P_BIRTH_VILLAGE", objStudent.Birthvillage);
                        objParams[5] = new SqlParameter("@P_BIRTH_TALUKA", objStudent.Birthtaluka);
                        objParams[6] = new SqlParameter("@P_BIRTH_DISTRICT", objStudent.Birthdistrict);
                        objParams[7] = new SqlParameter("@P_BIRTH_STATE", objStudent.Birthdistate);
                        objParams[8] = new SqlParameter("@P_BIRTH_PINCODE", objStudent.BirthPinCode);
                        objParams[9] = new SqlParameter("@P_HEIGHT", objStudent.Height);
                        objParams[10] = new SqlParameter("@P_WEIGHT", objStudent.Weight);
                        objParams[11] = new SqlParameter("@P_URBAN", objStudent.Urban);
                        objParams[12] = new SqlParameter("@P_IDENTI_MARK", objStudent.IdentyMark);
                        objParams[13] = new SqlParameter("@P_BANKNO", objStudent.BankNo);
                        objParams[14] = new SqlParameter("@P_ACC_NO", objStudent.AccNo);
                        objParams[15] = new SqlParameter("@P_PASSPORTNO", objStudent.PassportNo);
                        objParams[16] = new SqlParameter("@P_USER_TYPE", usertype);
                        objParams[17] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[17].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_STUDENT_SP_UPD_STUD_OTHER_INFORMATION", objParams, true);

                        if (Convert.ToInt32(ret) == 1)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.Error);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.UpdateStudentDASAInformation-> " + ex.ToString());
                    }

                    return retStatus;
                }

                public int UpdateStudentPersonalInformation(Student objStudent, StudentAddress objStudAddress, StudentPhoto objStudPhoto, StudentQualExm objStudQExm, string MotherMobile, string MotherOfficeNo, string IndusEmail, int usertype)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    //int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        //Update Student
                        objParams = new SqlParameter[44];
                        objParams[0] = new SqlParameter("@P_IDNO", objStudent.IdNo);
                        objParams[1] = new SqlParameter("@P_REGNO", objStudent.RegNo);
                        objParams[2] = new SqlParameter("@P_ENROLLNO", objStudent.EnrollNo);
                        objParams[3] = new SqlParameter("@P_STUDFULLNAME", objStudent.StudName);
                        objParams[4] = new SqlParameter("@P_STUDFIRSTNAME", objStudent.firstName);
                        objParams[5] = new SqlParameter("@P_STUDMIDDLENAME", objStudent.MiddleName);
                        objParams[6] = new SqlParameter("@P_STUDLASTNAME", objStudent.LastName);
                        objParams[7] = new SqlParameter("@P_FATHERFULLNAME", objStudent.FatherName);
                        objParams[8] = new SqlParameter("@P_FATHERFIRSTNAME", objStudent.fatherfirstName);
                        objParams[9] = new SqlParameter("@P_FATHERMIDDLENAME", objStudent.FatherMiddleName);
                        objParams[10] = new SqlParameter("@P_FATHERLASTNAME", objStudent.FatherLastName);
                        objParams[11] = new SqlParameter("@P_FATHERMOBILE", objStudent.FatherMobile);
                        objParams[12] = new SqlParameter("@P_FATHEROFFICENO", objStudent.FatherOfficeNo);
                        objParams[13] = new SqlParameter("@P_FATHER_DESIG", objStudAddress.FATHER_DESIG);
                        objParams[14] = new SqlParameter("@P_FATHEROCCUPATIONNO", objStudAddress.OCCUPATION);
                        objParams[15] = new SqlParameter("@P_FATHEREMAIL", objStudent.Fatheremail);
                        objParams[16] = new SqlParameter("@P_MOTHERNAME", objStudent.MotherName);
                        objParams[17] = new SqlParameter("@P_MOTHERMOBILE", MotherMobile);
                        objParams[18] = new SqlParameter("@P_MOTHEREMAIL", objStudent.Motheremail);
                        objParams[19] = new SqlParameter("@P_MOTHER_DESIG", objStudAddress.MOTHERDESIGNATION);
                        objParams[20] = new SqlParameter("@P_MOTHEROFFICENO", MotherOfficeNo);
                        objParams[21] = new SqlParameter("@P_MOTHER_OCCUPATIONNO", objStudAddress.MOTHEROCCUPATION);
                        objParams[22] = new SqlParameter("@P_CASTE", objStudent.Caste);
                        objParams[23] = new SqlParameter("@P_SUB_CASTE", objStudent.Subcaste);
                        objParams[24] = new SqlParameter("@P_DOB", objStudent.Dob);
                        objParams[25] = new SqlParameter("@P_BLOODGRPNO", objStudent.BloodGroupNo);
                        objParams[26] = new SqlParameter("@P_CLAIMID", objStudent.ClaimType);
                        objParams[27] = new SqlParameter("@P_RELIGIONNO", objStudent.ReligionNo);
                        objParams[28] = new SqlParameter("@P_NATIONALITYNO", objStudent.NationalityNo);
                        objParams[29] = new SqlParameter("@P_CATEGORYNO", objStudent.CategoryNo);
                        objParams[30] = new SqlParameter("@P_MARRIED", objStudent.Married);
                        objParams[31] = new SqlParameter("@P_ADDHARCARDNO", objStudent.AddharcardNo);
                        objParams[32] = new SqlParameter("@P_TYPE_OF_PHYSICALLY_HANDICAP", objStudent.Physical_Handicap);
                        objParams[33] = new SqlParameter("@P_SEX", objStudent.Sex);
                        objParams[34] = new SqlParameter("@P_INDUSEMAIL", IndusEmail);
                        objParams[35] = new SqlParameter("@P_STUDENTMOBILE", objStudent.StudentMobile);
                        objParams[36] = new SqlParameter("@P_EMAILID", objStudent.EmailID);
                        objParams[37] = new SqlParameter("@P_USERTYPE", usertype);
                        objParams[38] = new SqlParameter("@P_UANO", objStudent.Uano);
                        objParams[39] = new SqlParameter("@P_PHOTOPATH", objStudPhoto.PhotoPath);
                        objParams[40] = new SqlParameter("@P_ANNUAL_INCOME", objStudent.Annual_income);
                        objParams[41] = new SqlParameter("@P_PHOTOSIZE", objStudPhoto.PhotoSize);
                        if (objStudPhoto.Photo1 == null)
                            objParams[42] = new SqlParameter("@P_PHOTO", DBNull.Value);
                        else
                            objParams[42] = new SqlParameter("@P_PHOTO", objStudPhoto.Photo1);

                        objParams[42].SqlDbType = SqlDbType.Image;
                        objParams[43] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[43].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_STUDENT_SP_UPD_STUDENT_PERSONAL_INFORMATION", objParams, true);

                        if (Convert.ToInt32(ret) == 1)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.Error);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.UpdateStudentPersonalInformation-> " + ex.ToString());
                    }

                    return retStatus;
                }

                public int UpdateStudentQualifyingExamInformation(Student objStudent, StudentQualExm objStudQExm, int usertype)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        //Update Student Local Address
                        objParams = new SqlParameter[49];
                        objParams[0] = new SqlParameter("@P_IDNO", objStudent.IdNo);
                        objParams[1] = new SqlParameter("@P_SCHOOL_COLLEGE_NAMESSC", objStudQExm.SchoolCollegeNameSsc);
                        objParams[2] = new SqlParameter("@P_BOARDSSC", objStudQExm.BoardSsc);
                        objParams[3] = new SqlParameter("@P_YEAR_OF_EXAMSSC", objStudQExm.YearOfExamSsc);
                        objParams[4] = new SqlParameter("@P_SSC_MEDIUM", objStudQExm.SSC_medium);
                        objParams[5] = new SqlParameter("@P_MARKS_OBTAINEDSSC", objStudQExm.MarksObtainedSsc);
                        objParams[6] = new SqlParameter("@P_OUT_OF_MRKSSSC", objStudQExm.OutOfMarksSsc);
                        objParams[7] = new SqlParameter("@P_PERSSC", objStudQExm.PercentageSsc);
                        objParams[8] = new SqlParameter("@P_QEXMROLLNOSSC", objStudQExm.QEXMROLLNOSSC);
                        objParams[9] = new SqlParameter("@P_PERCENTILESSC", objStudQExm.PercentileSsc);
                        objParams[10] = new SqlParameter("@P_GRADESSC", objStudQExm.GradeSsc);
                        objParams[11] = new SqlParameter("@P_ATTEMPTSSC", objStudQExm.AttemptSsc);
                        objParams[12] = new SqlParameter("@P_COLLEGE_ADD_SSC", objStudQExm.colg_address_SSC);

                        objParams[13] = new SqlParameter("@P_SCHOOL_COLLEGE_NAMEHSSC", objStudQExm.SCHOOL_COLLEGE_NAME);
                        objParams[14] = new SqlParameter("@P_BOARDHSSC", objStudQExm.BOARD);
                        objParams[15] = new SqlParameter("@P_YEAR_OF_EXAMHSSC", objStudQExm.YEAR_OF_EXAMHSSC);
                        objParams[16] = new SqlParameter("@P_HSSC_MEDIUM", objStudQExm.HSSC_medium);
                        objParams[17] = new SqlParameter("@P_MARKS_OBTAINEDHSSC", objStudQExm.MARKOBTAINED);
                        objParams[18] = new SqlParameter("@P_OUT_OF_MRKSHSSC", objStudQExm.OUTOFMARK);
                        objParams[19] = new SqlParameter("@P_PERHSSC", objStudQExm.PERCENTAGE);
                        objParams[20] = new SqlParameter("@P_QEXMROLLNOHSSC", objStudQExm.QEXMROLLNOHSSC);
                        objParams[21] = new SqlParameter("@P_PERCENTILEHSSC", objStudQExm.PercentileHSsc);
                        objParams[22] = new SqlParameter("@P_GRADEHSSC", objStudQExm.GRADE);
                        objParams[23] = new SqlParameter("@P_ATTEMPTHSSC", objStudQExm.ATTEMPT);
                        objParams[24] = new SqlParameter("@P_COLLEGE_ADD_HSSC", objStudQExm.colg_address_HSSC);

                        objParams[25] = new SqlParameter("@P_HSC_CHE", objStudQExm.HSCCHE);
                        objParams[26] = new SqlParameter("@P_HSC_CHE_MAX", objStudQExm.HSCCHEMAX1);
                        objParams[27] = new SqlParameter("@P_HSC_PHY", objStudQExm.HSCPHY1);
                        objParams[28] = new SqlParameter("@P_HSC_PHY_MAX", objStudQExm.HSCPHYMAX1);
                        objParams[29] = new SqlParameter("@P_HSC_ENG", objStudQExm.ENG);
                        objParams[30] = new SqlParameter("@P_HSC_ENG_MAX", objStudQExm.HSCENGMAX);
                        objParams[31] = new SqlParameter("@P_HSC_MAT", objStudQExm.MATHS);
                        objParams[32] = new SqlParameter("@P_HSC_MAT_MAX", objStudQExm.MATHSMAX);

                        objParams[33] = new SqlParameter("@P_QUALIFYNO", objStudQExm.QUALIFYNO);
                        objParams[34] = new SqlParameter("@P_QEXMROLLNO", objStudent.QexmRollNo);
                        objParams[35] = new SqlParameter("@P_YEAR_OF_EXAM", objStudent.YearOfExam);
                        objParams[36] = new SqlParameter("@P_PERCENTAGE", objStudent.Percentage);
                        objParams[37] = new SqlParameter("@P_PERCENTILE", objStudQExm.PERCENTILE);
                        objParams[38] = new SqlParameter("@P_ALL_INDIA_RANK", objStudQExm.ALLINDIARANK);
                        objParams[39] = new SqlParameter("@P_SCORE", objStudent.Score);

                        objParams[40] = new SqlParameter("@P_OTHERQUALIFYNO", objStudent.PGQUALIFYNO);
                        objParams[41] = new SqlParameter("@P_OTHERQEXMROLLNO", objStudent.PGENTROLLNO);
                        objParams[42] = new SqlParameter("@P_OTHER_YEAR_OF_EXAM", objStudent.pgyearOfExam);
                        objParams[43] = new SqlParameter("@P_OTHERPERCENTAGE", objStudent.pgpercentage);
                        objParams[44] = new SqlParameter("@P_OTHERPERCENTILE", objStudent.pgpercentile);
                        objParams[45] = new SqlParameter("@P_OTHER_ALL_INDIA_RANK", objStudent.PGRANK);
                        objParams[46] = new SqlParameter("@P_OTHER_SCORE", objStudent.pgscore);

                        objParams[47] = new SqlParameter("@P_USER_TYPE", usertype);
                        objParams[48] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[48].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_STUDENT_SP_UPD_STUD_QUALIFYING_EXAM_INFORMATION", objParams, true);

                        if (Convert.ToInt32(ret) == 1)
                        {
                            if (objStudent.LastQualifiedExams != null)
                            {
                                foreach (QualifiedExam qualExam in objStudent.LastQualifiedExams)
                                {
                                    this.UpdateLastQualExams(qualExam);
                                }
                            }
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.Error);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.UpdateStudentDASAInformation-> " + ex.ToString());
                    }

                    return retStatus;
                }

                public int UpdateStudentWorkexp(Student objStudent, int expno)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[7];
                        objParams[0] = new SqlParameter("@P_IDNO", objStudent.IdNo);
                        objParams[1] = new SqlParameter("@P_WORK_EXP", objStudent.WorkExp);
                        objParams[2] = new SqlParameter("@P_ORG_LAST_WORK", objStudent.OrgLastWork);
                        objParams[3] = new SqlParameter("@P_DESIGNATION", objStudent.Designation);
                        objParams[4] = new SqlParameter("@P_EXPNO", expno);
                        objParams[5] = new SqlParameter("@P_EPFNO", objStudent.EpfNo);
                        objParams[6] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[6].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_STUDENT_SP_INS_STUD_WORK_INFORMATION", objParams, true);
                        if (Convert.ToInt32(ret) == 1)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else if (Convert.ToInt32(ret) == 2)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.Error);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.UpdateStudentDASAInformation-> " + ex.ToString());
                    }

                    return retStatus;
                }

                public DataSet GetStudentExpDetails(int idno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STUDENT_SP_RET_STUDENT_EXPDETAILS_BYID", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentDetails->" + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetSecionName(int semesterno, int weekno, string sectionname, string sectionno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_SEMESTERNO", semesterno);
                        objParams[1] = new SqlParameter("@P_WEEKNO", weekno);
                        objParams[2] = new SqlParameter("@P_SECTIONNAME", sectionname);
                        objParams[3] = new SqlParameter("@P_SECTIONNO", sectionno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_SECTION_NAME", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetSectionName->" + ex.ToString());
                    }
                    return ds;
                }

                public int GetdeleteDataRow(int idno, int expno)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        objParams[1] = new SqlParameter("@P_expno", expno);
                        objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[2].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_STUDENT_SP_INS_DELETE_WORK_INFORMATION", objParams, true);
                        if (Convert.ToInt32(ret) == 1)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordDeleted);
                        }
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.UpdateStudentDASAInformation-> " + ex.ToString());
                    }

                    return retStatus;
                }

                public DataSet GetStudentsBySchemeElective(int admbatch, int schemeno, int degreeno, int schemetype, int semno, int college_id, int sectionno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);

                        SqlParameter[] objParams = new SqlParameter[7];
                        objParams[0] = new SqlParameter("@P_ADMBATCH", admbatch);
                        objParams[1] = new SqlParameter("@P_SCHEMENO", schemeno);
                        objParams[2] = new SqlParameter("@P_DEGREENO", degreeno);
                        objParams[3] = new SqlParameter("@P_SCHEMETYPE", schemetype);
                        objParams[4] = new SqlParameter("@P_SEM", semno);
                        objParams[5] = new SqlParameter("@P_COLLEGEID", college_id);
                        objParams[6] = new SqlParameter("@P_SECTIONNO", sectionno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STUDENT_SP_RET_STUDENT_BY_SCHEME1_ELECTIVE", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentsForScheme-> " + ex.ToString());
                    }

                    return ds;
                }

                public string GeneratePassword()
                {
                    string allowedChars = "";
                    allowedChars = "a,b,c,d,e,f,g,h,i,j,k,m,n,p,q,r,s,t,u,v,w,x,y,z,";
                    allowedChars += "A,B,C,D,E,F,G,H,J,K,L,M,N,P,Q,R,S,T,U,V,W,X,Y,Z,";
                    allowedChars += "2,3,4,5,6,7,8,9"; //,!,@,#,$,%,&,?
                    //--------------------------------------
                    char[] sep = { ',' };

                    string[] arr = allowedChars.Split(sep);

                    string passwordString = "";

                    string temp = "";

                    Random rand = new Random();

                    for (int i = 0; i < 7; i++)
                    {
                        temp = arr[rand.Next(0, arr.Length)];
                        passwordString += temp;
                    }
                    return passwordString;
                }

                public DataSet GetStudentForFaculty(int facno, int branchno, int sem, int degreeno, int sectionno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_FAC", facno);
                        objParams[1] = new SqlParameter("@P_BRANCHNO", branchno);
                        objParams[2] = new SqlParameter("@P_SEM", sem);
                        objParams[3] = new SqlParameter("@P_DEGREENO", degreeno);
                        objParams[4] = new SqlParameter("@P_SECTIONNO", sectionno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STUDENT_SP_RET_STUDENT_FOR_FA", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentForFaculty-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetRemaingStudentForFaculty(int facno, int branchno, int sem, int degreeno, int sectionno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[5];

                        objParams[0] = new SqlParameter("@P_FAC", facno);
                        objParams[1] = new SqlParameter("@P_BRANCHNO", branchno);
                        objParams[2] = new SqlParameter("@P_SEM", sem);
                        objParams[3] = new SqlParameter("@P_DEGREENO", degreeno);
                        objParams[4] = new SqlParameter("@P_SECTIONNO", sectionno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STUDENT_SP_RET_STUDENT_FOR_FA_REMAINING", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentController.GetStudentForFaculty-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetStudentForFaculty_UA_NAME(int degreeno, int branchno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_DEGREENO", degreeno);
                        objParams[1] = new SqlParameter("@P_BRANCHNO", branchno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_GET_FACULTY_UANO_AND_NAME", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentController.GetStudentForFaculty-> " + ex.ToString());
                    }
                    return ds;
                }

                public int UpdateStudent_FacultAdvisor(Student objStudent)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_STUDID", objStudent.StudId);
                        objParams[1] = new SqlParameter("@P_FACULTYADVISOR", objStudent.FacAdvisor);
                        objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[2].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_STUDENT_SP_UPD_BYFACULTY_ADVISOR_NEW", objParams, false);
                        if (ret != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.UpdateStudent_TeachAllot-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int StudentBulkPhotoUpdate(int ddlValue, DataTable dt)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        // if (!(photo == null))
                        // {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_ID", ddlValue);
                        objParams[1] = new SqlParameter("@P_STUD_BULK_UPDATE", dt);

                        if (objSQLHelper.ExecuteNonQuerySP("STUD_BULK_PHOTO_UPDATE", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        // }
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentController.UpdateStudentPhoto->" + ex.ToString());
                    }

                    return retStatus;
                }

                public DataSet GetApprovalStatus(int AdmBatch, int AdmType)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_ADMBATCH", AdmBatch);
                        objParams[1] = new SqlParameter("@P_ADMTYPE", AdmType);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_GET_APPROVAL_STATUS", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentController.GetApprovalStatus-> " + ex.ToString());
                    }
                    return ds;
                }

                public int ToggleStudentApprovalStatus(int uaNo, int uaDec, int Aprv_by)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_UA_NO", uaNo);
                        objParams[1] = new SqlParameter("@P_UA_DEC", uaDec);
                        objParams[2] = new SqlParameter("@P_APRV_BY", Aprv_by);
                        objParams[3] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[3].Direction = ParameterDirection.Output;

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ACD_TOGGLE_STUDENT_APPROVAL_STATUS", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        // }
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentController.ToggleStudentApprovalStatus->" + ex.ToString());
                    }

                    return retStatus;
                }

                /// <summary>
                /// Added By Irfan Shaikh on 20190615
                /// used in : StudentapprovalStatus.aspx
                /// </summary>
                /// <returns></returns>
                public int ToggleStudentCancelStatus(int uaNo, int cancel, int can_by)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_UA_NO", uaNo);
                        objParams[1] = new SqlParameter("@P_CANCEL", cancel);
                        objParams[2] = new SqlParameter("@P_CAN_BY", can_by);
                        objParams[3] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[3].Direction = ParameterDirection.Output;

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ACD_STUDENT_CANCEL_STATUS", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        // }
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentController.ToggleStudentCancelStatus->" + ex.ToString());
                    }

                    return retStatus;
                }

                public DataSet getFacuilty(int session, int scheme, int semester, int subject)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", session);
                        objParams[1] = new SqlParameter("@P_SCHEMENO", scheme);
                        objParams[2] = new SqlParameter("@P_SEMESTERNO", semester);
                        objParams[3] = new SqlParameter("@P_COURSENO", subject);
                        ds = objSQLHelper.ExecuteDataSetSP("ACD_GET_FACULITY_INTAKE", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentRegistrationController.getFacuilty-> " + ex.ToString());
                    }
                    return ds;
                }

                public int updateFacultyLOCKIntake(int ctno1, int lockintake)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_CTNO", ctno1);
                        objParams[1] = new SqlParameter("@P_LOCK_INTAKE", lockintake);
                        objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[2].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("ACD_INSERT_FACULITY_LOCK_INTAKE", objParams, true);
                        if (ret != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.insertFacuiltyIntake-> " + ex.ToString());
                    }

                    return retStatus;
                }

                public int insertFacuiltyIntake(int intake, int ctno1)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_CTNO", ctno1);
                        objParams[1] = new SqlParameter("@P_INTAKE", intake);
                        objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[2].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("ACD_INSERT_FACULITY_INTAKE", objParams, true);
                        if (ret != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.insertFacuiltyIntake-> " + ex.ToString());
                    }

                    return retStatus;
                }

                public DataSet GetStudentListForIdentityCard(int college_id, int admbatch, int degreeno, int branchno, int semesterno, int studentidtype)
                {
                    DataSet ds = null;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[]
                        {
                            new SqlParameter("@P_COLLEGE_ID" ,college_id),
                            new SqlParameter("@P_ADMBATCH" ,admbatch),
                            new SqlParameter("@P_DEGREENO", degreeno),
                            new SqlParameter("@P_BRANCHNO", branchno),
                            new SqlParameter("@P_SEMESTERNO", semesterno),
                            new SqlParameter("@P_STUDENTIDTYPE", studentidtype)
                        };
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STUDENT_SEARCH_IDENTITY_CARD", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.EmpCreateController.GetStudentListForIdentityCard-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetCourseExamRule(int sessionno, int schemeno, int semno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);

                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_SCHEMENO", schemeno);
                        objParams[2] = new SqlParameter("@P_SEMESTERNO", semno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_EXAM_RULES_COURSE_WISE", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetCourseExamRule-> " + ex.ToString());
                    }

                    return ds;
                }

                public DataSet GetStudentCount(int admno, int degreeno, int branchno, int semester, int idtype)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_ADMBATCHNO", admno);
                        objParams[1] = new SqlParameter("@P_DEGREENO", degreeno);
                        objParams[2] = new SqlParameter("@P_BRANCHNO", branchno);
                        objParams[3] = new SqlParameter("@P_SEMESTERNO", semester);
                        objParams[4] = new SqlParameter("@P_IDTYPE", idtype);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_STUDENT_GET_STUDENT_COUNT", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentRegistrationController.GetStudentCount-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetStudentPhotoAndSign(int admno, int degreeno, int branchno, int semester, int idtype, int extractBy, int College_id)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[7];
                        objParams[0] = new SqlParameter("@P_ADMBATCHNO", admno);
                        objParams[1] = new SqlParameter("@P_DEGREENO", degreeno);
                        objParams[2] = new SqlParameter("@P_BRANCHNO", branchno);
                        objParams[3] = new SqlParameter("@P_SEMESTERNO", semester);
                        objParams[4] = new SqlParameter("@P_IDTYPE", idtype);
                        objParams[5] = new SqlParameter("@P_EXTRACT_BY", extractBy);
                        objParams[6] = new SqlParameter("@P_COLLEGE_ID", College_id);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_STUDENT_GET_STUDENT_PHOTO_AND_SIGN", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentRegistrationController.GetStudentPhotoAndSign-> " + ex.ToString());
                    }
                    return ds;
                }

                #region "Achievement Master"

                public int AddAchievementMaster(string AchievementName)
                {
                    int status = -99;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] sqlParams = new SqlParameter[]
                        {
                            new SqlParameter("@P_ACHIEVEMENT_NAME", AchievementName),
                            new SqlParameter("@P_OUTPUT", status)
                        };
                        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

                        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_ACHIEVEMENT_MASTER_INSERT", sqlParams, true);
                        status = (Int32)obj;
                    }
                    catch (Exception ex)
                    {
                        status = -99;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.AddAchievementMaster() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return status;
                }

                public int UpdateAchievementMaster(int AchievementNo, string AchievementName)
                {
                    int status = -99;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] sqlParams = new SqlParameter[]
                            {
                                 new SqlParameter("@P_ACHIEVEMENT_NO", AchievementNo),
                                 new SqlParameter("@P_ACHIEVEMENT_NAME", AchievementName),

                                 new SqlParameter("@P_OUTPUT",status)
                            };
                        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

                        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_ACHIEVEMENT_MASTER_UPDATE", sqlParams, true);
                        status = (Int32)obj;
                    }
                    catch (Exception ex)
                    {
                        status = -99;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.UpdateAchievementMaster() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return status;
                }

                public DataSet GetAllAchievement()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[0];

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_ACHIEVEMENT_MASTER_GET_ALL", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetAllAchievement() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }

                public SqlDataReader GetAchievementNo(int AchievementNo)
                {
                    SqlDataReader dr = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[] { new SqlParameter("@P_OUTPUT", AchievementNo) };

                        dr = objSQLHelper.ExecuteReaderSP("PKG_ACD_ACHIEVEMENT_MASTER_GET_BY_ACHIEVEMENT_NO", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetAchievementNo() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return dr;
                }

                #endregion "Achievement Master"

                /// <summary>
                /// Added by Neha Baranwal (02Oct19)
                /// </summary>
                /// <param name="Participation Level Master"></param>
                /// <returns></returns>

                #region "Participation Level Master"

                public int AddParticipationLevelMaster(string participationName)
                {
                    int status = -99;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] sqlParams = new SqlParameter[]
                        {
                            new SqlParameter("@P_PARTICIPATION_LEVEL", participationName),

                            new SqlParameter("@P_OUTPUT", status)
                        };
                        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

                        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_PARTICIPATION_LEVEL_MASTER_INSERT", sqlParams, true);
                        status = (Int32)obj;
                    }
                    catch (Exception ex)
                    {
                        status = -99;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.AddParticipationLevelMaster() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return status;
                }

                public int UpdateParticipationLevelMaster(int participationNo, string participationName)
                {
                    int status = -99;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] sqlParams = new SqlParameter[]
                            {
                                 new SqlParameter("@P_PARTICIPATION_NO", participationNo),
                                 new SqlParameter("@P_PARTICIPATION_LEVEL", participationName),

                                 new SqlParameter("@P_OUTPUT",status)
                            };
                        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

                        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_PARTICIPATION_LEVEL_MASTER_UPDATE", sqlParams, true);
                        status = (Int32)obj;
                    }
                    catch (Exception ex)
                    {
                        status = -99;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.UpdateParticipationLevelMaster() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return status;
                }

                public DataSet GetAllParticipationLevel()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[0];

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_PARTICIPATION_LEVEL_MASTER_GET_ALL", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetAllParticipationLevel() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }

                public SqlDataReader GetParticipationNo(int participationNo)
                {
                    SqlDataReader dr = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[] { new SqlParameter("@P_OUTPUT", participationNo) };

                        dr = objSQLHelper.ExecuteReaderSP("PKG_ACD_PARTICIPATION_LEVEL_MASTER_GET_BY_PARTICIPATION_NO", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetParticipationNo() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return dr;
                }

                #endregion "Participation Level Master"

                #region "Sports Details"

                public int AddSportsDetails(Student objStudent)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        //Add New Course
                        objParams = new SqlParameter[11];
                        objParams[0] = new SqlParameter("@P_SPORTS_TYPE", objStudent.sportsType);
                        objParams[1] = new SqlParameter("@P_SPORTS_NAME", objStudent.sportsName);
                        objParams[2] = new SqlParameter("@P_SPORTS_DATE", objStudent.sportsDate);
                        objParams[3] = new SqlParameter("@P_SPORTS_VENUE", objStudent.sportsVenue);

                        objParams[4] = new SqlParameter("@P_PARTICIPATION_NO", objStudent.participationNo);
                        objParams[5] = new SqlParameter("@P_ACHIEVEMENT_NO", objStudent.achievementNo);
                        objParams[6] = new SqlParameter("@P_IDNO", objStudent.idno);
                        objParams[7] = new SqlParameter("@P_GAME_NO", objStudent.gameNo);
                        objParams[8] = (objStudent.sportsFilename != "") ? new SqlParameter("@P_SPORTS_FILE_NAME", objStudent.sportsFilename) : new SqlParameter("@P_SPORTS_FILE_NAME", DBNull.Value);
                        objParams[9] = (objStudent.sportsFilepath != "") ? new SqlParameter("@P_SPORTS_FILE_PATH", objStudent.sportsFilepath) : new SqlParameter("@P_SPORTS_FILE_PATH", DBNull.Value);

                        objParams[10] = new SqlParameter("@P_SPORTS_NO", SqlDbType.Int);
                        objParams[10].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_SPORTS_SP_INS_SPORTS", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.AddSportsDetails-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int UpdateSportsDetails(Student objStudent)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        //Edit Course
                        objParams = new SqlParameter[11];

                        objParams[0] = new SqlParameter("@P_SPORTS_TYPE", objStudent.sportsType);
                        objParams[1] = new SqlParameter("@P_SPORTS_NAME", objStudent.sportsName);
                        objParams[2] = new SqlParameter("@P_SPORTS_DATE", objStudent.sportsDate);
                        objParams[3] = new SqlParameter("@P_SPORTS_VENUE", objStudent.sportsVenue);

                        objParams[4] = new SqlParameter("@P_PARTICIPATION_NO", objStudent.participationNo);
                        objParams[5] = new SqlParameter("@P_ACHIEVEMENT_NO", objStudent.achievementNo);
                        objParams[6] = new SqlParameter("@P_IDNO", objStudent.idno);
                        objParams[7] = new SqlParameter("@P_GAME_NO", objStudent.gameNo);
                        objParams[8] = (objStudent.sportsFilename != "") ? new SqlParameter("@P_SPORTS_FILE_NAME", objStudent.sportsFilename) : new SqlParameter("@P_SPORTS_FILE_NAME", DBNull.Value);
                        objParams[9] = (objStudent.sportsFilepath != "") ? new SqlParameter("@P_SPORTS_FILE_PATH", objStudent.sportsFilepath) : new SqlParameter("@P_SPORTS_FILE_PATH", DBNull.Value);

                        objParams[10] = new SqlParameter("@P_SPORTS_NO", objStudent.sportsNo);
                        objParams[10].Direction = ParameterDirection.InputOutput;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_SPORTS_SP_UPD_SPORTS", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.UpdateSportsDetails -> " + ex.ToString());
                    }

                    return retStatus;
                }

                public SqlDataReader GetSportsDetails(int sportsNo)
                {
                    SqlDataReader dr = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_SPORTS_NO", sportsNo);

                        dr = objSQLHelper.ExecuteReaderSP("PKG_ACAD_GET_STUDENT_SPORTS_DETAILS_BY_NO", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentController.GetSportsDetails-> " + ex.ToString());
                    }

                    return dr;
                }

                public DataSet GetCertificatesBySportsNo(int sportsNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_SPORTS_NO", sportsNo);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_GET_CERTIFICATES_BY_SPORTS_NO", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetCoursesBySchemeNo-> " + ex.ToString());
                    }

                    return ds;
                }

                public DataSet GetAllSportsDetails(int idno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_SPORTS_DETAILS_GET_ALL", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetAllSportsDetails() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }

                public int DeleteSportsDocs(int sportsdocsno)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[]
                        {
                        new SqlParameter("@P_SPORTS_DOCS_NO", sportsdocsno),
                        new SqlParameter("@P_OUT", SqlDbType.Int)
                        };

                        objParams[objParams.Length - 1].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_DELETE_SPORTS_DOCS_BY_NO", objParams, true);

                        if (ret != null && ret.ToString() != "-99")

                            retStatus = Convert.ToInt32(CustomStatus.RecordDeleted);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.AllotStudentBranch-> " + ex.ToString());
                    }
                    return retStatus;
                }

                #endregion "Sports Details"

                #region "Game Master"

                public int AddGameMaster(string GameName)
                {
                    int status = -99;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] sqlParams = new SqlParameter[]
                        {
                            new SqlParameter("@P_GAME_NAME", GameName),
                            new SqlParameter("@P_OUTPUT", status)
                        };
                        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

                        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_GAME_MASTER_INSERT", sqlParams, true);
                        status = (Int32)obj;
                    }
                    catch (Exception ex)
                    {
                        status = -99;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.AddGameMaster() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return status;
                }

                public int UpdateGameMaster(int GameNo, string GameName)
                {
                    int status = -99;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] sqlParams = new SqlParameter[]
                            {
                                 new SqlParameter("@P_GAME_NO", GameNo),
                                 new SqlParameter("@P_GAME_NAME", GameName),

                                 new SqlParameter("@P_OUTPUT",status)
                            };
                        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

                        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_GAME_MASTER_UPDATE", sqlParams, true);
                        status = (Int32)obj;
                    }
                    catch (Exception ex)
                    {
                        status = -99;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.UpdateGameMaster() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return status;
                }

                public DataSet GetAllGame()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[0];

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GAME_MASTER_GET_ALL", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetAllGame() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }

                public SqlDataReader GetGameNo(int GameNo)
                {
                    SqlDataReader dr = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[] { new SqlParameter("@P_OUTPUT", GameNo) };

                        dr = objSQLHelper.ExecuteReaderSP("PKG_ACD_GAME_MASTER_GET_BY_GAME_NO", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetGameNo() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return dr;
                }

                public DataSet GetStudentDataforuploadextermark(int sessionno, int collegeid, int degreeno, int branchno, int schemeno, int semesterno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSqlHlper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_COLLEGEID", collegeid);
                        objParams[2] = new SqlParameter("@P_DEGREENO", degreeno);
                        objParams[3] = new SqlParameter("@P_BRANCHNO", branchno);
                        objParams[4] = new SqlParameter("@P_SCHEMENO", schemeno);
                        objParams[5] = new SqlParameter("@P_SEMESTERNO", semesterno);
                        ds = objSqlHlper.ExecuteDataSetSP("PKG_ACD_GET_STUDENT_DATA_FOR_UPLOAD_MARKS", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentDataforuploadextermark-> " + ex.ToString());
                    }
                    return ds;
                }

                #endregion "Game Master"

                #region "NCC Details"

                public int AddNCCDetails(Student objStudent)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[11];
                        objParams[0] = new SqlParameter("@P_NCC_TYPE_NO", objStudent.nccTypeNo);
                        objParams[1] = new SqlParameter("@P_NCC_RATION_NO", objStudent.nccRationNo);
                        objParams[2] = new SqlParameter("@P_CAMP_NAME", objStudent.campName);
                        objParams[3] = new SqlParameter("@P_CAMP_LOCATION", objStudent.Location);

                        objParams[4] = new SqlParameter("@P_CAMP_DURATION", objStudent.duration);
                        objParams[5] = new SqlParameter("@P_IDNO", objStudent.idno);
                        objParams[6] = new SqlParameter("@P_CAMP_FROM_DATE", objStudent.campFromDate);
                        objParams[7] = new SqlParameter("@P_CAMP_TO_DATE", objStudent.campToDate);
                        objParams[8] = (objStudent.nccFilename != "") ? new SqlParameter("@P_NCC_FILE_NAME", objStudent.nccFilename) : new SqlParameter("@P_NCC_FILE_NAME", DBNull.Value);
                        objParams[9] = (objStudent.nccFilepath != "") ? new SqlParameter("@P_NCC_FILE_PATH", objStudent.nccFilepath) : new SqlParameter("@P_NCC_FILE_PATH", DBNull.Value);

                        objParams[10] = new SqlParameter("@P_NCC_NO", SqlDbType.Int);
                        objParams[10].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_NCC_SP_INS_NCC", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.AddNCCDetails-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int UpdateNCCDetails(Student objStudent)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[11];
                        objParams[0] = new SqlParameter("@P_NCC_TYPE_NO", objStudent.nccTypeNo);
                        objParams[1] = new SqlParameter("@P_NCC_RATION_NO", objStudent.nccRationNo);
                        objParams[2] = new SqlParameter("@P_CAMP_NAME", objStudent.campName);
                        objParams[3] = new SqlParameter("@P_CAMP_LOCATION", objStudent.Location);

                        objParams[4] = new SqlParameter("@P_CAMP_DURATION", objStudent.duration);
                        objParams[5] = new SqlParameter("@P_IDNO", objStudent.idno);
                        objParams[6] = new SqlParameter("@P_CAMP_FROM_DATE", objStudent.campFromDate);
                        objParams[7] = new SqlParameter("@P_CAMP_TO_DATE", objStudent.campToDate);
                        objParams[8] = (objStudent.nccFilename != "") ? new SqlParameter("@P_NCC_FILE_NAME", objStudent.nccFilename) : new SqlParameter("@P_NCC_FILE_NAME", DBNull.Value);
                        objParams[9] = (objStudent.nccFilepath != "") ? new SqlParameter("@P_NCC_FILE_PATH", objStudent.nccFilepath) : new SqlParameter("@P_NCC_FILE_PATH", DBNull.Value);

                        objParams[10] = new SqlParameter("@P_NCC_NO", objStudent.nccNo);
                        objParams[10].Direction = ParameterDirection.InputOutput;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_NCC_SP_UPD_NCC", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.UpdateNCCDetails -> " + ex.ToString());
                    }

                    return retStatus;
                }

                public SqlDataReader GetNCCDetails(int nccNo)
                {
                    SqlDataReader dr = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_NCC_NO", nccNo);

                        dr = objSQLHelper.ExecuteReaderSP("PKG_ACAD_GET_STUDENT_NCC_DETAILS_BY_NO", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentController.GetNCCDetails-> " + ex.ToString());
                    }

                    return dr;
                }

                public DataSet GetCertificatesByNCCNo(int nccNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_NCC_NO", nccNo);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_GET_CERTIFICATES_BY_NCC_NO", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetCertificatesByNCCNo-> " + ex.ToString());
                    }

                    return ds;
                }

                public DataSet GetAllNCCDetails(int idno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_NCC_DETAILS_GET_ALL", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetAllNCCDetails() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }

                public int DeleteNCCDocs(int nccdocsno)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[]
                        {
                        new SqlParameter("@P_NCC_DOCS_NO", nccdocsno),
                        new SqlParameter("@P_OUT", SqlDbType.Int)
                        };

                        objParams[objParams.Length - 1].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_DELETE_NCC_DOCS_BY_NO", objParams, true);

                        if (ret != null && ret.ToString() != "-99")

                            retStatus = Convert.ToInt32(CustomStatus.RecordDeleted);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.DeleteNCCDocs-> " + ex.ToString());
                    }
                    return retStatus;
                }

                #endregion "NCC Details"

                public DataSet GetStudentExamAdmitCard(int sessionno, int degreeno, int branchno, int semesterno, int idno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_DEGREENO", degreeno);
                        objParams[2] = new SqlParameter("@P_BRANCHNO", branchno);
                        objParams[3] = new SqlParameter("@P_SEMESTERNO", semesterno);
                        objParams[4] = new SqlParameter("@P_IDNO", idno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_STUD_ADMIT_CARD", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.EmpCreateController.GetStudentListForIdentityCard-> " + ex.ToString());
                    }
                    return ds;
                }

                //student home page

                public DataSet RetrieveStudentTimeTableDetails(int sessionno, int schemeno, int semesterno, int idno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_SCHEMENO", schemeno);
                        objParams[2] = new SqlParameter("@P_SEMESTERNO", semesterno);
                        objParams[3] = new SqlParameter("@P_IDNO", idno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_SP_TIMETABLE", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.RetrieveStudentCurrentRegDetails-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet RetrieveStudentTaskDetails(int idno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_ACTIVE_TASK_LIST", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.RetrieveStudentCurrentRegDetails-> " + ex.ToString());
                    }
                    return ds;
                }

                public int AddLinkStatus(int uano, int alno)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_UANO", uano);
                        objParams[1] = new SqlParameter("@P_ALNO", alno);
                        objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[2].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_ADD_LINK_STATUS", objParams, true);

                        if (Convert.ToInt32(ret) == 1)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        else if (Convert.ToInt32(ret) == 2)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.Insert_Update_StudentDocuments-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetAnnouncementCount(int idno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ITLE_ANNOUNCMENT_COUNT_DASHBOARD", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.RetrieveStudentCurrentRegDetails-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetAssignmentCount(int idno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ITLE_ASSIGNMENT_COUNT_DASHBOARD", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.RetrieveStudentCurrentRegDetails-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetAnnouncementAssignmentCount(int idno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ITLE_ASSIGN_ANNOUNCE_COUNT_DASHBOARD", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.RetrieveStudentCurrentRegDetails-> " + ex.ToString());
                    }
                    return ds;
                }

                //end

                #region "Photo Copy"

                //Added by Neha Baranwal on date 08/01/2020
                public DataSet GetCourseFor_RevalOrPhotoCopy(int idno, int sessionno, int applytype)
                {
                    DataSet ds = null;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        objParams[1] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[2] = new SqlParameter("@P_OPERATION_FLAG ", applytype);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_COURSE_FOR_REVAL_OR_PHOTOCOPY", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetCourseFor_RevalOrPhotoCopy->" + ex.ToString());
                    }
                    return ds;
                }

                public int UpdateChallanDetails(int idno, int sessionno, int applytype)
                {
                    int status = -99;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] sqlParams = new SqlParameter[]
                            {
                                 new SqlParameter("@P_IDNO", idno),
                                 new SqlParameter("@P_SESSIONNO", sessionno),
                                 new SqlParameter("@P_OPERATION_FLAG", applytype),
                                 new SqlParameter("@P_OUTPUT",status)
                            };
                        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

                        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_UPDATE_CHALLAN_DETAILS", sqlParams, true);
                        status = (Int32)obj;
                    }
                    catch (Exception ex)
                    {
                        status = -99;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.UpdateChallanDetails() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return status;
                }

                #endregion "Photo Copy"

                public DataSet RetrieveFacultyTimeTableDetails(int sessionno, int schemeno, int semesterno, int uano)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_SCHEMENO", schemeno);
                        objParams[2] = new SqlParameter("@P_SEMESTERNO", semesterno);
                        objParams[3] = new SqlParameter("@P_UANO", uano);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_SP_TIMETABLE_FACULTY", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.RetrieveStudentCurrentRegDetails-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet RetrieveFacultyTodaysTimeTable(int sessionno, int schemeno, int semesterno, int uano)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_SCHEMENO", schemeno);
                        objParams[2] = new SqlParameter("@P_SEMESTERNO", semesterno);
                        objParams[3] = new SqlParameter("@P_UANO", uano);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_SP_TIMETABLE_FACULTY_DAYWISE_TIMESLOT", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.RetrieveStudentCurrentRegDetails-> " + ex.ToString());
                    }
                    return ds;
                }

                // Added by Abhinay Lad Start's Here

                // ----  jointly singly count for annexure a   -- added by dipali on 10222019
                public DataSet GetJointlySinglyCount(int uano)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);

                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_SUPERVISIORNO", uano);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_PHD_JOINTLY_SINGLY", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentsForScheme-> " + ex.ToString());
                    }

                    return ds;
                }

                public string RejectSupervisorStatus(Student objStudent, string Remark)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        //Update drc
                        objParams = new SqlParameter[5];

                        objParams[0] = new SqlParameter("@P_IDNO", objStudent.IdNo);
                        objParams[1] = new SqlParameter("@P_PHDSUPERVISORNO", objStudent.PhdSupervisorNo);
                        objParams[2] = new SqlParameter("@P_PHDCOSUPERVISORNO1", objStudent.PhdCoSupervisorNo1);
                        objParams[3] = new SqlParameter("@P_REMARK", Remark);
                        objParams[4] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[4].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_PHD_SUPERVISOR_REJECT", objParams, true);

                        retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        return ret.ToString();
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.RejectSupervisorStatus-> " + ex.ToString());
                    }
                }

                //Update Dean status
                public string UpdateDeanStatus(Student objStudent)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        //Update drc
                        objParams = new SqlParameter[2];

                        objParams[0] = new SqlParameter("@P_IDNO", objStudent.IdNo);
                        objParams[1] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[1].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_PHD_DEAN_UPDATE", objParams, true);

                        retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        return ret.ToString();
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.UpdateStudent-> " + ex.ToString());
                    }
                }

                //Update Supervisor status and Outside member
                public string UpdateDGCSupervisorOutside(Student objStudent, string outsidmem, string dgcstatus, int outsidmemno)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    //int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        //Update Student
                        //objParams = new SqlParameter[15];
                        objParams = new SqlParameter[18];
                        //First Add Student Parameter
                        objParams[0] = new SqlParameter("@P_IDNO", objStudent.IdNo);
                        objParams[1] = new SqlParameter("@P_SUPERVISORNO", objStudent.SupervisorNo);
                        objParams[2] = new SqlParameter("@P_SUPERVISORMEMBERNO", objStudent.SupervisormemberNo);
                        objParams[3] = new SqlParameter("@P_JOINTSUPERVISORNO", objStudent.JoinsupervisorNo);
                        objParams[4] = new SqlParameter("@P_JOINSUPERVISORMEMBERNO", objStudent.JoinsupervisormemberNo);
                        objParams[5] = new SqlParameter("@P_INSTITUTEFACULTYNO", objStudent.InstitutefacultyNo);
                        objParams[6] = new SqlParameter("@P_INSTITUTEFACMEMBERNO", objStudent.InstitutefacmemberNo);
                        objParams[7] = new SqlParameter("@P_DRCNO", objStudent.DrcNo);
                        objParams[8] = new SqlParameter("@P_DRCMEMBERNO", objStudent.DrcmemberNo);
                        objParams[9] = new SqlParameter("@P_DRCCHNO", objStudent.DrcChairNo);
                        objParams[10] = new SqlParameter("@P_DRCCHMEMBERNO", objStudent.DrcChairmemberNo);
                        objParams[11] = new SqlParameter("@P_COLLEGE_CODE", objStudent.CollegeCode);
                        objParams[12] = new SqlParameter("@P_OUTSIDMEM", outsidmem);
                        objParams[13] = new SqlParameter("@P_DGCSTATUS", dgcstatus);
                        //ADDED FOR EXTRA SUPERVISOR
                        objParams[14] = new SqlParameter("@P_SECONDJOINTSUPERVISORNO", objStudent.Secondjoinsupervisorno);
                        objParams[15] = new SqlParameter("@P_SECONDJOINSUPERVISORMEMBERNO", objStudent.Secondjoinsupervisormemberno);
                        objParams[16] = new SqlParameter("@P_OUTSIDMEMNO", outsidmemno);
                        objParams[17] = new SqlParameter("@P_OUT", SqlDbType.Int);

                        objParams[17].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PHD_SUPERVISOR_SP_UPD_DGCMEMBER_OUTMEM", objParams, true);

                        if (objStudent.LastQualifiedExams != null)
                        {
                            foreach (QualifiedExam qualExam in objStudent.LastQualifiedExams)
                            {
                                this.UpdateLastQualExams(qualExam);
                            }
                        }
                        retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        return ret.ToString();
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.UpdateDGCSupervisorOutside-> " + ex.ToString());
                    }
                }

                //Update Supervisor status and Authority to the DGC member status
                public string UpdateDGCSupervisor(Student objStudent, string noofdgc)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    //int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        //Update Student
                        objParams = new SqlParameter[16];
                        //First Add Student Parameter
                        objParams[0] = new SqlParameter("@P_IDNO", objStudent.IdNo);
                        objParams[1] = new SqlParameter("@P_SUPERVISORNO", objStudent.SupervisorNo);
                        objParams[2] = new SqlParameter("@P_SUPERVISORMEMBERNO", objStudent.SupervisormemberNo);
                        objParams[3] = new SqlParameter("@P_JOINTSUPERVISORNO", objStudent.JoinsupervisorNo);
                        objParams[4] = new SqlParameter("@P_JOINSUPERVISORMEMBERNO", objStudent.JoinsupervisormemberNo);
                        objParams[5] = new SqlParameter("@P_INSTITUTEFACULTYNO", objStudent.InstitutefacultyNo);
                        objParams[6] = new SqlParameter("@P_INSTITUTEFACMEMBERNO", objStudent.InstitutefacmemberNo);
                        objParams[7] = new SqlParameter("@P_DRCNO", objStudent.DrcNo);
                        objParams[8] = new SqlParameter("@P_DRCMEMBERNO", objStudent.DrcmemberNo);
                        objParams[9] = new SqlParameter("@P_DRCCHNO", objStudent.DrcChairNo);
                        objParams[10] = new SqlParameter("@P_DRCCHMEMBERNO", objStudent.DrcChairmemberNo);
                        objParams[11] = new SqlParameter("@P_COLLEGE_CODE", objStudent.CollegeCode);
                        objParams[12] = new SqlParameter("@P_NOOFDGC", noofdgc);
                        objParams[13] = new SqlParameter("@P_SECONDJOINTSUPERVISORNO", objStudent.Secondjoinsupervisorno);
                        objParams[14] = new SqlParameter("@P_SECONDJOINSUPERVISORMEMBERNO", objStudent.Secondjoinsupervisormemberno);
                        objParams[15] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[15].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PHD_SUPERVISOR_SP_UPD_DGCMEMBER", objParams, true);

                        if (objStudent.LastQualifiedExams != null)
                        {
                            foreach (QualifiedExam qualExam in objStudent.LastQualifiedExams)
                            {
                                this.UpdateLastQualExams(qualExam);
                            }
                        }
                        retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        return ret.ToString();
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.UpdateStudent-> " + ex.ToString());
                    }
                }

                //Update Drc status
                public string UpdateDRCStatus(Student objStudent, string Status)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        //Update drc
                        objParams = new SqlParameter[3];

                        objParams[0] = new SqlParameter("@P_IDNO", objStudent.IdNo);
                        objParams[1] = new SqlParameter("@P_STATUS", Status);
                        objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[2].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_PHD_DRC_UPDATE", objParams, true);

                        retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        return ret.ToString();
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.UpdateStudent-> " + ex.ToString());
                    }
                }

                public DataSet GetFeeDetailCount(int sessionno, int admbatch, int degreeno, string reciepttype)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_ADMBATCH", admbatch);
                        objParams[2] = new SqlParameter("@P_DEGREENO", degreeno);
                        objParams[3] = new SqlParameter("@P_RECIEPTCODE", reciepttype);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_GET_FEES_COUNT", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.RetrieveStudentCurrentRegDetails-> " + ex.ToString());
                    }
                    return ds;
                }

                //Get Instalment Details:

                public DataSet GetStudentInfoInstalmentListByEnrollNo(int SessionNo, string ENROLLNO, string Reciept_Code)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objDataAccess = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_SESSIONNO ", SessionNo),
                    new SqlParameter("@P_ENROLLNO ", ENROLLNO),
                    new SqlParameter("@P_RECIEPT_CODE ", Reciept_Code)
                };
                        ds = objDataAccess.ExecuteDataSetSP("PKG_STUDENT_INSTALMENT_LIST_GET_STUDENT_INFO", sqlParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.GetStudentInfoDocumentListByEnrollNo() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }

                //Insert Details:

                public int InsertStudentInstalment(int idno, int Semesterno, DateTime DueDate, decimal Amount, decimal TotalAmount, int InstalmentNo, string Remark, int totalcount, int usertype, string recieptcode, int dmno, int recon, int userno, int Collegecode, string ipaddress)
                {
                    int status = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objDataAccess = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] sqlParams = new SqlParameter[]
                     {
                           new SqlParameter("@P_IDNO", idno),
                           new SqlParameter("@P_SEMESTERNO", Semesterno),
                           new SqlParameter("@P_DUE_DATE", DueDate),
                           new SqlParameter("@P_AMOUNT", Amount),
                           new SqlParameter("@P_TOTALAMOUNT", TotalAmount),
                           new SqlParameter("@P_NO_OF_INSTALL", InstalmentNo),
                           new SqlParameter("@P_REMARK_BY_AUTHORITY", Remark),
                           new SqlParameter("@P_TOTAL_INSTALMENT", totalcount),
                           new SqlParameter("@P_UA_TYPE", usertype),
                           new SqlParameter("@P_RECIEPT_CODE", recieptcode),
                           new SqlParameter("@P_DMNO", dmno),
                           new SqlParameter("@P_RECON", recon),
                           new SqlParameter("@P_UA_NO", userno),
                           new SqlParameter("@P_COLLEGE_CODE", Collegecode),
                           new SqlParameter("@P_IPADDRESS", ipaddress),
                           new SqlParameter("@P_OUT", SqlDbType.Int),
                       };
                        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;
                        status = (Int32)objDataAccess.ExecuteNonQuerySP("PKG_ACD_STUD_INSERT_INSTALMENT_AMOUNT", sqlParams, true);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.SessionActivityController.AddSessionActivity() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return status;
                }

                public int UpdateAEFChallanDetails(int idno, int sessionno, int semesterno)
                {
                    int status = -99;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] sqlParams = new SqlParameter[]
                            {
                                 new SqlParameter("@P_IDNO", idno),
                                 new SqlParameter("@P_SESSIONNO", sessionno),
                                 new SqlParameter("@P_SEMESTERNO", semesterno),
                                 new SqlParameter("@P_OUTPUT",status)
                            };
                        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

                        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_UPDATE_ARREAR_CHALLAN_DETAILS", sqlParams, true);
                        status = (Int32)obj;
                    }
                    catch (Exception ex)
                    {
                        status = -99;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.UpdateAEFChallanDetails() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return status;
                }

                #region Condonation Payment

                public DataSet GetStudentDetailsForCondonation(int idno)
                {
                    DataSet ds = null;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STUDENT_SP_RET_STUDENT_BYID_FOR_CONDONATION", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentDetails->" + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetStudentAllCondonationSubjects(int idno, int Sessionno, int Schemeno)
                {
                    DataSet ds = null;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        objParams[1] = new SqlParameter("@P_SESSIONNO", Sessionno);
                        objParams[2] = new SqlParameter("@P_SCHEMENO", Schemeno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_GET_ALL_CONDONATION_SUBJECTS", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentFailExamSubjects->" + ex.ToString());
                    }
                    return ds;
                }

                #endregion Condonation Payment

                public int InsertBatchAllotmentDetails(int Session, int idno, int semester, int branch, int batch, int course, int departmentIN, int departmentEX, int internal_ua, int external_ua, int scheme, int createdby)
                {
                    int status = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objDataAccess = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] sqlParams = new SqlParameter[]
                     {
                           new SqlParameter("@P_SESSION_NO",Session),
                           new SqlParameter("@P_IDNO",idno),
                           new SqlParameter("@P_SEMESTERNO",semester),
                           new SqlParameter("@P_BRANCHNO",branch),
                           new SqlParameter("@P_BATCHNO",batch),
                           new SqlParameter("@P_COURSENO",course),
                           new SqlParameter("@P_INT_DEPT_NO",departmentIN),
                           new SqlParameter("@P_INTERNAL_UA_NO",internal_ua),
                           new SqlParameter("@P_EX_DEPT_NO",departmentEX),
                           new SqlParameter("@P_EXTERNAL_UA_NO",external_ua),
                           new SqlParameter("@P_SCHEMENO",scheme),
                           new SqlParameter("@P_CREATED_BY",createdby),
                           new SqlParameter("@P_OUT", SqlDbType.Int),
                       };
                        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;
                        status = (Int32)objDataAccess.ExecuteNonQuerySP("PKG_ACD_STUD_INSERT_BATCH_ALLOTMENT", sqlParams, true);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.SessionActivityController.AddSessionActivity() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return status;
                }

                public DataSet GetStudentForBatchAllotment(int session, int collegeid, int degree, int branch, int scheme, int semester, int course)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objDataAccess = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] sqlParams = new SqlParameter[]
                {
                           new SqlParameter("@P_SESSIONNO",session),
                           new SqlParameter("@P_COLLEGE_ID",collegeid) ,
                           new SqlParameter("@P_DEGREENO",degree),
                           new SqlParameter("@P_BRANCHNO",branch),
                           new SqlParameter("@P_SCHEMENO",scheme),
                           new SqlParameter("@P_SEMESTERNO",semester),
                           new SqlParameter("@P_COURSENO",course),
                          // new SqlParameter("@P_SECTIONNO",section),
                           //new SqlParameter("@P_OUT", SqlDbType.Int),
                };
                        ds = objDataAccess.ExecuteDataSetSP("PKG_ACAD_GET_STUDENT_FOR_BATCH_ALLOTMENT", sqlParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.GetStudentInfoDocumentListByEnrollNo() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }

                public int UpdateBatchAllotmentDetails(Student_Acd objStudent)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[10];
                        objParams[0] = new SqlParameter("@P_STUDID", objStudent.StudId);
                        objParams[1] = new SqlParameter("@P_SESSIONNO", objStudent.SessionNo);
                        objParams[2] = new SqlParameter("@P_SCHEMENO", objStudent.SchemeNo);
                        objParams[3] = new SqlParameter("@P_SEMESTERNO", objStudent.SemesterNo);
                        objParams[4] = new SqlParameter("@P_COURSENO", objStudent.CourseNo);
                        objParams[5] = new SqlParameter("@P_SECTIONNO", objStudent.Sectionno);
                        objParams[6] = new SqlParameter("@P_SUBID", objStudent.ThBatchNo);
                        objParams[7] = new SqlParameter("@P_BATCHNO", objStudent.BatchNo);
                        objParams[8] = new SqlParameter("@P_ATTTYPE", objStudent.Pract_Theory);
                        objParams[9] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[9].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_STUD_UPDATE_BATCH_ALLOTMENT", objParams, false);
                        if (ret != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.UpdateStudent_BatchAllot-> " + ex.ToString());
                    }
                    return retStatus;
                }//END: External Practical Batch Allotment Details

                //Added By PRiTiSH S. on 01/04/2020
                public DataSet SearchStudent(string search, string category, int uatype, int uano)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_SEARCHSTRING", search);
                        objParams[1] = new SqlParameter("@P_SEARCH", category);
                        objParams[2] = new SqlParameter("@P_UATYPE", uatype);
                        objParams[3] = new SqlParameter("@P_UANO", uano);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_SEARCH_STUDENT", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.EmpCreateController.RetrieveEmpDetails-> " + ex.ToString());
                    }
                    return ds;
                }

                //HOME PAGE
                public DataSet RetrieveStudentTimeTableDetails(int sessionno, int schemeno, int semesterno, int idno, int sectionno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_SCHEMENO", schemeno);
                        objParams[2] = new SqlParameter("@P_SEMESTERNO", semesterno);
                        objParams[3] = new SqlParameter("@P_IDNO", idno);
                        objParams[4] = new SqlParameter("@P_SECTIONNO", sectionno);                 // Added on 07-04-2020

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_SP_STUDENT_TIMETABLE", objParams);          // Added on 06-04-2020
                        //  ds = objSQLHelper.ExecuteDataSetSP("PKG_SP_TIMETABLE", objParams);                // Commented on 06-04-2020
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.RetrieveStudentCurrentRegDetails-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet RetrieveFacultyTimeTableDetails(int sessionno, int schemeno, int semesterno, int uano, int sectionno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_SCHEMENO", schemeno);
                        objParams[2] = new SqlParameter("@P_SEMESTERNO", semesterno);
                        objParams[3] = new SqlParameter("@P_UANO", uano);
                        objParams[4] = new SqlParameter("@P_SECTIONNO", sectionno);                     // Added on 07-04-2020

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_SP_FACULTY_TIMETABLE", objParams);    // Added on 06-04-2020
                        // ds = objSQLHelper.ExecuteDataSetSP("PKG_SP_TIMETABLE_FACULTY", objParams);      // Commented on 06-04-2020

                        //  ds = objSQLHelper.ExecuteDataSetSP("PKG_SP_TIMETABLE_FACULTY_0104", objParams);  // Comment on 01-04-2020
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.RetrieveStudentCurrentRegDetails-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet RetrieveFacultyTodaysTimeTable(int sessionno, int schemeno, int semesterno, int uano, int sectionno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_SCHEMENO", schemeno);
                        objParams[2] = new SqlParameter("@P_SEMESTERNO", semesterno);
                        objParams[3] = new SqlParameter("@P_UANO", uano);
                        objParams[4] = new SqlParameter("@P_SECTIONNO", sectionno);         // Added on 07-04-2020

                        //ds = objSQLHelper.ExecuteDataSetSP("PKG_SP_TIMETABLE_FACULTY_DAYWISE_TIMESLOT", objParams);               // Commented on 06-04-2020
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_SP_TIMETABLE_FACULTY_DAYWISE", objParams);                 // Added on 06-04-2020
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.RetrieveStudentCurrentRegDetails-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet RetrieveStudentAttendanceDetailsForDashboard(int sessionno, int schemeno, int semesterno, int idno, int sectionno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_SCHEMENO", schemeno);
                        objParams[2] = new SqlParameter("@P_SEMESTERNO", semesterno);
                        objParams[3] = new SqlParameter("@P_IDNO", idno);
                        objParams[4] = new SqlParameter("@P_SECTIONNO", sectionno);                                     // Added on 07-04-2020

                        // ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_REPORT_STUDENT_ATTENDANCE", objParams);         // Commented on 07-04-2020
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_REPORT_STUDENT_ATTENDANCE_DASHBOARD", objParams);            // Added on 07-04-2020
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.RetrieveStudentCurrentRegDetails-> " + ex.ToString());
                    }
                    return ds;
                }

                // TO GET ACADEMIC ACTIVITY DETAILS
                public DataSet GetAcdActivityDetailsForPrincipalDashboard()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        ds = objSQLHelper.ExecuteDataSet("[dbo].[GET_ACADEMIC_ACTIVITY_DETAILS_DASHBOARD]");
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.RetrieveStudentCurrentRegDetails-> " + ex.ToString());
                    }
                    return ds;
                }

                // TO GET ADMISSION FEES DETAILS
                public DataSet GetAdmFeesDetailsForPrincipalDashboard()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        ds = objSQLHelper.ExecuteDataSet("[dbo].[GET_ADMISSION_FEES_DETAILS_DASHBOARD]");
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.RetrieveStudentCurrentRegDetails-> " + ex.ToString());
                    }
                    return ds;
                }

                // TO GET RESULT ANALYSIS DATA
                public DataSet GetResultAnalysisForPrincipalDashboard()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        ds = objSQLHelper.ExecuteDataSet("[dbo].[PKG_GET_RESULT_ANALYSIS_DASHBOARD]");
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.RetrieveStudentCurrentRegDetails-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetQuickAccessForStudentDashboard(int UserTypeId)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_usertype", UserTypeId);
                        ds = objSQLHelper.ExecuteDataSetSP("[dbo].[GET_QUICK_LINK_USERWISE]", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.RetrieveStudentCurrentRegDetails-> " + ex.ToString());
                    }
                    return ds;
                }

                //Added By PRiTiSH S. on 20/04/2020
                public DataSet StudentAdmRegister(int admbatch, string degreeno, string branchno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_ADMBATCH", admbatch);
                        objParams[1] = new SqlParameter("@P_DEGREENO", degreeno);
                        objParams[2] = new SqlParameter("@P_BRANCHNO", branchno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_GET_ADMISSION_REGISTER", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.StudentAdmRegister-> " + ex.ToString());
                    }

                    return ds;
                }

                //Added by Pankaj nakhale on date 27-04-2020
                public DataSet GetCourseFor_ReviewApply(int idno, int sessionno)
                {
                    DataSet ds = null;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        objParams[1] = new SqlParameter("@P_SESSIONNO", sessionno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_COURSE_FOR_REVIEW_APPLY", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetCourseFor_RevalOrPhotoCopy->" + ex.ToString());
                    }
                    return ds;
                }

                //Added by Pankaj nakhale on date 27-04-2020
                public DataSet GetCourseFor_ReviewAppliedCourse(int idno, int sessionno)
                {
                    DataSet ds = null;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        objParams[1] = new SqlParameter("@P_SESSIONNO", sessionno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_COURSE_FOR_REVIEW_APPLY_FOR_APPLIED", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetCourseFor_RevalOrPhotoCopy->" + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetCourseList_ReviewAppliedStudent(int idno, int sessionno)
                {
                    DataSet ds = null;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        objParams[1] = new SqlParameter("@P_SESSIONNO", sessionno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_COURSE_FOR_REVIEW_APPLIED_STUDENT", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetCourseFor_RevalOrPhotoCopy->" + ex.ToString());
                    }
                    return ds;
                }

                //used for approve and refund made by pankaj nakhale 06052020
                public int ApproveCourseReview(StudentRegist objSR, string DDNO, int Flag)
                {
                    int status = -99;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] sqlParams = new SqlParameter[]
                            {
                                 new SqlParameter("@P_IDNO", objSR.IDNO),
                                 new SqlParameter("@P_SEMESTERNO", objSR.SEMESTERNO),
                                 new SqlParameter("@P_COURSENO", objSR.COURSENO),
                                 new SqlParameter("@P_DDNO", DDNO),
                                 new SqlParameter("@P_Flag", Flag),
                                 new SqlParameter("@P_OUTPUT",status)
                            };
                        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

                        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_APPROVE_REVIEW_REGISTRATION", sqlParams, true);
                        status = (Int32)obj;
                    }
                    catch (Exception ex)
                    {
                        status = -99;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.UpdateChallanDetails() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return status;
                }

                #region Student Bulk Update

                public int UpdateStudentDataFields(string TITLENAME, string field, int idNo, string Tblname, int uano)
                {
                    int result = 0;
                    try
                    {
                        SQLHelper sQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] array = null;
                        if (sQLHelper.ExecuteNonQuerySP("PKG_STUDENT_DATA_FOR_UPDATE", new SqlParameter[5]
        {
            new SqlParameter("@P_TITLENAME", TITLENAME),
            new SqlParameter("@P_FIELD", field),
            new SqlParameter("@P_IDNO", idNo),
            new SqlParameter("@P_TBLNAME", Tblname),
            new SqlParameter("@P_UANO", uano)
        }, flag: false) != null)
                        {
                            result = Convert.ToInt32(CustomStatus.RecordUpdated);
                        }
                    }
                    catch (Exception ex)
                    {
                        result = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.UpdateStudentDataFields->" + ex.ToString());
                    }

                    return result;
                }

                public DataSet GetStudentListForUpdate(int degreeno, int branchno, int admbatch, int college_id, int campusno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSqlHlper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_DEGREENO", degreeno);
                        objParams[1] = new SqlParameter("@P_BRANCHNO", branchno);
                        objParams[2] = new SqlParameter("@P_ADMBATCHNO", admbatch);
                        objParams[3] = new SqlParameter("@P_COLLEGE_ID", college_id);
                        objParams[4] = new SqlParameter("@P_CAMPUSNO", campusno);

                        ds = objSqlHlper.ExecuteDataSetSP("PKG_GET_STUDENT_DATA_FOR_UPDATE", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentDataforupdate-> " + ex.ToString());
                    }
                    return ds;
                }


                public DataSet GetCopyCaseData(int sessionno)
                {
                    DataSet ds = null;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        //objParams[0] = new SqlParameter("@P_IDNO", idno);
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STUDENT_GET_COPY_CASE_DETAILS", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.EmpCreateController.GetCopyCaseData-> " + ex.ToString());
                    }
                    return ds;
                }

                /////////////////////////////////////////////////////////////////////////////////////////

                public DataSet GetCopyCaseCourseDetails(Student_Acd objStudent)
                {
                    DataSet ds = null;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_REGNO", objStudent.RegNo);
                        objParams[1] = new SqlParameter("@P_SEMESTERNO", objStudent.SemesterNo);
                        objParams[2] = new SqlParameter("@P_SESSIONNO", objStudent.SessionNo);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STUDENT_GET_COPY_SUBJECT_STUDENTWISE", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.EmpCreateController.GetCopyCaseData-> " + ex.ToString());
                    }
                    return ds;
                }

                ///////////////////////////////////////////////////////////////////////////////

                public int AddCopyCaseForExam(Student_Acd objStud, int Juniorname, int Seniorname)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[]
                        {
                             new SqlParameter("@P_SESSIONNO",objStud.SessionNo),
                             new SqlParameter("@P_IDNO",objStud.IdNo),
                             new SqlParameter("@P_SEATNO",objStud.Seatno),
                             new SqlParameter("@P_SEMESTERNO",objStud.SemesterNo),
                             new SqlParameter("@P_SCHEMENO",objStud.SchemeNo),
                             new SqlParameter("@P_DEGREENO",objStud.DegreeNo),
                             new SqlParameter("@P_SECTIONNO",objStud.Sectionno ),
                             new SqlParameter("@P_STATUS",objStud.Status),
                             new SqlParameter("@P_COURSENO",objStud.Coursenos),
                             new SqlParameter("@P_PUNISHMENTNO",objStud.PUNISHMENTNO),
                              new SqlParameter("@P_JUNIORNAME",Juniorname),
                             new SqlParameter("@P_SENIORNAME",Seniorname),
                             new SqlParameter("@P_UFMDETAIL",objStud.Ufmdetails),
                             new SqlParameter("@P_REMARKS",objStud.Remarks),
                             new SqlParameter("@P_IPADDRESS",objStud.Ipaddress),
                             new SqlParameter("@P_COLLEGECODE",objStud.COLLEGE_ID),
                             new SqlParameter("@P_UANO",objStud.UA_No),
                             new SqlParameter("@P_OUT",SqlDbType.Int)
                        };
                        objParams[objParams.Length - 1].Direction = ParameterDirection.Output;
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_STUDENT_PUNISHMENET_APPLIED_STUD_LIST", objParams, false) != null)

                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentController.AddCopyCase --> " + ex.ToString());
                    }
                    return retStatus;
                }

                #endregion Student Bulk Update

                //END

                public int InsertUpdateHigherStudies(int idno, string uniName, int countryno, int stateno, string ProgName, string FinacialAds, decimal EntraceMarks, int userno, string ipAddress)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[10];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        objParams[1] = new SqlParameter("@P_UniName", uniName);
                        objParams[2] = new SqlParameter("@P_COUNTRYNO", countryno);
                        objParams[3] = new SqlParameter("@P_STATENO", stateno);
                        objParams[4] = new SqlParameter("@P_ProgName", ProgName);
                        objParams[5] = new SqlParameter("@P_FinacialAds", FinacialAds);
                        objParams[6] = new SqlParameter("@P_MARKS", EntraceMarks);
                        objParams[7] = new SqlParameter("@P_UA_NO", userno);
                        objParams[8] = new SqlParameter("@P_IPADDRESS", ipAddress);
                        objParams[9] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[9].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_INS_HIGHER_STUDIES_DETAIL", objParams, false);
                        if (ret != null)
                            if (ret.ToString() != "-99")
                                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.InsertUpdateHigherStudies-> " + ex.ToString());
                    }
                    return retStatus;
                }

                #region Convocation Related

                //------ Convocation Ceromony attaind -----

                public DataSet GetConvocationCeremonyAttaind(string rollno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);

                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_ROLLNO", rollno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_CONVO_CEREMONY_ATTAIND", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.SessionController.GetDateList-> " + ex.ToString());
                    }
                    return ds;
                }

                public string UpdateConvocationAttaind(Student objStudent)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        //Update Student
                        objParams = new SqlParameter[4];
                        //First Add Student Parameter
                        objParams[0] = new SqlParameter("@P_ROLLNO", objStudent.RollNo);
                        objParams[1] = new SqlParameter("@P_ENROLLNO", objStudent.EnrollNo);
                        objParams[2] = new SqlParameter("@P_CEREMONY", objStudent.PhdStatus);

                        objParams[3] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                        objParams[3].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_UPDATE_CONVO_CEREMONY_ATTAIND", objParams, true);

                        retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        return ret.ToString();
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.UpdateStudent-> " + ex.ToString());
                    }
                }

                //-------------------- end -------------//

                //------------------ Convocation withheld  --------------
                public int InsertDegreeConvocationDataWithheld(DegreeComplition objDc)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_ROLLNO", objDc.Rollno);
                        objParams[1] = new SqlParameter("@P_ENROLLNO", objDc.EnrollNo);
                        objParams[2] = new SqlParameter("@P_SESSIONNO", objDc.SessionNo);

                        objParams[3] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[3].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_INSERT_DEGREE_CONVOCATION_STUDENT_WITHHELD", objParams, true);
                        if (ret.ToString() == "1")
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else if (ret.ToString() == "-99")
                        {
                            retStatus = Convert.ToInt32(CustomStatus.Error);
                        }
                        return retStatus;
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.InsertBlockStudentData-> " + ex.ToString());
                    }
                }

                //----convocation master--------------

                // for modify degree compltetion
                public DataSet GEtStudDegreeCompleteForConvocation(Student objStud)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSH = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParam = new SqlParameter[4];
                        objParam[0] = new SqlParameter("@P_SESSIONNO", objStud.SessionNo);
                        objParam[1] = new SqlParameter("@P_DEGREENO", objStud.DegreeNo);
                        objParam[2] = new SqlParameter("@P_BRANCHNO", objStud.BranchNo);
                        objParam[3] = new SqlParameter("@P_SCHEME", objStud.SchemeNo);
                        ds = objSH.ExecuteDataSetSP("PKG_ACD_DEGREE_COMPLETE_CONVOCATION", objParam);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.SessionController.GetDateList-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GEtStudDegreeConvocationWithheld(Student objStud)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSH = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParam = new SqlParameter[4];
                        objParam[0] = new SqlParameter("@P_SESSIONNO", objStud.SessionNo);
                        objParam[1] = new SqlParameter("@P_DEGREENO", objStud.DegreeNo);
                        objParam[2] = new SqlParameter("@P_BRANCHNO", objStud.BranchNo);
                        objParam[3] = new SqlParameter("@P_SCHEME", objStud.SchemeNo);
                        ds = objSH.ExecuteDataSetSP("PKG_ACD_DEGREE_COMPLETE_CONVOCATION_WITHELD", objParam);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.SessionController.GetDateList-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GEtStudDegreeCompleteForConvocationExcel(Student objStud)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSH = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParam = new SqlParameter[4];
                        objParam[0] = new SqlParameter("@P_SESSIONNO", objStud.SessionNo);
                        objParam[1] = new SqlParameter("@P_DEGREENO", objStud.DegreeNo);
                        objParam[2] = new SqlParameter("@P_BRANCHNO", objStud.BranchNo);
                        objParam[3] = new SqlParameter("@P_SCHEME", objStud.SchemeNo);
                        ds = objSH.ExecuteDataSetSP("PKG_ACD_DEGREE_COMPLETE_CONVOCATION_EXCEL", objParam);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.SessionController.GetDateList-> " + ex.ToString());
                    }
                    return ds;
                }

                public int InsertDegreeConvocationData(DegreeComplition objDc)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_ROLLNO", objDc.Rollno);
                        objParams[1] = new SqlParameter("@P_ENROLLNO", objDc.EnrollNo);
                        objParams[2] = new SqlParameter("@P_SESSIONNO", objDc.SessionNo);

                        objParams[3] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[3].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_INSERT_DEGREE_CONVOCATION_STUDENT", objParams, true);
                        if (ret.ToString() == "1")
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else if (ret.ToString() == "-99")
                        {
                            retStatus = Convert.ToInt32(CustomStatus.Error);
                        }
                        return retStatus;
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.InsertBlockStudentData-> " + ex.ToString());
                    }
                }

                //TO GET TASK DATA FOR FACULTY DASHBOARD
                public DataSet GetTaskForFacultyDashboard(int UserTypeId, int ua_no)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_usertype", UserTypeId);
                        objParams[1] = new SqlParameter("@P_userno", ua_no);
                        ds = objSQLHelper.ExecuteDataSetSP("[dbo].[GET_TASK_LINK_FACULTY_USERWISE]", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetTaskForFacultyDashboard-> " + ex.ToString());
                    }
                    return ds;
                }

                //TO GET TASK DATA FOR STUDENT DASHBOARD
                public DataSet GetTaskForStudentDashboard(int UserTypeId, int ua_no)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_usertype", UserTypeId);
                        objParams[1] = new SqlParameter("@P_userno", ua_no);
                        ds = objSQLHelper.ExecuteDataSetSP("[dbo].[GET_TASK_LINK_STUDENT_USERWISE]", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetTaskForStudentDashboard-> " + ex.ToString());
                    }
                    return ds;
                }

                //TO GET STUDENT ATTENDANCE PERCENTAGE FOR DASHBOARD
                public DataSet GetStudentAttPerDashboard(int ua_no)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_UANO", ua_no);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_STUDENT_ATTENDANCE_DASHBOARD", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentAttPerDashboard-> " + ex.ToString());
                    }
                    return ds;
                }

                //TO GET TODO LIST USERWISE
                public DataSet GetTODOListUserWiseDashboard(int ua_no)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_UA_NO", ua_no);
                        ds = objSQLHelper.ExecuteDataSetSP("GET_TODOLIST_DETAILS_USERWISE", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetTODOListUserWiseDashboard-> " + ex.ToString());
                    }
                    return ds;
                }

                //SAVE TODO LIST DATA
                public int SaveTodoListData(int UA_NO, string todolist)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_UA_NO", UA_NO);
                        objParams[1] = new SqlParameter("@P_TODO", todolist);
                        objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[2].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_INSERT_TODOLIST_USERWISE", objParams, true);
                        if (ret != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.SaveTodoListData-> " + ex.ToString());
                    }

                    return retStatus;
                }

                //UPDATE TODO LIST DATA
                public int UpdateTodoListData(int UA_NO, int srno)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_UA_NO", UA_NO);
                        objParams[1] = new SqlParameter("@P_SRNO", srno);
                        objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[2].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_UPDATE_TODOLIST_USERWISE", objParams, true);
                        if (ret != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.UpdateTodoListData-> " + ex.ToString());
                    }

                    return retStatus;
                }

                //higher studies by Naresh Beerla for Multiple Entry on 14062020

                public int InsertUpdateHigherStudies(DataTable Higher_studies_data, int idno, int userno, string ipAddress)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_TBL_ACD_INS_HIGHER_STUDIES_DETAIL", Higher_studies_data);
                        objParams[1] = new SqlParameter("@P_IDNO", idno);
                        objParams[2] = new SqlParameter("@P_UA_NO", userno);
                        objParams[3] = new SqlParameter("@P_IPADDRESS", ipAddress);
                        objParams[4] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[4].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_INS_HIGHER_STUDIES_DETAIL", objParams, false);
                        if (ret != null)
                            if (ret.ToString() != "-99")
                                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.InsertUpdateHigherStudies-> " + ex.ToString());
                    }
                    return retStatus;
                }

                //ends here higher studies for Multiple Entry by Naresh Beerla on 14062020
                //////////=====SHOW REPORT COLLECTION DATA=======////////////////

                public DataSet GetReportCollectionDataDashboard(int UserTypeId)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_usertype", UserTypeId);
                        ds = objSQLHelper.ExecuteDataSetSP("GET_REPORT_COLLECTION_USERWISE", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetReportCollectionDataDashboard-> " + ex.ToString());
                    }
                    return ds;
                }

                public int UpdateStudentname(int idno, string name, DateTime Dob, string SInitial, string Fname, string FInitial, string Sex, int Category) // New fields added on 07072020 by Naresh Beerla
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[]
                        {
                             new SqlParameter("@P_IDNO",idno),
                             new SqlParameter("@P_name",name),

                             new SqlParameter("@P_SDob",Dob),
                             new SqlParameter("@P_SInitial",SInitial),
                             new SqlParameter("@P_FName",Fname),
                             new SqlParameter("@P_FInitial",FInitial),
                             new SqlParameter("@P_SEX",Sex),
                             new SqlParameter("@P_CATEGORY",Category),

                             new SqlParameter("@P_OUT",SqlDbType.Int)
                        };
                        objParams[objParams.Length - 1].Direction = ParameterDirection.Output;
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_UPDATE_NAME", objParams, false) != null)

                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentController.AddCopyCase --> " + ex.ToString());
                    }
                    return retStatus;
                }

                //END

                #endregion Convocation Related

                #region Theory Improvement

                public DataSet GetStudentImprovementExamSubjects(int idno, int sessionno, int semesterno)
                {
                    DataSet ds = null;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        objParams[1] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[2] = new SqlParameter("@P_SEMESTERNO", semesterno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_GET_IMPROVEMENT_STUDENT_LIST", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentDetails->" + ex.ToString());
                    }
                    return ds;
                }

                public int RegisterImprovementSubject(StudentRegist objSR)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        //Add New eXAM Registered Subject Details
                        objParams = new SqlParameter[7];

                        objParams[0] = new SqlParameter("@P_IDNO", objSR.IDNO);
                        objParams[1] = new SqlParameter("@P_UANO", objSR.UA_NO);
                        objParams[2] = new SqlParameter("@P_SESSIONNO", objSR.SESSIONNO);
                        objParams[3] = new SqlParameter("@P_COURSENOS", objSR.COURSENOS);
                        objParams[4] = new SqlParameter("@P_SEMESTERNO", objSR.SEMESTERNO);
                        objParams[5] = new SqlParameter("@P_IPADDRESS", objSR.IPADDRESS);
                        objParams[6] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[6].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_REGIST_SP_INS_IMPROVEMENT_COURSE", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamRegistration-> " + ex.ToString());
                    }
                    return retStatus;
                }

                #endregion Theory Improvement

                public int GetUnpaidFeeDetails(int idno, int sessionno, int Reciept_Code, int uano)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        objParams[1] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[2] = new SqlParameter("@P_RECIEPT_NO", Reciept_Code);
                        objParams[3] = new SqlParameter("@P_UANO", uano);
                        objParams[4] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[4].Direction = ParameterDirection.Output;

                        object ret = Convert.ToInt32(objSQLHelper.ExecuteNonQuerySP("PKG_ACD_ONLINE_DCR_PAID", objParams, true));

                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordDeleted);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.DeleteCourseRegistered-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int GetUnpaidFeeDetails(int idno, int sessionno, int Reciept_Code)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        objParams[1] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[2] = new SqlParameter("@P_RECIEPT_NO", Reciept_Code);
                        objParams[3] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[3].Direction = ParameterDirection.Output;

                        object ret = Convert.ToInt32(objSQLHelper.ExecuteNonQuerySP("PKG_ACD_ONLINE_DCR_PAID", objParams, true));

                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordDeleted);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.DeleteCourseRegistered-> " + ex.ToString());
                    }
                    return retStatus;
                }

                /// <summary>
                /// Added by Satish T on 02/11/2020
                /// To download student document
                /// </summary>
                /// <param name="admno"></param>
                /// <param name="degreeno"></param>
                /// <param name="branchno"></param>
                /// <param name="semester"></param>
                /// <param name="idtype"></param>
                /// <returns></returns>
                public DataSet GetStudentDocument(int admno, int degreeno, int branchno, int semester, int idtype)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_ADMBATCHNO", admno);
                        objParams[1] = new SqlParameter("@P_DEGREENO", degreeno);
                        objParams[2] = new SqlParameter("@P_BRANCHNO", branchno);
                        objParams[3] = new SqlParameter("@P_SEMESTERNO", semester);
                        objParams[4] = new SqlParameter("@P_IDTYPE", idtype);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_STUDENT_GET_STUDENT_DOCUMENT", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentRegistrationController.GetStudentPhotoAndSign-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetStudentPhotoAndSign(int admno, int degreeno, int branchno, int semester, int idtype, int extractBy)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_ADMBATCHNO", admno);
                        objParams[1] = new SqlParameter("@P_DEGREENO", degreeno);
                        objParams[2] = new SqlParameter("@P_BRANCHNO", branchno);
                        objParams[3] = new SqlParameter("@P_SEMESTERNO", semester);
                        objParams[4] = new SqlParameter("@P_IDTYPE", idtype);
                        objParams[5] = new SqlParameter("@P_EXTRACT_BY", extractBy);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_STUDENT_GET_STUDENT_PHOTO_AND_SIGN", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentRegistrationController.GetStudentPhotoAndSign-> " + ex.ToString());
                    }
                    return ds;
                }

                // TO GET ACADEMIC ACTIVITY DETAILS
                public DataSet GetAcdActivityDetailsForPrincipalDashboard(string collegenos)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);

                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_COLLEGENO", collegenos);
                        ds = objSQLHelper.ExecuteDataSetSP("[dbo].[GET_ACADEMIC_ACTIVITY_DETAILS_DASHBOARD]", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.RetrieveStudentCurrentRegDetails-> " + ex.ToString());
                    }
                    return ds;
                }

                // TO GET RESULT ANALYSIS DATA
                public DataSet GetResultAnalysisForPrincipalDashboard(string collegenos)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);

                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_COLLEGENO", collegenos);
                        ds = objSQLHelper.ExecuteDataSetSP("[dbo].[PKG_GET_RESULT_ANALYSIS_DASHBOARD]", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.RetrieveStudentCurrentRegDetails-> " + ex.ToString());
                    }
                    return ds;
                }

                // get TOTAL User count...
                public DataSet GetTotalUserStudCount(string collegenos)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_COLLEGENO", collegenos);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_GET_TOTAL_USER_COUNT", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetBranchWiseMaleCount-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetDegreeAdmYearWiseStudList(int admYr)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        //objParams[0] = new SqlParameter("@P_DEGREENO", degreeNo);
                        objParams[0] = new SqlParameter("@P_AdmYear", admYr);
                        ds = objSQLHelper.ExecuteDataSetSP("GET_DEGREE_ADM_YEAR_WISE_STUD_LIST", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetAdmissionDataForPrincipalDashboardStudList-> " + ex.ToString());
                    }
                    return ds;
                }

                // get branch wise TOTAL count...
                public DataSet GetBranchWiseTotalStudCount(string collegenos)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_COLLEGENO", collegenos);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_GET_BRANCH_WISE_TOTAL_STUDENT_COUNT", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetBranchWiseMaleCount-> " + ex.ToString());
                    }
                    return ds;
                }

                // get branch wise Female count...
                public DataSet GetBranchWiseFemaleCount(string collegenos)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_COLLEGENO", collegenos);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_GET_BRANCH_WISE_FEMALE_COUNT", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetBranchWiseMaleCount-> " + ex.ToString());
                    }
                    return ds;
                }

                // get branch wise male count...
                public DataSet GetBranchWiseMaleCount(string collegenos)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_COLLEGENO", collegenos);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_GET_BRANCH_WISE_MALE_COUNT", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetBranchWiseMaleCount-> " + ex.ToString());
                    }
                    return ds;
                }

                // To Get Admission Batch wise Students count
                public DataSet GetAdmissionDataForPrincipalDashboard(string collegenos)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);

                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_COLLEGENO", collegenos);
                        ds = objSQLHelper.ExecuteDataSetSP("GET_ADMBATCH_WISE_STUDENT_COUNT_DASHBOARD", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.RetrieveStudentCurrentRegDetails-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetActiveUserCount(string collegenos)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_COLLEGENO", collegenos);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_TOTAL_USER_COUNT", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetActiveUserCount-> " + ex.ToString());
                    }
                    return ds;
                }

                //Added by Naresh on 22012021 to get the students list to update the master fields
                public DataSet GetStudentListForUpdate(int degreeno, int branchno, int schemeno, int semesterno, int admbatch, int college_id)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSqlHlper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[6];

                        objParams[0] = new SqlParameter("@P_DEGREENO", degreeno);
                        objParams[1] = new SqlParameter("@P_BRANCHNO", branchno);
                        objParams[2] = new SqlParameter("@P_SCHEMENO", schemeno);
                        objParams[3] = new SqlParameter("@P_SEMESTERNO", semesterno);
                        objParams[4] = new SqlParameter("@P_ADMBATCHNO", admbatch);
                        objParams[5] = new SqlParameter("@P_COLLEGE_ID", college_id);

                        ds = objSqlHlper.ExecuteDataSetSP("PKG_GET_STUDENT_DATA_FOR_UPDATE", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentDataforupdate-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetStudentListForUpdate(int degreeno, int admbatch, int college_id)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSqlHlper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_DEGREENO", degreeno);
                        objParams[1] = new SqlParameter("@P_ADMBATCHNO", admbatch);
                        objParams[2] = new SqlParameter("@P_COLLEGE_ID", college_id);

                        ds = objSqlHlper.ExecuteDataSetSP("PKG_GET_STUDENT_DATA_FOR_UPDATE", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentDataforupdate-> " + ex.ToString());
                    }
                    return ds;
                }

                // THIS METHOD IS USED TO RETRIEVE THE STUDENTS AS PER THE SESSION[COLLEGE_NOS] WHILE SEARCHING IN STUDENT DETAILS PAGE //ADDED BY NARESH BEERLA ON 28012021
                public DataSet RetrieveStudentDetailsForStudentDetails(string search, string category, string collegenos)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_SEARCHSTRING", search);
                        objParams[1] = new SqlParameter("@P_SEARCH", category);
                        objParams[2] = new SqlParameter("@P_COLLEGE_ID", collegenos);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STUDENT_SP_SEARCH_STUDENT_FOR_STUDENTDETAILS", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.EmpCreateController.RetrieveEmpDetails-> " + ex.ToString());
                    }
                    return ds;
                }

                // Added by Pritish S. on 02/02/2021
                public string AddNewStudent(Student objStudent, string studpanno, string fatherpanno, string password, int operation)
                {
                    string retStatus = string.Empty;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[21];
                        objParams[0] = new SqlParameter("@P_STUDNAME", objStudent.StudName);
                        objParams[1] = new SqlParameter("@P_EMAILID", objStudent.EmailID);
                        objParams[2] = new SqlParameter("@P_STUDENTMOBILE", objStudent.StudentMobile);
                        objParams[3] = new SqlParameter("@P_COLLEGE_ID", objStudent.College_ID);
                        objParams[4] = new SqlParameter("@P_DEGREENO", objStudent.DegreeNo);
                        objParams[5] = new SqlParameter("@P_BRANCHNO", objStudent.BranchNo);
                        objParams[6] = new SqlParameter("@P_ADMCATEGORYNO", objStudent.AdmCategoryNo);
                        objParams[7] = new SqlParameter("@P_ADMDATE", objStudent.AdmDate);
                        objParams[8] = new SqlParameter("@P_IDTYPENO", objStudent.IdType);
                        objParams[9] = new SqlParameter("@P_DOR", objStudent.DOR);
                        objParams[10] = new SqlParameter("@P_CLAIMID", objStudent.ClaimType);
                        objParams[11] = new SqlParameter("@P_YEAR", objStudent.Year);
                        objParams[12] = new SqlParameter("@P_SEX", objStudent.Sex);
                        objParams[13] = new SqlParameter("@P_ADMBATCH", objStudent.AdmBatch);
                        objParams[14] = new SqlParameter("@P_PTYPE", objStudent.PType);
                        objParams[15] = new SqlParameter("@P_SEMESTERNO", objStudent.SemesterNo);
                        objParams[16] = new SqlParameter("@P_STUDPANNO", studpanno);
                        objParams[17] = new SqlParameter("@P_FATHERPANNO", fatherpanno);
                        objParams[18] = new SqlParameter("@P_PASSWORD", password);
                        objParams[19] = new SqlParameter("@P_OPERATION", operation);
                        objParams[20] = new SqlParameter("@P_REGOUT", SqlDbType.NVarChar, 25);
                        objParams[20].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_ADD_NEW_STUDENT", objParams, true);
                        retStatus = ret.ToString();
                    }
                    catch (Exception ex)
                    {
                        retStatus = "0";
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.AddStudent-> " + ex.ToString());
                    }

                    return retStatus;
                }

                public string AddNewStudentDemand(int idno, string receiptcode, int branchno, int semesterno, int paytypeno, int uano, string collegecode)
                {
                    string retStatus = string.Empty;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[8];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        objParams[1] = new SqlParameter("@P_RECIEPTCODE", receiptcode);
                        objParams[2] = new SqlParameter("@P_BRANCHNO", branchno);
                        objParams[3] = new SqlParameter("@P_SEMESTERNO", semesterno);
                        objParams[4] = new SqlParameter("@P_PAYMENTTYPE", paytypeno);
                        objParams[5] = new SqlParameter("@P_UA_NO", uano);
                        objParams[6] = new SqlParameter("@P_COLLEGE_CODE", collegecode);
                        objParams[7] = new SqlParameter("@P_OUTPUT", SqlDbType.NVarChar, 25);
                        objParams[7].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_CREATEDEMAND_NEW_STUDENT", objParams, true);
                        retStatus = ret.ToString();
                    }
                    catch (Exception ex)
                    {
                        retStatus = "0";
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.AddStudent-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetApprovedStudentDetails(int Idno, int operation)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_IDNO", Idno);
                        objParams[1] = new SqlParameter("@P_OPERATION", operation);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_GET_STUDENT_DETAILS", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamController.GetAllExamName-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetApprovedStudentDetails(int Idno, int operation, string password)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_IDNO", Idno);
                        objParams[1] = new SqlParameter("@P_OPERATION", operation);
                        objParams[2] = new SqlParameter("@P_PASSWORD", password);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_GET_STUDENT_DETAILS", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamController.GetAllExamName-> " + ex.ToString());
                    }
                    return ds;
                }

                #region "ParentDashboard"

                //Added by Pritish on 16/02/2021
                public DataSet GetStudentBasicDetails(int idno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_STUDENT_DETAILS_PARENT_DASHBOARD", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentBasicDetails-> " + ex.ToString());
                    }
                    return ds;
                }

                ///Added by Pritish on 17/02/2021
                public DataSet GetStudentCourseTeacherDetails(int idno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_COURSE_TEACHER_PARENT_DASHBOARD", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentBasicDetails-> " + ex.ToString());
                    }
                    return ds;
                }

                ///Added by Pritish on 18/02/2021
                public DataSet GetStudentAttendanceDetails(int year, int monthno, int sessionno, int schemeno, int semesterno, int sectionno, int batchno, int idno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[8];
                        objParams[0] = new SqlParameter("@P_YEAR", year);
                        objParams[1] = new SqlParameter("@P_MONTHNO", monthno);
                        objParams[2] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[3] = new SqlParameter("@P_SCHEMENO", schemeno);
                        objParams[4] = new SqlParameter("@P_SEMESTERNO", semesterno);
                        objParams[5] = new SqlParameter("@P_SECTIONNO", sectionno);
                        objParams[6] = new SqlParameter("@P_BATCHNO", batchno);
                        objParams[7] = new SqlParameter("@P_IDNO", idno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACACD_GET_STUDENT_MONTHLY_ATT", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentBasicDetails-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetStudentDataOnIdno(int idno, string SP)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        ds = objSQLHelper.ExecuteDataSetSP(SP.ToString(), objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentBasicDetails-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetFeesDetails(int sessionno, int idno, int type, string SP)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_IDNO", idno);
                        objParams[2] = new SqlParameter("@P_TYPE", type);
                        ds = objSQLHelper.ExecuteDataSetSP(SP.ToString(), objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentBasicDetails-> " + ex.ToString());
                    }
                    return ds;
                }

                #endregion "ParentDashboard"

                public int UpdateStudentAnnualIncome(string studids, string Income)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_IDNO", studids);
                        objParams[1] = new SqlParameter("@P_ACCNO", Income);
                        objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[2].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_STUDENT_UPDATE_Bank_AccountNo", objParams, false);
                        if (ret != null)
                            if (ret.ToString() != "-99")
                                retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.UpdateStudentAnnualIncome-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int UpdateStudentAnnualIncomedoj(string studidsdoj, string joiningdates)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_IDNO", studidsdoj);
                        objParams[1] = new SqlParameter("@P_ADMDATE", joiningdates);
                        objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[2].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_STUDENT_UPDATE_Bank_AccountNo_dateofjoin", objParams, false);
                        if (ret != null)
                            if (ret.ToString() != "-99")
                                retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.UpdateStudentAnnualIncome-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int UpdateStudentAnnualIncomeifsc(string studidsifsc, string ifscs)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_IDNO", studidsifsc);
                        objParams[1] = new SqlParameter("@P_IFSC", ifscs);
                        objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[2].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_STUDENT_UPDATE_Bank_AccountNo_IFSC", objParams, false);

                        if (ret != null)
                            if (ret.ToString() != "-99")
                                retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.UpdateStudentAnnualIncome-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int UpdateChallan(int idno, int sessionno, string rec_code)
                {
                    int status = -99;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] sqlParams = new SqlParameter[]
                            {
                                 new SqlParameter("@P_IDNO", idno),
                                 new SqlParameter("@P_SESSIONNO", sessionno),
                                 new SqlParameter("@P_RECEIPT_CODE", rec_code),
                                 new SqlParameter("@P_OUTPUT",status)
                            };
                        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

                        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_UPDATE_CHALLAN_DETAILS_FINAL", sqlParams, true);
                        status = (Int32)obj;
                    }
                    catch (Exception ex)
                    {
                        status = -99;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.UpdateChallan() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return status;
                }

                public DataSet GetStudScholarshipDetails(int idno, int monthno, int yearno, int sessionno)
                {
                    DataSet ds = null;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        objParams[1] = new SqlParameter("@P_MONTHNO", monthno);
                        objParams[2] = new SqlParameter("@P_YEARNO", yearno);
                        objParams[3] = new SqlParameter("@P_SESSIONNO", sessionno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_STUD_SCHOLARSHIP_DETAILS_FINAL", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudScholarshipDetails->" + ex.ToString());
                    }
                    return ds;
                }

                public string AddNewStudent(Student objStudent, string firstname, string lastname, int telephone, int nic, DateTime dob, string mothername, string fathername,
                   string parentsmob, int sri, int passport, string password, string remark, int idno, int operation)
                {
                    string retStatus = string.Empty;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[29];

                        objParams[0] = new SqlParameter("@P_STUDFIRSTNAME", firstname);
                        objParams[1] = new SqlParameter("@P_STUDLASTNAME", lastname);
                        objParams[2] = new SqlParameter("@P_STUDNAME", objStudent.StudName);
                        objParams[3] = new SqlParameter("@P_STUDENTMOBILE", objStudent.StudentMobile);
                        objParams[4] = new SqlParameter("@P_TELPHONENO", telephone);
                        objParams[5] = new SqlParameter("@P_NICNO", nic);
                        objParams[6] = new SqlParameter("@P_PASSPORTNO", passport);
                        objParams[7] = new SqlParameter("@P_DOB", dob);
                        objParams[8] = new SqlParameter("@P_MOTHERNAME", mothername);
                        objParams[9] = new SqlParameter("@P_FATHERNAME", fathername);
                        objParams[10] = new SqlParameter("@P_FATHERMOBILE", parentsmob);
                        objParams[11] = new SqlParameter("@P_EMAILID", objStudent.EmailID);
                        objParams[12] = new SqlParameter("@P_SRILANKAN", sri);
                        objParams[13] = new SqlParameter("@P_COLLEGE_ID", objStudent.College_ID);
                        objParams[14] = new SqlParameter("@P_DEGREENO", objStudent.DegreeNo);
                        objParams[15] = new SqlParameter("@P_BRANCHNO", objStudent.BranchNo);
                        objParams[16] = new SqlParameter("@P_IDTYPE", objStudent.IdType);
                        objParams[17] = new SqlParameter("@P_ADMCATEGORYNO", objStudent.AdmCategoryNo);
                        objParams[18] = new SqlParameter("@P_ADMDATE", objStudent.AdmDate);
                        objParams[19] = new SqlParameter("@P_YEAR", objStudent.Year);
                        objParams[20] = new SqlParameter("@P_SEX", objStudent.Sex);
                        objParams[21] = new SqlParameter("@P_ADMBATCH", objStudent.AdmBatch);
                        objParams[22] = new SqlParameter("@P_PTYPE", objStudent.PType);
                        objParams[23] = new SqlParameter("@P_SEMESTERNO", objStudent.SemesterNo);
                        objParams[24] = new SqlParameter("@P_PASSWORD", password);
                        objParams[25] = new SqlParameter("@P_REMARK", remark);
                        objParams[26] = new SqlParameter("@P_IDNO", idno);
                        objParams[27] = new SqlParameter("@P_OPERATION", operation);
                        objParams[28] = new SqlParameter("@P_REGOUT", SqlDbType.NVarChar, 25);
                        objParams[28].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_ADD_NEW_STUDENT", objParams, true);
                        retStatus = ret.ToString();
                    }
                    catch (Exception ex)
                    {
                        retStatus = "0";
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.AddStudent-> " + ex.ToString());
                    }

                    return retStatus;
                }

                public int Insert_PostChequeEntry(int idno, int chequeno, DateTime Receivedate, double Amount, string cheque_no, int bankno, DateTime DueDate, string ipaddress, int uano)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[9];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        objParams[1] = new SqlParameter("@P_CHEQUENO", chequeno);
                        objParams[2] = new SqlParameter("@P_RECEIVED_DATE", Receivedate);
                        objParams[3] = new SqlParameter("@P_AMOUNT", Amount);
                        objParams[4] = new SqlParameter("@P_CHEQUE_NO", cheque_no);
                        objParams[5] = new SqlParameter("@P_BANK_NO", bankno);
                        objParams[6] = new SqlParameter("@P_DUE_DATE", DueDate);
                        objParams[7] = new SqlParameter("@P_IPADDRESS", ipaddress);
                        objParams[8] = new SqlParameter("@P_ENTRYBY", uano);
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INS_POST_CHEQUE_ENTRY", objParams, true);

                        if (Convert.ToInt32(ret) == 1)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        else if (Convert.ToInt32(ret) == 2)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.Insert_Update_StudentDocuments-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetStudentFeesDetails(string receiptcode, int sessionno, int idno, int semesterno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_RECEIPTCODE", receiptcode);
                        objParams[1] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[2] = new SqlParameter("@P_IDNO", idno);
                        objParams[3] = new SqlParameter("@P_SEMESTERNO", semesterno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_STUD_FEES_DETAILS", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentBasicDetails-> " + ex.ToString());
                    }
                    return ds;
                }

                public int UpdateStudentAnnualIncome(string studids, string Income, string bankno, string bankBranch)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_IDNO", studids);
                        objParams[1] = new SqlParameter("@P_ACCNO", Income);
                        objParams[2] = new SqlParameter("@P_BANKNO", bankno);
                        objParams[3] = new SqlParameter("@P_BANKBRANCH", bankBranch);
                        objParams[4] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[4].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_STUDENT_UPDATE_BANK_ACCOUNTNO", objParams, false);
                        if (ret != null)
                            if (ret.ToString() != "-99")
                                retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.UpdateStudentAnnualIncome-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetSemPromotionStudList(int collegeid, int sessionno, int degreeno, int branchno, int schemeno, int semesterno)
                {
                    DataSet ds = null;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_COLLEGEID", collegeid);
                        objParams[1] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[2] = new SqlParameter("@P_DEGREENO", degreeno);
                        objParams[3] = new SqlParameter("@P_BRANCHNO", branchno);
                        objParams[4] = new SqlParameter("@P_SCHEMENO", schemeno);
                        objParams[5] = new SqlParameter("@P_SEMESTERNO", semesterno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_SEM_PROMOTION_STUD_LIST", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentDetails->" + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetSemesterPromotion(int sessionnno, int collegeid, string degreeno, string branchno, string schemeno, int semesterno, int status)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[7];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionnno);
                        objParams[1] = new SqlParameter("@P_COLLEGE_ID", collegeid);
                        objParams[2] = new SqlParameter("@P_DEGREENO", degreeno);
                        objParams[3] = new SqlParameter("@P_BRANCHNO", branchno);
                        objParams[4] = new SqlParameter("@P_SCHEMENO", schemeno);
                        objParams[5] = new SqlParameter("@P_SEMESTERNO", semesterno);
                        objParams[6] = new SqlParameter("@P_STATUS", status);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_SEMESTER_PROMOTION_LIST", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentDocVerification() -->" + ex.ToString());
                    }
                    return ds;
                }

                public int bulkStudentSemPromo(Student objStudent, string IDNO, int sessionNo, string ipadd, string Status, int ALVERIFIED)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        //Update Student semester Rtm
                        objParams = new SqlParameter[8];
                        objParams[0] = new SqlParameter("@P_IDNO", IDNO);
                        objParams[1] = new SqlParameter("@P_SEMESTERNO", objStudent.SemesterNo);
                        objParams[2] = new SqlParameter("@P_UA_NO", objStudent.Uano);
                        objParams[3] = new SqlParameter("@P_IPADDRESS", ipadd);
                        objParams[4] = new SqlParameter("@P_SESSIONNO", sessionNo);
                        objParams[5] = new SqlParameter("@P_STATUS", Status);
                        objParams[6] = new SqlParameter("@P_AL_VERIFIED", ALVERIFIED);
                        objParams[7] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[7].Direction = ParameterDirection.Output;

                        int status = (Int32)objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_INS_SEM_PROMOTION_NEW", objParams, true);
                        if (status == 2)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        else if (status == 2627)
                            retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.Error);
                        return retStatus;
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.UpdateStudent-> " + ex.ToString());
                    }
                }

                //ADDED BY AASHNA 27-07-2022
                public DataSet GetEXAMINATION(int idno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_DATA", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.RetrieveStudentFeesDetails-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet getstudentid(string DynamicFilter, string EndDate, int Adm_type, int Command_type, int intake,
                 int UGPG, int decipline, int campus, int aptitude_center, int aptitude_medium, string FROM_DATE, string TO_DATE)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[12];
                        objParams[0] = new SqlParameter("@P_DYNAMIC_FILTER", DynamicFilter);
                        objParams[1] = new SqlParameter("@P_END_DATE", EndDate);
                        objParams[2] = new SqlParameter("@P_ADMIN_TYPE", Adm_type);
                        objParams[3] = new SqlParameter("@P_COMMAND_TYPE", Command_type);
                        objParams[4] = new SqlParameter("@P_INTAKE", intake);
                        objParams[5] = new SqlParameter("@P_UGPGOT", UGPG);
                        objParams[6] = new SqlParameter("@P_DISCIPLINE", decipline);
                        objParams[7] = new SqlParameter("@P_CAMPUS", campus);
                        objParams[8] = new SqlParameter("@P_APTITUDE_TEST_CENTRE", aptitude_center);
                        objParams[9] = new SqlParameter("@P_APTITUDE_TEST_MEDIUM", aptitude_medium);
                        objParams[10] = new SqlParameter("@P_FROM_DATE", FROM_DATE);
                        objParams[11] = new SqlParameter("@P_TO_DATE", TO_DATE);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_SEARCH_STUDENT_FOR_LEAD_ALLOTMENT", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.EmpCreateController.RetrieveEmpDetails-> " + ex.ToString());
                    }
                    return ds;
                }

                public int Insert_StudentRecord(string UserNo, string ApplicationNo, string EnqueryNos, string ipAddress, int uano)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_USERNOS", UserNo);
                        objParams[1] = new SqlParameter("@P_APPLICATIONNOS", ApplicationNo);
                        objParams[2] = new SqlParameter("@P_ENQUIRYNOS", EnqueryNos);
                        objParams[3] = new SqlParameter("@P_IPADDRESS", ipAddress);
                        objParams[4] = new SqlParameter("@P_UANO", uano);
                        objParams[5] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[5].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INSERT_UPDATE_LEAD_ENQUERY_DETAILS", objParams, true);

                        if (Convert.ToInt32(ret) == 1)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        else if (Convert.ToInt32(ret) == 2)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.Insert_Update_StudentDocuments-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet getAppicantData(string Applicant_No)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_APPLICATION_NO", Applicant_No);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_APPLICANT_DATA", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.EmpCreateController.RetrieveEmpDetails-> " + ex.ToString());
                    }
                    return ds;
                }

                //added by swapnil thakare on dated 28-09-2021
                public DataSet getMainHeadData(string mainheadids)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_LEAD_ALLOT_STATUS", mainheadids);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_LEAD_ALLOT_FILTER_DATA_MAIN", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.EmpCreateController.RetrieveEmpDetails-> " + ex.ToString());
                    }
                    return ds;
                }

                //added by swapnil thakare on dated 29-09-2021
                public DataSet getSecondHeadData(string mainheadids)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        //objParams[0] = new SqlParameter("@P_STATE", 0);
                        objParams[0] = new SqlParameter("@P_MAIN_HEAD", mainheadids);
                        //objParams[2] = new SqlParameter("@P_LEAD_ALLOT_STATUS","");
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_LEAD_ALLOT_FILTER_DATA_SECOND", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.EmpCreateController.RetrieveEmpDetails-> " + ex.ToString());
                    }
                    return ds;
                }

                //Added by swapnil thakare on dated 16-09-2021
                public DataSet getLeadStatusData(int enquirystatus)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_ENQUIRYSTATUS", enquirystatus);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_LEAD_STATUS_DATA", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.EmpCreateController.RetrieveEmpDetails-> " + ex.ToString());
                    }
                    return ds;
                }

                //Added by swapnil thakare on dated 17-09-2021
                public DataSet getShowReport(int admbatch, int degree, int reporttype)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_DEGREENO", degree);
                        objParams[1] = new SqlParameter("@P_ADMBATCH", admbatch);
                        objParams[2] = new SqlParameter("@P_REPORTTYPE", reporttype);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_STUDENT_ADMISSION_STATUS_REPORT", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.EmpCreateController.RetrieveEmpDetails-> " + ex.ToString());
                    }
                    return ds;
                }

                //add by aashna -04-10-2021
                public SqlDataReader GetEditResourseRoom(int Roomno)
                {
                    SqlDataReader dr = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[] { new SqlParameter("@P_OUTPUT", Roomno) };

                        dr = objSQLHelper.ExecuteReaderSP("PKG_ACD_GET_RESOURSE_ROOM", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.GradeEntryController.GetGradeEntryByGradeNo() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return dr;
                }

                //add by aashna -04-10-2021
                public DataSet GetResourseRoom(int COLLEGE_ID)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_COLLEGE_ID", COLLEGE_ID);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_RESOURSE_ROOM_DETAILS", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentController.GetCancelCourseData() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }

                //add by aashna -04-10-2021
                public int InsertResourseRoom(Student_Acd objStudent, string roomname, int CAMPUSNO, string COLLEGE_CODE)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[8];

                        objParams[0] = new SqlParameter("@P_COLLEGE_ID", objStudent.COLLEGE_ID);
                        objParams[1] = new SqlParameter("@P_CAMPUSNO", CAMPUSNO);
                        objParams[2] = new SqlParameter("@P_ROOMNAME", roomname);
                        objParams[3] = new SqlParameter("@P_ROOM_CATEGORY", objStudent.Roomcategory);
                        objParams[4] = new SqlParameter("@P_SEATING_CAPACITY", objStudent.SeatingCapacity);
                        objParams[5] = new SqlParameter("@P_EXAM_CAPACITY", objStudent.Examcapacity);
                        objParams[6] = new SqlParameter("@P_COLLEGE_CODE", COLLEGE_CODE);
                        objParams[7] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[7].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_NST_RESOURSE_ROOM", objParams, false);
                        if (ret != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.UpdateStudent_TeachAllot-> " + ex.ToString());
                    }
                    return retStatus;
                }

                //add by aashna -04-10-2021
                public int UpdateResourseRoom(Student_Acd objStudent, string roomname, int ROOMNO, int CAMPUSNO)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[8];

                        objParams[0] = new SqlParameter("@P_COLLEGE_ID", objStudent.COLLEGE_ID);
                        objParams[1] = new SqlParameter("@P_CAMPUSNO", CAMPUSNO);
                        objParams[2] = new SqlParameter("@P_ROOMNAME", roomname);
                        objParams[3] = new SqlParameter("@P_ROOM_CATEGORY", objStudent.Roomcategory);
                        objParams[4] = new SqlParameter("@P_SEATING_CAPACITY", objStudent.SeatingCapacity);
                        objParams[5] = new SqlParameter("@P_EXAM_CAPACITY", objStudent.Examcapacity);
                        objParams[6] = new SqlParameter("@P_Roomno", ROOMNO);
                        objParams[7] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[7].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_UPD_RESOURSE_ROOM", objParams, false);
                        if (ret != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.UpdateStudent_TeachAllot-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetRegistrationBulkDetails(string Emailid, string Mobileno, int StudyType)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_EMAILID", Emailid);
                        objParams[1] = new SqlParameter("@P_MOBILENO", Mobileno);
                        objParams[2] = new SqlParameter("@P_STUDY_LEVEL", StudyType);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_REGISTRATION_BULK_FILLDROPDOWN_DATA", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic-> " + ex.ToString());
                    }
                    return ds;
                }

                public int RegisterNewUser(Student_Acd objus, DataTable dtRecord, int Command_type, string homeTelCode, string DateofBirth)
                {
                    int retStatus = 0;//Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        //Add New User
                        objParams = new SqlParameter[21];
                        objParams[0] = new SqlParameter("@P_EMAILID", objus.EMAILID);
                        objParams[1] = new SqlParameter("@P_MOBILENO", objus.MOBILENO);
                        objParams[2] = new SqlParameter("@P_FIRSTNAME", objus.FIRSTNAME);
                        objParams[3] = new SqlParameter("@P_LASTNAME", objus.LASTNAME);
                        objParams[4] = new SqlParameter("@P_DEGREENO", objus.DEGREENO);
                        objParams[5] = new SqlParameter("@P_BRANCHNO", objus.BRANCHNO);
                        objParams[6] = new SqlParameter("@P_MOBILECODE", objus.MobileCode);
                        objParams[7] = new SqlParameter("@P_ADMBATCH", objus.AdmBatch);
                        objParams[8] = new SqlParameter("@P_ADMTYPE", objus.ADMTYPE);
                        objParams[9] = new SqlParameter("@P_DOB", DateofBirth);
                        objParams[10] = new SqlParameter("@P_PASSPORTNO", objus.PassportNo);
                        objParams[11] = new SqlParameter("@P_NIC", objus.NIC);
                        objParams[12] = new SqlParameter("@P_GENDER", objus.Gender);
                        objParams[13] = new SqlParameter("@P_DTRECORDS", dtRecord);
                        objParams[14] = new SqlParameter("@P_USERNO", objus.USERNO);
                        objParams[15] = new SqlParameter("@P_COMMAND_TYPE", Command_type);
                        objParams[16] = new SqlParameter("@P_HOME_MOBILENO", objus.Homemobileno);
                        objParams[17] = new SqlParameter("@P_STUDY_TYPE", objus.UGPG);
                        objParams[18] = new SqlParameter("@P_HOME_TEL_CODE", homeTelCode);
                        objParams[19] = new SqlParameter("@P_SOURCETYPENO", objus.source);

                        objParams[20] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[20].Direction = ParameterDirection.Output;

                        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_USER_INSERT", objParams, true);

                        if (obj != null && obj.ToString().Equals("-1001"))
                        {
                            retStatus = Convert.ToInt32(obj);// Convert.ToInt32(CustomStatus.DuplicateRecord);
                        }
                        else
                        {
                            retStatus = Convert.ToInt32(obj);// Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.UserController.RegisterNewUser-> " + ex.ToString());
                    }

                    return retStatus;
                }

                public int LockUpdateMeritList(string usernos, int admbatch, int degreeno, int EntranceNo, string MeritNos, int uano, string Cutoff, int Commandtype, string ProgramNos)
                {
                    int retStatus = 0;//Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        //Add New User
                        objParams = new SqlParameter[10];
                        objParams[0] = new SqlParameter("@P_USERNO", usernos);
                        objParams[1] = new SqlParameter("@P_ADMBATCH", admbatch);
                        objParams[2] = new SqlParameter("@P_DEGREENO", degreeno);
                        objParams[3] = new SqlParameter("@P_ENTRANCENO", EntranceNo);
                        objParams[4] = new SqlParameter("@P_MERIT_NOS", MeritNos);
                        objParams[5] = new SqlParameter("@P_UA_NO", uano);
                        objParams[6] = new SqlParameter("@P_CUTOFF", Cutoff);
                        objParams[7] = new SqlParameter("@P_COMMAND_TYPE", Commandtype);
                        objParams[8] = new SqlParameter("@P_PROGRAMNOS", ProgramNos);
                        objParams[9] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[9].Direction = ParameterDirection.Output;

                        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_UPDATE_MERIT_LIST_DETAILS", objParams, true);

                        if (obj != null && obj.ToString().Equals("-1001"))
                        {
                            retStatus = Convert.ToInt32(obj);// Convert.ToInt32(CustomStatus.DuplicateRecord);
                        }
                        else
                        {
                            retStatus = Convert.ToInt32(obj);// Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.UserController.RegisterNewUser-> " + ex.ToString());
                    }

                    return retStatus;
                }

                public int CreateOnlineAdmBulkUSer(string usernos, string Password, int ua_no)
                {
                    int retStatus = 0;//Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        //Add New User
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_USERNO", usernos);
                        objParams[1] = new SqlParameter("@P_PASSWORD", Password);
                        objParams[2] = new SqlParameter("@P_UA_NO", ua_no);
                        objParams[3] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[3].Direction = ParameterDirection.Output;

                        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_TRANSFER_STUD_ONLINE_ADMISSION_DATA_NEW", objParams, true);

                        if (obj != null && obj.ToString().Equals("-1001"))
                        {
                            retStatus = Convert.ToInt32(obj);// Convert.ToInt32(CustomStatus.DuplicateRecord);
                        }
                        else
                        {
                            retStatus = Convert.ToInt32(obj);// Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.UserController.RegisterNewUser-> " + ex.ToString());
                    }

                    return retStatus;
                }

                public int DeleteStudentInstalment(int idno, int Semesterno, string recieptcode)
                {
                    int status = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objDataAccess = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] sqlParams = new SqlParameter[]
                     {
                           new SqlParameter("@P_IDNO", idno),
                           new SqlParameter("@P_SEMESTERNO", Semesterno),

                           new SqlParameter("@P_RECIEPT_CODE", recieptcode),

                           new SqlParameter("@P_OUT", SqlDbType.Int),
                       };
                        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;
                        status = (Int32)objDataAccess.ExecuteNonQuerySP("PKG_ACD_STUD_DELETE_INSTALMENT", sqlParams, true);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.SessionActivityController.AddSessionActivity() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return status;
                }

                public DataSet GetStudentTrackRecord(int userno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_USERNO", userno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_ADMISSION_TRACK_RECORD", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic-> " + ex.ToString());
                    }
                    return ds;
                }

                //add by aashna 06-12-2021
                public int InsertFollowDate(int userno, int uano, string username, string name, string emailid, string mobile, DateTime followdate, int complete)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[9];

                        objParams[0] = new SqlParameter("@P_USERNO", userno);
                        objParams[1] = new SqlParameter("@P_UA_NO", uano);
                        objParams[2] = new SqlParameter("@P_USERNAME", username);
                        objParams[3] = new SqlParameter("@P_NAME", name);
                        objParams[4] = new SqlParameter("@P_MOBILENO", emailid);
                        objParams[5] = new SqlParameter("@P_EMAILID", mobile);
                        objParams[6] = new SqlParameter("@P_FOLLOWUP_DATE", followdate);
                        objParams[7] = new SqlParameter("@P_COMPLETED_DATE", complete);
                        objParams[8] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[8].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("ACD_MARKED_AS_COMPLETED_FOLLOWUPDATE", objParams, false);
                        if (ret != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.UpdateStudent_TeachAllot-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GETTODAYSDATE(int Adm_type)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_ADMIN_TYPE", Adm_type);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_TODAYS_DATE", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.EmpCreateController.RetrieveEmpDetails-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GETUPCOMINGDATE(int Adm_type)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_ADMIN_TYPE", Adm_type);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_UPCOMING_DATE", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.EmpCreateController.RetrieveEmpDetails-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GETOVERDUEDATE(int Adm_type)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_ADMIN_TYPE", Adm_type);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_OVERDUE_DATE", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.EmpCreateController.RetrieveEmpDetails-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GETALLDATE(int Adm_type)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_ADMIN_TYPE", Adm_type);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_ALL_DATE", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.EmpCreateController.RetrieveEmpDetails-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GETCOMPLETEDATE(int Adm_type)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_ADMIN_TYPE", Adm_type);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_COMPLETED_DATE", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.EmpCreateController.RetrieveEmpDetails-> " + ex.ToString());
                    }
                    return ds;
                }

                //Added by swapnil thakare on dated 09/01/2022
                public DataSet GetApplicationDetailsInterviw(int intake, string StudyType, string programNos, string branchNos, int progresslevel)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_INTAKE", intake);
                        objParams[1] = new SqlParameter("@P_STUDYLEVELNOS", StudyType);
                        objParams[2] = new SqlParameter("@P_PROGRAMNOS", programNos);
                        objParams[3] = new SqlParameter("@P_BRANCHNOS", branchNos);
                        objParams[4] = new SqlParameter("@P_PROGRESS_LEVEL", progresslevel);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_INTERVIEW_APPLICATION_DETAILS_SHOW", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic-> " + ex.ToString());
                    }
                    return ds;
                }

                //ADD BY AASHNA -14-1-2022
                public DataSet getaptideatils(int userno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);

                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_INTAKE", userno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_STUDENT_APTI_DATA", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentsForScheme-> " + ex.ToString());
                    }

                    return ds;
                }

                //ADD BY AASHNA -15-1-2022
                public DataSet getaplieddeatils(int userno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);

                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_INTAKE", userno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_STUDENT_APLIED_DATA", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentsForScheme-> " + ex.ToString());
                    }

                    return ds;
                }

                //ADD BY AASHNA -17-01-2022
                public int Insertecaminer(int sessionno, int courseno, string ua_nos, string campus, int uano)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[6];

                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_COURSENO", courseno);
                        objParams[2] = new SqlParameter("@P_ADD_UA_NOS", ua_nos);
                        objParams[3] = new SqlParameter("@P_CAMPUS_NOS", campus);
                        objParams[4] = new SqlParameter("@P_ENTRY_BY_UANO", uano);
                        objParams[5] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[5].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_SP_INS_ADDITIONAL_FACULTY_SUB_WISE", objParams, false);
                        if (ret != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.UpdateStudent_TeachAllot-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet Getcoexaminer(int session)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", session);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_SP_GET_ADDITIONAL_FACULTY_SUB_WISE", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.EmpCreateController.RetrieveEmpDetails-> " + ex.ToString());
                    }
                    return ds;
                }

                //ADD BY AASHNA -24-1-2022
                public DataSet getsummaryreport(int admbatch, int degree, int branch, int college_id, int AFFILIATED)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);

                        SqlParameter[] objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_INTAKE", admbatch);
                        objParams[1] = new SqlParameter("@P_DEGREENO", degree);
                        objParams[2] = new SqlParameter("@P_BRANCHNO", branch);
                        objParams[3] = new SqlParameter("@P_COLLGE_ID", college_id);
                        objParams[4] = new SqlParameter("@P_AFFLIATEDNO", AFFILIATED);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_STUDENT_SUMMERY_REPORT", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentsForScheme-> " + ex.ToString());
                    }

                    return ds;
                }

                public DataSet GetOnlineSearchDetails(string SearchValue, int DynamicFilter)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);

                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_SEARCHVALUE", SearchValue);
                        objParams[1] = new SqlParameter("@P_DYNAMIC_FILTER", DynamicFilter);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_SEARCH_STUDENT_ONLINE_DETAILS", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentsForScheme-> " + ex.ToString());
                    }

                    return ds;
                }

                //ADD BY AASHNA 25-01-2022
                public int Updwith(int idno, string status)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[3];

                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        objParams[1] = new SqlParameter("@P_ADMIN_APPROVAL", status);
                        objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[2].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_UPDWITH_APPR", objParams, false);
                        if (ret != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.UpdateStudent_TeachAllot-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet getjob_profile_data(string idno, int Scheduleno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        objParams[1] = new SqlParameter("@P_SCHEDULENO", Scheduleno);
                        ds = objSQLHelper.ExecuteDataSetSP("ACD_TP_BIND_ROUND_DETAILS_ON_JOB_PROFILE", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic-> " + ex.ToString());
                    }
                    return ds;
                }

                public int InsertBridgingGroup(int Criteria, int Intake, int Facultyno, int Grouptype, int NoofGroups, int Status, int uano, DataTable dt)
                {
                    int retStatus = 0;//Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        //Add New User
                        objParams = new SqlParameter[9];
                        objParams[0] = new SqlParameter("@P_INTAKE", Intake);
                        objParams[1] = new SqlParameter("@P_FACULTY", Facultyno);
                        objParams[2] = new SqlParameter("@P_GROUPTYPE", Grouptype);
                        objParams[3] = new SqlParameter("@P_NUMBER_OF_GROUPS", NoofGroups);
                        objParams[4] = new SqlParameter("@P_STATUS", Status);
                        objParams[5] = new SqlParameter("@P_UA_NO", uano);
                        objParams[6] = new SqlParameter("@P_DATATABLE", dt);
                        objParams[7] = new SqlParameter("@P_CRITERIANO", Criteria);
                        objParams[8] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[8].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INSERT_BRIDGING_GROUP", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        if (Convert.ToInt32(ret) == 1)

                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.UserController.RegisterNewUser-> " + ex.ToString());
                    }

                    return retStatus;
                }

                public DataSet BindLVBridgingGroup(int admbatch, int college_id, int grouptype, int criteriano)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_INTAKE", admbatch);
                        objParams[1] = new SqlParameter("@P_FACULTY", college_id);
                        objParams[2] = new SqlParameter("@P_GROUPTYPE", grouptype);
                        objParams[3] = new SqlParameter("@P_CRITERIANO", criteriano);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_BIND_DATA_ON_BRIDGING_GROUP", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetStudentListonGroupCreationpage(int Intake, int Faculty, int Studylevel, int Branchno, int Degreeno, int Affuno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSqlHlper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_INTAKE", Intake);
                        objParams[1] = new SqlParameter("@P_FACULTY", Faculty);
                        objParams[2] = new SqlParameter("@P_STAUDYLEVEL", Studylevel);
                        objParams[3] = new SqlParameter("@P_BRANCHNO", Branchno);
                        objParams[4] = new SqlParameter("@P_Degreeno", Degreeno);
                        objParams[5] = new SqlParameter("@P_AffUniversity", Affuno);

                        ds = objSqlHlper.ExecuteDataSetSP("PKG_ACD_BIND_LV_ON_GROUP_CREATION_PAGE", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentDataforupdate-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet EditBridgingGroupbyID(int BID)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSqlHlper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_BID", BID);
                        ds = objSqlHlper.ExecuteDataSetSP("PKG_ACD_GET_DATA_BY_BGID_EDIT_RECORDS", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentDataforupdate-> " + ex.ToString());
                    }
                    return ds;
                }

                public int UpdateBridgingGroup(int BID, int Inatake, int Facultyno, int Grouptype, string GroupName, int Capacity, int Status)
                {
                    int retStatus = 0;//Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        //Add New User
                        objParams = new SqlParameter[8];
                        objParams[0] = new SqlParameter("@P_BID", BID);
                        objParams[1] = new SqlParameter("@P_INTAKE", Inatake);
                        objParams[2] = new SqlParameter("@P_FACULTY", Facultyno);
                        objParams[3] = new SqlParameter("@P_GROUPTYPE", Grouptype);
                        objParams[4] = new SqlParameter("@P_GROUPNAME", GroupName);
                        objParams[5] = new SqlParameter("@P_CAPACITY", Capacity);
                        objParams[6] = new SqlParameter("@P_STATUS", Status);
                        objParams[7] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[7].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_BRIDGING_GROUP_UPDATE", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        if (Convert.ToInt32(ret) == 2)

                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.UserController.RegisterNewUser-> " + ex.ToString());
                    }

                    return retStatus;
                }

                public int InsertGroupNameonGroupCreationPage(int IDNO, int GROUPNAMEID, int Admbatch)
                {
                    int retStatus = 0;//Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        //Add New User
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_IDNO", IDNO);
                        objParams[1] = new SqlParameter("@P_GROUPNAMEID", GROUPNAMEID);
                        objParams[2] = new SqlParameter("@P_ADMBATCH", Admbatch);
                        objParams[3] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[3].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_UPDATE_GROUPNAME_GROUP_CREATION", objParams, true);
                        if (Convert.ToInt32(ret) == 1)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.Error);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.UserController.RegisterNewUser-> " + ex.ToString());
                    }

                    return retStatus;
                }

                public DataSet GetRefundAmountDetails(int idno, int srno, int request_type)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);

                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        objParams[1] = new SqlParameter("@P_SRNO", srno);
                        objParams[2] = new SqlParameter("@P_REQUEST_TYPE", request_type);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_AMOUNT_DETAILS", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentsForScheme-> " + ex.ToString());
                    }

                    return ds;
                }

                public int UPDATEGROUPNAMEONGROUPCREATION(int IDNO, int GROUPNAMEID)
                {
                    int retStatus = 0;//Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        //Add New User
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_IDNO", IDNO);
                        objParams[1] = new SqlParameter("@P_GROUPNAMEID", GROUPNAMEID);
                        objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[2].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_UPDATE_GROUPID_ON_GROUP_CREATION", objParams, true);
                        if (Convert.ToInt32(ret) == 2)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.Error);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.UserController.RegisterNewUser-> " + ex.ToString());
                    }

                    return retStatus;
                }

                //ADD BY AASHNA -24-1-2022
                public DataSet getwithapprovaldata(int commandtype, string fromdate, string todate, int stattus, int college_id)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);

                        SqlParameter[] objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_COMMAND_TYPE", commandtype);
                        objParams[1] = new SqlParameter("@P_STATUS", stattus);
                        objParams[2] = new SqlParameter("@P_FROM_DATE", fromdate);
                        objParams[3] = new SqlParameter("@P_TO_DATE", todate);
                        objParams[4] = new SqlParameter("@P_COLLEGE_ID", college_id);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_WITHDRAWAL_APPROVAL_DATA", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentsForScheme-> " + ex.ToString());
                    }

                    return ds;
                }

                public int UpdDetailsByAdmin(int idno, decimal amount, string remark, int status, string description, int srno, decimal total_refund, string sturemark, string dcrno)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[10];

                        objParams[0] = new SqlParameter("@P_DESCRIPTION_BY_ADMIN", description);
                        objParams[1] = new SqlParameter("@P_STATS_BY_ADMIN", status);
                        objParams[2] = new SqlParameter("@P_REMARK_BY_ADMIN", remark);
                        objParams[3] = new SqlParameter("@P_REFUND_AMOUNT", amount);
                        objParams[4] = new SqlParameter("@P_IDNO", idno);
                        objParams[5] = new SqlParameter("@P_SRNO", srno);
                        objParams[6] = new SqlParameter("@P_TOTAL_REFUND_AMOUNT", total_refund);
                        objParams[7] = new SqlParameter("@P_STUDENT_REMARK_BY_O_M_D_F", sturemark);
                        objParams[8] = new SqlParameter("@P_DCR_NO", dcrno);
                        objParams[9] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[9].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_UPDATE_DETAILS_BY_ADMIN", objParams, false);
                        if (ret != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.UpdateStudent_TeachAllot-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet RetrieveStudentCurrentRegDetailSem(int idno)  //Used for semester reg status
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_GET_COURSEREGISTRATION_DETAILS_BY_ID", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.RetrieveStudentCurrentRegDetails-> " + ex.ToString());
                    }
                    return ds;
                }

                //ADD BY AASHNA -02-03-2022
                public int UPDFINANCESTATUS(int idno, string status)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[3];

                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        objParams[1] = new SqlParameter("@P_FINANCE_APPROVAL", status);
                        objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[2].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_UPD_WITH_FINANCE", objParams, false);
                        if (ret != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.UpdateStudent_TeachAllot-> " + ex.ToString());
                    }
                    return retStatus;
                }

                //ADD BY AASHNA -03-03-2022

                public DataSet getwithapprovaldataforfinance(int commandtype, string fromdate, string todate, int status, int college_id)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);

                        SqlParameter[] objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_COMMAND_TYPE", commandtype);
                        objParams[1] = new SqlParameter("@P_FROM_DATE", fromdate);
                        objParams[2] = new SqlParameter("@P_TO_DATE", todate);
                        objParams[3] = new SqlParameter("@P_STATUS", status);
                        objParams[4] = new SqlParameter("@P_COLLEGE_ID", college_id);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_WITHDRAWAL_APPROVAL_DATA_FOR_FINANCE", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentsForScheme-> " + ex.ToString());
                    }

                    return ds;
                }

                public DataSet GetStudentonResultProcessPage(int SESSIONNO, int COLLEGE_ID, int SECTIONNO, string BRANCHNO, string DEGREENO, string affUniveristy, int SEMESTERNO, int RESULTTYPE)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSqlHlper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[8];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", SESSIONNO);
                        objParams[1] = new SqlParameter("@P_COLLEGE_ID", COLLEGE_ID);
                        objParams[2] = new SqlParameter("@P_SECTIONNO", SECTIONNO);
                        objParams[3] = new SqlParameter("@P_BRANCHNO", BRANCHNO);
                        objParams[4] = new SqlParameter("@P_DEGREENO", DEGREENO);
                        objParams[5] = new SqlParameter("@P_AFFUNIVERISTY", affUniveristy);
                        objParams[6] = new SqlParameter("@P_SEMESTERNO", SEMESTERNO);
                        objParams[7] = new SqlParameter("@P_RESULTYPE", RESULTTYPE);
                        ds = objSqlHlper.ExecuteDataSetSP("PKG_ACD_GET_STUD_LIST_ON_RESULT_PROCESS_PAGE", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentDataforupdate-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetStudentonExamReportPage(int SESSIONNO, int COLLEGE_ID, int SECTIONNO, string BRANCHNO, string DEGREENO, string affUniveristy, int SEMESTERNO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSqlHlper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[7];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", SESSIONNO);
                        objParams[1] = new SqlParameter("@P_COLLEGE_ID", COLLEGE_ID);
                        objParams[2] = new SqlParameter("@P_SECTIONNO", SECTIONNO);
                        objParams[3] = new SqlParameter("@P_BRANCHNO", BRANCHNO);
                        objParams[4] = new SqlParameter("@P_DEGREENO", DEGREENO);
                        objParams[5] = new SqlParameter("@P_AFFUNIVERISTY", affUniveristy);
                        objParams[6] = new SqlParameter("@P_SEMESTERNO", SEMESTERNO);
                        ds = objSqlHlper.ExecuteDataSetSP("PKG_ACD_GET_STUD_LIST_ON_EXAM_REPORT_PAGE", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentDataforupdate-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet RetrieveStudentFeesDetailsNEW(int idno)  //StudentController.cs
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_GET_FEES_DETAILS_BY_ID_NEW", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.RetrieveStudentFeesDetailsNEW-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet RetrieveStudentCurrentResult(int idno)  //StudentController.cs
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_STUDENT_RESULT_NEW", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.RetrieveStudentCurrentRegDetails-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetOnlineSearchDetailsForStudent(string SearchValue, int DynamicFilter)  //StudentController.cs
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);

                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_SEARCHVALUE", SearchValue);
                        objParams[1] = new SqlParameter("@P_DYNAMIC_FILTER", DynamicFilter);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_SEARCH_STUDENT_INFORMATION_DETAILS", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentsForScheme-> " + ex.ToString());
                    }

                    return ds;
                }

                //ADDED BY SWAPNIL THAKARE ON DATED 20-04-2022

                public int InsertStudentChangeData(int idno, string RFID)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        objParams[1] = new SqlParameter("@P_RFID", RFID);
                        objParams[2] = new SqlParameter("@P_OUTPUT   ", SqlDbType.Int);
                        objParams[2].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("[ACD_INSERT_STUDENT_DATA_RFID]", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else if (Convert.ToInt32(ret) == 2627)
                            retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentController.InsertStudentChangeData -> " + ex.ToString());
                    }

                    return retStatus;
                }

                //added by aashna 21-03-2022
                public DataSet gettabexcel(int sessionno, int collegeid, int degreeno, int branchno, int studtype)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);

                        SqlParameter[] objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_COLLEGE_ID", collegeid);
                        objParams[2] = new SqlParameter("@P_DEGREENO", degreeno);
                        objParams[3] = new SqlParameter("@P_BRANCHNO", branchno);
                        objParams[4] = new SqlParameter("@P_STUD_TYPE", studtype);
                        objParams[5] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[5].Direction = ParameterDirection.Output;
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_EXAM_MARKS_DETAILS", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetDetainedList-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetStudentAllDetails(int admbatch, int ugpg, int CommandType, string StartDate, string EndDate)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);

                        SqlParameter[] objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_ADMBATCH", admbatch);
                        objParams[1] = new SqlParameter("@P_UGPG", ugpg);
                        objParams[2] = new SqlParameter("@P_COMMAND_TYPE", CommandType);
                        objParams[3] = new SqlParameter("@P_START_DATE", StartDate);
                        objParams[4] = new SqlParameter("@P_END_DATE", EndDate);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_STUDENT_COMMON_DETAILS", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentAllDetails-> " + ex.ToString());
                    }

                    return ds;
                }

                //ADD BY AASHNA -22-4-2022
                public DataSet GetRefundAllocationData()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);

                        SqlParameter[] objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_POLICY_ALLOCATION_DATA", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentsForScheme-> " + ex.ToString());
                    }

                    return ds;
                }

                //ADDED BY AASHNA 22-04-2022
                public int UpdateRefundAllocationData(int college_id, string degreeno, string branchno, string semesterno, int ugpg, int affilated, int policy_id, int ua_no, int srno, int REQUEST_TYPE)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[11];

                        objParams[0] = new SqlParameter("@P_COLLEGE_ID", college_id);
                        objParams[1] = new SqlParameter("@P_DEGREENO", degreeno);
                        objParams[2] = new SqlParameter("@P_BRANCHNO", branchno);
                        objParams[3] = new SqlParameter("@P_SEMESTERNO", semesterno);
                        objParams[4] = new SqlParameter("@P_UGPG", ugpg);
                        objParams[5] = new SqlParameter("@P_AFFILIATED_NO", affilated);
                        objParams[6] = new SqlParameter("@P_POLICY_ID", policy_id);
                        objParams[7] = new SqlParameter("@P_MODIFIED_UA_NO", ua_no);
                        objParams[8] = new SqlParameter("@P_SRNO", srno);
                        objParams[9] = new SqlParameter("@P_REQUEST_TYPE", REQUEST_TYPE);
                        objParams[10] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                        objParams[10].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_UPDATE_POLICY_ALLOCATION_DATA", objParams, false);
                        if (ret != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.UpdateStudent_TeachAllot-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int InsertRefundAllocationData(int college_id, string degreeno, string branchno, string semesterno, int ugpg, int affilated, int policy_id, int ua_no, int REQUESTTYPE)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[10];

                        objParams[0] = new SqlParameter("@P_COLLEGE_ID", college_id);
                        objParams[1] = new SqlParameter("@P_DEGREENO", degreeno);
                        objParams[2] = new SqlParameter("@P_BRANCHNO", branchno);
                        objParams[3] = new SqlParameter("@P_SEMESTERNO", semesterno);
                        objParams[4] = new SqlParameter("@P_UGPG", ugpg);
                        objParams[5] = new SqlParameter("@P_AFFILIATED_NO", affilated);
                        objParams[6] = new SqlParameter("@P_POLICY_ID", policy_id);
                        objParams[7] = new SqlParameter("@P_CREATED_UA_NO", ua_no);
                        objParams[8] = new SqlParameter("@P_REQUEST_TYPE", REQUESTTYPE);
                        objParams[9] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                        objParams[9].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INSERT_POLICY_ALLOCATION_DATA", objParams, false);
                        if (ret != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.UpdateStudent_TeachAllot-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet getwithapprovaldataByDirector(int commandtype, string fromdate, string todate, int status, int college_id)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);

                        SqlParameter[] objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_COMMAND_TYPE", commandtype);
                        objParams[1] = new SqlParameter("@P_FROM_DATE", fromdate);
                        objParams[2] = new SqlParameter("@P_TO_DATE", todate);
                        objParams[3] = new SqlParameter("@P_STATUS", status);
                        objParams[4] = new SqlParameter("@P_COLLEGE_ID", college_id);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_WITHDRAWAL_DIRECTOR_APPROVAL_DATA", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentsForScheme-> " + ex.ToString());
                    }

                    return ds;
                }

                public int UpdDetailsByOperator(int idno, int srno, string remark, int refundamt, decimal total_refund, string sturemark, int POLICY, string dcrno)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[9];
                        objParams[0] = new SqlParameter("@P_SRNO", srno);
                        objParams[1] = new SqlParameter("@P_IDNO", idno);
                        objParams[2] = new SqlParameter("@P_OPERATOR_REMARK", remark);
                        objParams[3] = new SqlParameter("@P_REFUND_AMOUNT", refundamt);
                        objParams[4] = new SqlParameter("@P_TOTAL_REFUND_AMOUNT", total_refund);
                        objParams[5] = new SqlParameter("@P_STUDENT_REMARK_BY_O_M_D_F", sturemark);
                        objParams[6] = new SqlParameter("@P_POLICY_NAME", POLICY);
                        objParams[7] = new SqlParameter("@P_DCR_NO", dcrno);
                        objParams[8] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[8].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_UPDATE_DETAILS_BY_OPERATOR", objParams, false);
                        if (ret != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.UpdateStudent_TeachAllot-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet getwithapprovaldataByOperator(int commandtype, string fromdate, string todate, int status, int college_id)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);

                        SqlParameter[] objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_COMMAND_TYPE", commandtype);
                        objParams[1] = new SqlParameter("@P_STATUS", status);
                        objParams[2] = new SqlParameter("@P_FROM_DATE", fromdate);
                        objParams[3] = new SqlParameter("@P_TO_DATE", todate);
                        objParams[4] = new SqlParameter("@P_COLLEGE_ID", college_id);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_WITHDRAWAL_OPERATOR_APPROVAL_DATA", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentsForScheme-> " + ex.ToString());
                    }

                    return ds;
                }

                public DataSet GetRefundAmountDetailsInitial(int idno, int SRNO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);

                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        objParams[1] = new SqlParameter("@P_SRNO", SRNO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_AMOUNT_DETAILS_INITIAL", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentsForScheme-> " + ex.ToString());
                    }

                    return ds;
                }

                //ADD BY AASHNA -05-03-2022
                public int UpdDetailsByDirector(int idno, string remark, int status, int SRNO, int refundamt, decimal total_refund, string sturemark, string dcrno)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[9];
                        objParams[0] = new SqlParameter("@P_STATS_BY_DIRECTOR", status);
                        objParams[1] = new SqlParameter("@P_REMARK_BY_DIRECTOR", remark);
                        objParams[2] = new SqlParameter("@P_SRNO", SRNO);
                        objParams[3] = new SqlParameter("@P_IDNO", idno);
                        objParams[4] = new SqlParameter("@P_REFUND_AMOUNT", refundamt);
                        objParams[5] = new SqlParameter("@P_TOTAL_REFUND_AMOUNT", total_refund);
                        objParams[6] = new SqlParameter("@P_STUDENT_REMARK_BY_O_M_D_F", sturemark);
                        objParams[7] = new SqlParameter("@P_DCR_NO", dcrno);
                        objParams[8] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[8].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_UPDATE_DETAILS_BY_DIRECTOR", objParams, false);
                        if (ret != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.UpdateStudent_TeachAllot-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int UpdateSingleStudentDataFields(
                    int idno, int intake, int campusno, int semesterno, int ptype,
                    string firstName, string middleName, string lastName,
                    string fathersName, string studMob, string studEmail,
                    string colEmail,  DateTime dob, int gender,
                    int old_intake, int old_campusno, int old_semesterno, int old_ptype,
                    string old_firstName, string old_middleName, string old_lastName,
                    string old_fathersName, string old_studMob, string old_studEmail,
                    string old_colEmail,  DateTime old_dob,
                    int old_gender, int modifiedBy
                   )
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        //Add New File
                        objParams = new SqlParameter[29];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        objParams[1] = new SqlParameter("@P_INTAKE", intake);
                        objParams[2] = new SqlParameter("@P_CAMPUSNO", campusno);
                        objParams[3] = new SqlParameter("@P_SEMESTERNO", semesterno);
                        objParams[4] = new SqlParameter("@P_FIRST_NAME", firstName);

                        if (!string.IsNullOrEmpty(middleName))
                            objParams[5] = new SqlParameter("@P_MIDDEL_NAME", middleName);
                        else
                            objParams[5] = new SqlParameter("@P_MIDDEL_NAME", DBNull.Value);

                        objParams[6] = new SqlParameter("@P_LAST_NAME", lastName);
                        if (!string.IsNullOrEmpty(fathersName))
                            objParams[7] = new SqlParameter("@P_FATHERNAME", fathersName);
                        else
                            objParams[7] = new SqlParameter("@P_FATHERNAME", DBNull.Value);

                        if (!string.IsNullOrEmpty(studMob))
                            objParams[8] = new SqlParameter("@P_STUDENTMOBILE", studMob);
                        else
                            objParams[8] = new SqlParameter("@P_STUDENTMOBILE", DBNull.Value);

                        if (!string.IsNullOrEmpty(studEmail))
                            objParams[9] = new SqlParameter("@P_STUDENTEMAIL", studEmail);
                        else
                            objParams[9] = new SqlParameter("@P_STUDENTEMAIL", DBNull.Value);

                        //if (!string.IsNullOrEmpty(colMob))
                        //    objParams[10] = new SqlParameter("@P_COLLEGE_MOB", colMob);
                        //else
                        //    objParams[10] = new SqlParameter("@P_COLLEGE_MOB", DBNull.Value);

                        if (!string.IsNullOrEmpty(colEmail))
                            objParams[10] = new SqlParameter("@P_CLG_EMAIL", colEmail);
                        else
                            objParams[10] = new SqlParameter("@P_CLG_EMAIL", DBNull.Value);

                       // objParams[11] = new SqlParameter("@P_CLG_EMAIL", colEmail);
                        if (dob == DateTime.MinValue)
                            objParams[11] = new SqlParameter("@P_DOB", DBNull.Value);
                        else
                            objParams[11] = new SqlParameter("@P_DOB", dob);

                        objParams[12] = new SqlParameter("@P_GENDER", gender);
                        objParams[13] = new SqlParameter("@P_OLD_INTAKE", old_intake);
                        objParams[14] = new SqlParameter("@P_OLD_CAMPUSNO", old_campusno);
                        objParams[15] = new SqlParameter("@P_OLD_SEMESTERNO", old_semesterno);
                        objParams[16] = new SqlParameter("@P_OLD_FIRST_NAME", old_firstName);

                        if (!string.IsNullOrEmpty(old_middleName))
                            objParams[17] = new SqlParameter("@P_OLD_MIDDEL_NAME", old_middleName);
                        else
                            objParams[17] = new SqlParameter("@P_OLD_MIDDEL_NAME", DBNull.Value);

                        objParams[18] = new SqlParameter("@P_OLD_LAST_NAME", old_lastName);
                        objParams[19] = new SqlParameter("@P_OLD_FATHERNAME", old_fathersName);

                        if (!string.IsNullOrEmpty(old_studMob))
                            objParams[20] = new SqlParameter("@P_OLD_STUDENTMOBILE", old_studMob);
                        else
                            objParams[20] = new SqlParameter("@P_OLD_STUDENTMOBILE", DBNull.Value);

                        if (!string.IsNullOrEmpty(old_studEmail))
                            objParams[21] = new SqlParameter("@P_OLD_STUDENTEMAIL", old_studEmail);
                        else
                            objParams[21] = new SqlParameter("@P_OLD_STUDENTEMAIL", DBNull.Value);

                        //if (!string.IsNullOrEmpty(old_colMob))
                        //    objParams[23] = new SqlParameter("@P_OLD_COLLEGE_MOB", old_colMob);
                        //else
                        //    objParams[23] = new SqlParameter("@P_OLD_COLLEGE_MOB", DBNull.Value);

                        if (!string.IsNullOrEmpty(old_colEmail))
                            objParams[22] = new SqlParameter("@P_OLD_CLG_EMAIL", old_colEmail);
                        else
                            objParams[22] = new SqlParameter("@P_OLD_CLG_EMAIL", DBNull.Value);
                        //objParams[24] = new SqlParameter("@P_OLD_CLG_EMAIL", old_colEmail);

                        if (old_dob == DateTime.MinValue)
                            objParams[23] = new SqlParameter("@P_OLD_DOB", DBNull.Value);
                        else
                            objParams[23] = new SqlParameter("@P_OLD_DOB", old_dob);
                        objParams[24] = new SqlParameter("@P_PTYPE", ptype);
                        objParams[25] = new SqlParameter("@P_OLD_PTYPE", old_ptype);
                        objParams[26] = new SqlParameter("@P_OLD_GENDER", old_gender);
                        objParams[27] = new SqlParameter("@P_UA_NO", modifiedBy);
                        objParams[28] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);

                        objParams[28].Direction = ParameterDirection.Output;

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ACD_UPDATE_SINGLE_STUDENT_DATA", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.UpdateStudentDataFields->" + ex.ToString());
                    }
                    return retStatus;
                }

                private bool Disposed = false;

                ~StudentController()
                {
                    //Destructor Called
                    Dispose(false);
                }

                public void Dispose()
                {
                    Dispose(true);
                    GC.SuppressFinalize(this);
                }

                public void Dispose(bool disposing)
                {
                    if (!Disposed)
                    {
                        if (disposing)
                        {
                            //Called From Dispose
                            //Clear all the managed resources here
                        }
                        else
                        {
                            //Clear all the unmanaged resources here
                        }
                        Disposed = true;
                    }
                }

                //Added by Yograj on 08-03-2022
                public DataSet GetOnlineSearchDetailsByAdmin(string SearchValue, int DynamicFilter)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);

                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_SEARCHVALUE", SearchValue);
                        objParams[1] = new SqlParameter("@P_DYNAMIC_FILTER", DynamicFilter);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_SEARCH_STUDENT_DETAILS", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetOnlineSearchDetailsByAdmin-> " + ex.ToString());
                    }

                    return ds;
                }

                public DataSet GetHitherSemesterdata(int Semester, int degree, int branch, int college_id)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);

                        SqlParameter[] objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_SEMESTERNO", Semester);
                        objParams[1] = new SqlParameter("@P_DEGREENO", degree);
                        objParams[2] = new SqlParameter("@P_BRANCHNO", branch);
                        objParams[3] = new SqlParameter("@P_COLLGE_ID", college_id);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_STUDENT_HIGHRE_SEMESTER", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentsForScheme-> " + ex.ToString());
                    }

                    return ds;
                }

                public int UPDINSERT_COMMITEE_FINALSUBMIT(int Sessionno, int Courseno, int Collegeid, int semesterno, int userno, int borderline, string borderlineFDate, int ExamOff, string ExamOffDate, int withHold, string withHoldDate, string Remark, int FinalSubmitFlag, int Idno, string ccode, int Pageno, decimal Border_Grace)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[18];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", Sessionno);
                        objParams[1] = new SqlParameter("@P_COURSENO", Courseno);
                        objParams[2] = new SqlParameter("@P_COLLEGE_ID", Collegeid);
                        objParams[3] = new SqlParameter("@P_SEMESTERNO", semesterno);
                        objParams[4] = new SqlParameter("@P_ENTRY_BY_UANO", userno);
                        objParams[5] = new SqlParameter("@P_CONSIDER_BORDER", borderline);
                        objParams[6] = new SqlParameter("@P_BORDER_LINE_DATE", borderlineFDate);
                        objParams[7] = new SqlParameter("@P_EXAM_OFFENCE", ExamOff);
                        objParams[8] = new SqlParameter("@P_EXAM_OFFENCE_DATE", ExamOffDate);
                        objParams[9] = new SqlParameter("@P_WITH_HOLD", withHold);
                        objParams[10] = new SqlParameter("@P_WITH_HOLD_DATE", withHoldDate);
                        objParams[11] = new SqlParameter("@P_REMARK", Remark);
                        objParams[12] = new SqlParameter("@P_EXAM_OFFENCE_FLAG", FinalSubmitFlag);
                        objParams[13] = new SqlParameter("@P_IDNO", Idno);
                        objParams[14] = new SqlParameter("@P_PAGENO", Pageno);
                        objParams[15] = new SqlParameter("@P_CCODE", ccode);
                        objParams[16] = new SqlParameter("@P_BORDER_GRACE", Border_Grace);
                        objParams[17] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[17].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_SP_UPD_MARKENTRY_BY_COMMITTEE", objParams, false);
                        if (ret != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.UpdateStudent_TeachAllot-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int UPDINSERT_COMMITEE_LOCK(int Sessionno, int Courseno, int Collegeid, int semesterno, int userno, int PAGENO)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[7];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", Sessionno);
                        objParams[1] = new SqlParameter("@P_COURSENO", Courseno);
                        objParams[2] = new SqlParameter("@P_COLLEGE_ID", Collegeid);
                        objParams[3] = new SqlParameter("@P_SEMESTERNO", semesterno);
                        objParams[4] = new SqlParameter("@P_ENTRY_BY_UANO", userno);
                        objParams[5] = new SqlParameter("@P_PAGENO", PAGENO);

                        objParams[6] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[6].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_SP_UPD_MARKENTRY_BY_COMMITTEE_UNLOCK", objParams, false);
                        if (ret != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.UpdateStudent_TeachAllot-> " + ex.ToString());
                    }
                    return retStatus;
                }

                //ADDED BY AASHNA 15-07-2022
                public DataSet NewStudentDetails(string search, string category)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_SEARCHSTRING", search);
                        objParams[1] = new SqlParameter("@P_SEARCH", category);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STUDENT_SP_SEARCH_NEW_STUDENT", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.EmpCreateController.RetrieveEmpDetails-> " + ex.ToString());
                    }
                    return ds;
                }

                //ADDED BY AASHNA 15-07-2022
                public DataSet GetNewStudentDetails(int idno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_USERNO", idno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_USER_DETAILS_SELECT_NEW_STUDENT", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.EmpCreateController.RetrieveEmpDetails-> " + ex.ToString());
                    }
                    return ds;
                }

                //ADDED BY AASHNA 15-07-2022
                public DataSet GetDocumentNewStudentDetails(int idno, int admbatch, int degreeno, int branchno, int collegeid)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_ADMBATCH", admbatch);
                        objParams[1] = new SqlParameter("@P_BRANCHNO", branchno);
                        objParams[2] = new SqlParameter("@P_COLLEGE_ID", collegeid);
                        objParams[3] = new SqlParameter("@P_DEGREENO", degreeno);
                        objParams[4] = new SqlParameter("@P_USERNO", idno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_DOCUMENTENTRY_SEL_STUD_NEW", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.EmpCreateController.RetrieveEmpDetails-> " + ex.ToString());
                    }
                    return ds;
                }

                public int SubmitNewStudentAllDetails(Student objStudent, string grade1, string enrollmentNo, string amt, string receipt, byte[] challanCopy, string filename, string bankname, string bankno, string orderid, string ALINDEX, string OLINDEX, decimal APTIMARKS, string alschool, string olschool, int source, int idno, string FULLNAME, string FatherName, string FMobileNo, string PEmail, int FMobileCode, string Laddress, string Lcountry, string Lprovince, string Ldistrict)
                {
                    //  int retStatus = Convert.ToInt32(CustomStatus.Others);
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[78];
                        //Personal Details
                        objParams[0] = new SqlParameter("@P_STUDFIRSTNAME", objStudent.studfirstname);
                        objParams[1] = new SqlParameter("@P_STUDLASTNAME", objStudent.studlastname);
                        objParams[2] = new SqlParameter("@P_NAME_WITH_INITIAL", objStudent.name_with_initial);
                        objParams[3] = new SqlParameter("@P_EMAILID", objStudent.emailid);
                        objParams[4] = new SqlParameter("@P_STUDENTMOBILE", objStudent.studmobile);
                        objParams[5] = new SqlParameter("@P_TELPHONENO", objStudent.telphoneno);
                        objParams[6] = new SqlParameter("@P_NICNO", objStudent.nicno);
                        objParams[7] = new SqlParameter("@P_PASSPORTNO", objStudent.passportno);
                        objParams[8] = new SqlParameter("@P_DOB", objStudent.stud_dob);
                        objParams[9] = new SqlParameter("@P_SEX", objStudent.stud_sex);
                        objParams[10] = new SqlParameter("@P_CITIZENSHIP", objStudent.stud_citizenship);
                        objParams[11] = new SqlParameter("@P_IDENTI_MARK", objStudent.identi_mark);
                        //Address
                        objParams[12] = new SqlParameter("@P_PADDRESS", objStudent.paddress);
                        objParams[13] = new SqlParameter("@P_PCOUNTRY", objStudent.pcountry);
                        objParams[14] = new SqlParameter("@P_PSTATE", objStudent.pprovince);
                        objParams[15] = new SqlParameter("@P_PDISTRICT", objStudent.pdistrict);
                        //Education Details
                        objParams[16] = new SqlParameter("@P_ALTYPE", objStudent.altype);
                        objParams[17] = new SqlParameter("@P_ALSTREAMNO", objStudent.alstreamno);
                        objParams[18] = new SqlParameter("@P_ALATTEMPTNO", objStudent.alattemptno);
                        objParams[19] = new SqlParameter("@P_ALSUBJECT1", objStudent.alsubject1);
                        objParams[20] = new SqlParameter("@P_ALGRADE1", grade1);
                        objParams[21] = new SqlParameter("@P_ALSUBJECT2", objStudent.alsubject2);
                        objParams[22] = new SqlParameter("@P_ALGRADE2", objStudent.algrade2);
                        objParams[23] = new SqlParameter("@P_ALSUBJECT3", objStudent.alsubject3);
                        objParams[24] = new SqlParameter("@P_ALGRADE3", objStudent.algrade3);
                        objParams[25] = new SqlParameter("@P_ALSUBJECT4", objStudent.alsubject4);
                        objParams[26] = new SqlParameter("@P_ALGRADE4", objStudent.algrade4);
                        objParams[27] = new SqlParameter("@P_OLTYPE", objStudent.oltype);
                        objParams[28] = new SqlParameter("@P_OLSTREAMNO", objStudent.olstreamno);
                        objParams[29] = new SqlParameter("@P_OLATTEMPTNO", objStudent.olattemptno);
                        objParams[30] = new SqlParameter("@P_OLSUBJECT1", objStudent.olsubject1);
                        objParams[31] = new SqlParameter("@P_OLGRADE1", objStudent.olgrade1);
                        objParams[32] = new SqlParameter("@P_OLSUBJECT2", objStudent.olsubject2);
                        objParams[33] = new SqlParameter("@P_OLGRADE2", objStudent.olgrade2);
                        objParams[34] = new SqlParameter("@P_OLSUBJECT3", objStudent.olsubject3);
                        objParams[35] = new SqlParameter("@P_OLGRADE3", objStudent.olgrade3);
                        objParams[36] = new SqlParameter("@P_OLSUBJECT4", objStudent.olsubject4);
                        objParams[37] = new SqlParameter("@P_OLGRADE4", objStudent.olgrade4);
                        objParams[38] = new SqlParameter("@P_OLSUBJECT5", objStudent.olsubject5);
                        objParams[39] = new SqlParameter("@P_OLGRADE5", objStudent.olgrade5);
                        objParams[40] = new SqlParameter("@P_OLSUBJECT6", objStudent.olsubject6);
                        objParams[41] = new SqlParameter("@P_OLGRADE6", objStudent.olgrade6);
                        //Faculty Details
                        objParams[42] = new SqlParameter("@P_COLLEGE_ID", objStudent.stud_college_id);
                        objParams[43] = new SqlParameter("@P_DEGREENO", objStudent.degreeno);
                        objParams[44] = new SqlParameter("@P_BRANCHNO", objStudent.branchno);
                        objParams[45] = new SqlParameter("@P_CAMPUSNO", objStudent.campusno);
                        objParams[46] = new SqlParameter("@P_WEEKSNOS", objStudent.weeksnos);
                        objParams[47] = new SqlParameter("@P_SEMESTERNO", objStudent.semesterno);
                        objParams[48] = new SqlParameter("@P_ADMBATCH", objStudent.admbatch);
                        objParams[49] = new SqlParameter("@P_UA_NO", objStudent.ua_no);

                        objParams[50] = new SqlParameter("@P_UA_PWD", objStudent.password);
                        objParams[51] = new SqlParameter("@P_REMARK", objStudent.stud_remark);
                        objParams[52] = new SqlParameter("@P_MOBILECODE", objStudent.mobilecode);
                        objParams[53] = new SqlParameter("@P_HOMETELEPHONECODE", objStudent.hometelephonecode);
                        objParams[54] = new SqlParameter("@P_ENROLLMENTNO", enrollmentNo);
                        objParams[55] = new SqlParameter("@P_AMOUNT", amt);
                        objParams[56] = new SqlParameter("@P_RECEIPT_NO", receipt);
                        objParams[57] = new SqlParameter("@P_DOC_FILENAME", filename);
                        //if (challanCopy == null)
                        //{
                        //    objParams[58] = new SqlParameter("@P_CHALLAN_COPY", DBNull.Value);
                        //}
                        //else
                        //{
                        //    objParams[58] = new SqlParameter("@P_CHALLAN_COPY", challanCopy);
                        //}
                        //if (challanCopy != null)
                        //{
                        //    objParams[58] = new SqlParameter("@P_CHALLAN_COPY", challanCopy);
                        //}
                        //else
                        //{
                        //    objParams[58] = new SqlParameter("@P_CHALLAN_COPY", DBNull.Value);
                        //}
                        objParams[58] = new SqlParameter("@P_BANK_NO", bankno);
                        objParams[59] = new SqlParameter("@P_BANK_NAME", bankname);
                        objParams[60] = new SqlParameter("@P_ORDER_ID", orderid);
                        objParams[61] = new SqlParameter("@P_ALINDEX", ALINDEX);
                        objParams[62] = new SqlParameter("@P_OLINDEX", OLINDEX);
                        objParams[63] = new SqlParameter("@P_APTIMARKS", APTIMARKS);
                        objParams[64] = new SqlParameter("@P_ALSCHOOL_NAME", alschool);
                        objParams[65] = new SqlParameter("@P_OLSCHOOL_NAME", olschool);
                        objParams[66] = new SqlParameter("@P_SOURCETYPENO", source);
                        objParams[67] = new SqlParameter("@P_IDNO", idno);
                        objParams[68] = new SqlParameter("@P_STUDNAME", FULLNAME);
                        objParams[69] = new SqlParameter("@P_FATHERNAME", FatherName);
                        objParams[70] = new SqlParameter("@P_FATHERMOBILE", FMobileNo);
                        objParams[71] = new SqlParameter("@P_FATHEREMAIL", PEmail);
                        objParams[72] = new SqlParameter("@P_FATHERCODE", FMobileCode);
                        objParams[73] = new SqlParameter("@P_LADDRESS", Laddress);
                        objParams[74] = new SqlParameter("@P_LCOUNTRY", Lcountry);
                        objParams[75] = new SqlParameter("@P_LSTATE", Lprovince);
                        objParams[76] = new SqlParameter("@P_LDISTRICT", Ldistrict);
                        objParams[77] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[77].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INSERT_NEW_STUDENT_ALL_DETAILS", objParams, true);

                        retStatus = Convert.ToInt32(ret);
                        if (Convert.ToInt32(ret) == 2627)
                            retStatus = (Convert.ToInt32(ret));
                        else if (ret != null && ret.ToString() != "-99" && ret.ToString() != "-1001")
                            retStatus = (Convert.ToInt32(ret));
                        else
                            retStatus = (Convert.ToInt32(ret));
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.SubmitNewStudentAllDetails-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetStudentTransferIntake(int admbatch, int degree, int branch, int college_id)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);

                        SqlParameter[] objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_INTAKE", admbatch);
                        objParams[1] = new SqlParameter("@P_DEGREENO", degree);
                        objParams[2] = new SqlParameter("@P_BRANCHNO", branch);
                        objParams[3] = new SqlParameter("@P_COLLGE_ID", college_id);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_TRANSFER_INTAKE_STUDENT_SUMMERY_REPORT", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentTransferIntake-> " + ex.ToString());
                    }

                    return ds;
                }

                public int AddWIthheldStudentRemoval(Student objStud)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[10];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", objStud.SessionNo);
                        objParams[1] = new SqlParameter("@P_IDNO", objStud.IDNO);
                        objParams[2] = new SqlParameter("@P_COURSENO", objStud.Courseno);
                        objParams[3] = new SqlParameter("@P_SEMSETERNO", objStud.semesterno);
                        objParams[4] = new SqlParameter("@P_SCHEMENO", objStud.SchemeNo);
                        objParams[5] = new SqlParameter("@P_COLLEGE_ID", objStud.Collegeid);
                        objParams[6] = new SqlParameter("@P_CCODE", objStud.Ccode);
                        objParams[7] = new SqlParameter("@P_UA_NO", objStud.ua_no);
                        objParams[8] = new SqlParameter("@P_REMARK", objStud.Remark);
                        objParams[9] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[9].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_INSERT_WITH_HELD_STUDENT_STATUS", objParams, true);

                        if (Convert.ToInt32(ret) == 1)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.AddWIthheldStudentRemoval-> " + ex.ToString());
                    }
                    return retStatus;
                }

                //ADDEDD BY AASHNA 16/08-2022
                public DataSet Couresewiseresult(int idno, int SemesterNo, int SessionNo, int couresno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        objParams[1] = new SqlParameter("@P_SEMESTERNO", SemesterNo);
                        objParams[2] = new SqlParameter("@P_SESSIONNO", SessionNo);
                        objParams[3] = new SqlParameter("@P_COURSENO", couresno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_STUD_CURRENT_RESULT_FOR_GRADE_SK_NEW", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.RetrieveStudentCurrentRegDetails-> " + ex.ToString());
                    }
                    return ds;
                }

                //Added by Bhagyashree 27-08-2022
                public DataSet GetStudentShiftData(int admbatch, int collegeid, string degreeno, string branchno, string affiliatedno, int excel)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_ADMBATCH", admbatch);
                        objParams[1] = new SqlParameter("@P_COLLEGE_ID", collegeid);
                        objParams[2] = new SqlParameter("@P_DEGREENO", degreeno);
                        objParams[3] = new SqlParameter("@P_BRANCHNO", branchno);
                        objParams[4] = new SqlParameter("@P_AFFILIATED_NO", affiliatedno);
                        objParams[5] = new SqlParameter("@P_EXCEL", excel);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_STUDENT_SHIFT_STUDENT_DATA", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.GetStudentShiftData.RetrieveEmpDetails-> " + ex.ToString());
                    }
                    return ds;
                }

                //Added by Bhagyashree 27-08-2022
                public int AddStudentShiftData(string idno, int admbatch, int collegeid, string degreeno, string branchno, string affiliatedno, int studentshift, int ua_no)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[9];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        objParams[1] = new SqlParameter("@P_ADMBATCH", admbatch);
                        objParams[2] = new SqlParameter("@P_COLLEGE_ID", collegeid);
                        objParams[3] = new SqlParameter("@P_DEGREENO", degreeno);
                        objParams[4] = new SqlParameter("@P_BRANCHNO", branchno);
                        objParams[5] = new SqlParameter("@P_AFFILIATED_NO", affiliatedno);
                        objParams[6] = new SqlParameter("@P_STUDENTSHIFT", studentshift);
                        objParams[7] = new SqlParameter("@P_UA_NO", ua_no);
                        objParams[8] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[8].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_ADD_STUDNT_SHIFT", objParams, true);

                        if (Convert.ToInt32(ret) == 2)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.AddStudentShiftData-> " + ex.ToString());
                    }
                    return retStatus;
                }

                //added by aashna 01-10-2022
                public DataSet CouresewiseresultExternal(int idno, int SemesterNo, int SessionNo, int couresno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        objParams[1] = new SqlParameter("@P_SEMESTERNO", SemesterNo);
                        objParams[2] = new SqlParameter("@P_SESSIONNO", SessionNo);
                        objParams[3] = new SqlParameter("@P_COURSENO", couresno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_STUD_CURRENT_RESULT_FOR_GRADE_SK_NEW_FINAL", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.RetrieveStudentCurrentRegDetails-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet RetrieveStudentCurrentResultFORGRADEEX(int idno, int SemesterNo, int SessionNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        objParams[1] = new SqlParameter("@P_SEMESTERNO", SemesterNo);
                        objParams[2] = new SqlParameter("@P_SESSIONNO", SessionNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_STUD_CURRENT_RESULT_FOR_GRADE_EX", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.RetrieveStudentCurrentRegDetails-> " + ex.ToString());
                    }
                    return ds;
                }

                //added by aashna 17-10-2022
                public DataSet GetEnrollCount(int admbatch)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_INTAKE", admbatch);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_ENROLL_CONF_DATA", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.EmpCreateController.RetrieveEmpDetails-> " + ex.ToString());
                    }
                    return ds;
                }

                //added by aashna 11-11-2022
                public int Insert_Slot_Booking(int idno, string activityno, string slotname, string START, string END, string SLOTNO)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[7];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        objParams[1] = new SqlParameter("@P_ACTIVITYNO", activityno);
                        objParams[2] = new SqlParameter("@P_SLOTNAME", slotname);
                        objParams[3] = new SqlParameter("@P_START", START);
                        objParams[4] = new SqlParameter("@P_END", END);
                        objParams[5] = new SqlParameter("@P_SLOTNO", SLOTNO);
                        objParams[6] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                        objParams[6].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INSERT_SLOT_BOOKING", objParams, true);

                        if (Convert.ToInt32(ret) == 1)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.Insert_Update_StudentDocuments-> " + ex.ToString());
                    }
                    return retStatus;
                }

                //added by aashna 11-11-2022
                public DataSet GetSlot_Booking()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_SLOT_BOOKING", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentDetails->" + ex.ToString());
                    }
                    return ds;
                }

                //added by aashna 11-11-2022
                public int Update_Slot_Booking(int idno, string activityno, string slotname, int slotno)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        objParams[1] = new SqlParameter("@P_ACTIVITYNO", activityno);
                        objParams[2] = new SqlParameter("@P_SLOTNAME", slotname);
                        objParams[3] = new SqlParameter("@P_SLOT_BOOK_NO", slotno);
                        objParams[4] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                        objParams[4].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_UPDATE_SLOT_BOOKING", objParams, true);

                        if (Convert.ToInt32(ret) == 1)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.Insert_Update_StudentDocuments-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetAdmissionCancelStudentData(string FromDate, string ToDate, int degreeno, int branchno, int collegeid)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objDataAccess = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] sqlParams = new SqlParameter[]
                    {
                        new SqlParameter("@P_START_DATE ", FromDate),
                        new SqlParameter("@P_END_DATE ", ToDate),
                        new SqlParameter("@P_DEGREENO ", degreeno),
                        new SqlParameter("@P_COLLEGE_ID ", collegeid),
                        new SqlParameter("@P_BRANCHNO ", branchno)
                    };
                        ds = objDataAccess.ExecuteDataSetSP("PKG_ACAD_ADMISSIONCANCEL_BRANCH_REPORT_EXCEL", sqlParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.GetStudentInfoDocumentListByEnrollNo() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }

                public DataSet GetAdmissionCancelStudentInfo(string ENROLLNO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objDataAccess = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_ENROLLNO ", ENROLLNO)
                };
                        ds = objDataAccess.ExecuteDataSetSP("PKG_ADMISSION_CANCEL_STUDENT_SHOW", sqlParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.GetStudentInfoDocumentListByEnrollNo() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }

                public int UpdateBulkStudentPhoto(int idno, byte[] photo, string PhotoPath)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        if (!(photo == null))
                        {
                            SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                            SqlParameter[] objParams = new SqlParameter[3];
                            objParams[0] = new SqlParameter("@P_IDNO", idno);
                            objParams[1] = new SqlParameter("@P_PHOTO", photo);
                            objParams[2] = new SqlParameter("@P_PHOTOPATH", PhotoPath);

                            if (objSQLHelper.ExecuteNonQuerySP("PKG_ACD_UPDATE_BULK_PHOTO", objParams, false) != null)
                                retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        }
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.UpdateStudentPhoto->" + ex.ToString());
                    }

                    return retStatus;
                }


                public int bulkStudentSemDemotion(Student objStudent, string IDNO,int Ua_no)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        //Update Student semester Rtm
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_IDNO", IDNO);
                        objParams[1] = new SqlParameter("@P_SEMESTERNO", objStudent.SemesterNo);
                        objParams[2] = new SqlParameter("@P_UANO", Ua_no);
                        objParams[3] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[3].Direction = ParameterDirection.Output;

                        int status = (Int32)objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_INS_SEM_DEMOTION_NEW", objParams, true);
                        if (status == 1)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        else if (status == 2627)
                            retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.Error);
                        return retStatus;
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.UpdateStudent-> " + ex.ToString());
                    }
                }
                public int SaveExcelSheetDataInDataBase(Student objstud, int ADMBATCH)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);

                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[20];
                        objParams[0] = new SqlParameter("@P_REGNO", objstud.RegNo);
                        objParams[1] = new SqlParameter("@P_ROLLNO", objstud.RollNo);
                        objParams[2] = new SqlParameter("@P_SEMESTERNO", objstud.SemesterNo);
                        objParams[3] = new SqlParameter("@P_YEAR", objstud.Year);
                        objParams[4] = new SqlParameter("@P_STUDNAME", objstud.StudName);
                        objParams[5] = new SqlParameter("@P_FATHERNAME", objstud.FatherName);
                        objParams[6] = new SqlParameter("@P_STUDENTMOBILE", objstud.StudentMobile);
                        objParams[7] = new SqlParameter("@P_EMILID", objstud.EmailID);
                        objParams[8] = new SqlParameter("@P_COLLEGE_ID", objstud.Collegeid);
                        objParams[9] = new SqlParameter("@P_DEGREENO", objstud.DegreeNo);
                        objParams[10] = new SqlParameter("@P_BRANCHNO", objstud.BranchNo);
                        if (objstud.AdmDate == DateTime.MinValue)
                            objParams[11] = new SqlParameter("@P_ADM_DATE", DBNull.Value);
                        else
                            objParams[11] = new SqlParameter("@P_ADM_DATE", objstud.AdmDate);
                        objParams[12] = new SqlParameter("@P_IDTYPENO", objstud.IdType);
                        objParams[13] = new SqlParameter("@P_SEX", objstud.Sex);
                        objParams[14] = new SqlParameter("@P_ADMBATCH", ADMBATCH);
                        objParams[15] = new SqlParameter("@P_FIRST_NAME", objstud.firstName);
                        objParams[16] = new SqlParameter("@P_MIDDLE_NAME", objstud.MiddleName);
                        objParams[17] = new SqlParameter("@P_LAST_NAME", objstud.LastName);
                        objParams[18] = new SqlParameter("@P_SUFFIX", objstud.name_with_initial);
                        objParams[19] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[19].Direction = ParameterDirection.Output;

                        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_UPLOAD_STUD_ADMISSION_DATA_EXCEL", objParams, true);
                        if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-999")
                        {
                            if (obj.ToString() == "1")
                                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                            else if (obj.ToString() == "-1001")
                                retStatus = Convert.ToInt32(CustomStatus.DuplicateRecord);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.Error);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LMController.SaveExcelSheetDataInDataBase() -> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet RetrieveMasterData()
                {
                    DataSet ds = null;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[0];

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_MASTER_DATA", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetAllSubModuleDetails-> " + ex.ToString());
                    }

                    return ds;
                }

                public DataSet GetUserParentCreationList(int admbatch, int collegeid, int degreeno, int branchno, int semesterno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_ADMBATCH", admbatch);
                        objParams[1] = new SqlParameter("@P_COLLEGE_ID", collegeid);
                        objParams[2] = new SqlParameter("@P_DEGREENO", degreeno);
                        objParams[3] = new SqlParameter("@P_BRANCHNO", branchno);
                        objParams[4] = new SqlParameter("@P_SEMESTERNO", semesterno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_PARENT_USERCREATION_LIST", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetUserParentCreationList->" + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetUserCreationList(int admbatch, int collegeid, int degreeno, int branchno, int semesterno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_ADMBATCH", admbatch);
                        objParams[1] = new SqlParameter("@P_COLLEGE_ID", collegeid);
                        objParams[2] = new SqlParameter("@P_DEGREENO", degreeno);
                        objParams[3] = new SqlParameter("@P_BRANCHNO", branchno);
                        objParams[4] = new SqlParameter("@P_SEMESTERNO", semesterno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_STUDENT_USERCREATION_LIST", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetUserCreationList->" + ex.ToString());
                    }
                    return ds;
                }

                public int UpdateStudentSectionManual(string studids, string rollnos, string sectionnos, int ua_no, int admbatchno, int degreeno, int branchno, string ipaddress)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[9];
                        objParams[0] = new SqlParameter("@P_STUDID", studids);
                        objParams[1] = new SqlParameter("@P_SECTIONNO", sectionnos);
                        objParams[2] = new SqlParameter("@P_ROLLNOS", rollnos);
                        objParams[3] = new SqlParameter("@P_UA_NO", ua_no);
                        objParams[4] = new SqlParameter("@P_ADMBATCHNO", admbatchno);
                        objParams[5] = new SqlParameter("@P_DEGREENO", degreeno);
                        objParams[6] = new SqlParameter("@P_BRANCHNO", branchno);
                        objParams[7] = new SqlParameter("@P_IPADDRESS", ipaddress);
                        objParams[8] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[8].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_STUDENT_SP_UPD_SECTION_ALLOTMENT_MANUAL", objParams, false);
                        if (ret != null)
                            if (ret.ToString() != "-99")
                                retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.UpdateStudentSection-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int UpdateStudentCourseWiseSectionManual(int sessiono, int college_id, int degreeno, int branchno, int schemeno, int semesterno, int courseno, string studids, string sectionnos, int userno)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[11];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessiono);
                        objParams[1] = new SqlParameter("@P_COLLEGE_ID", college_id);
                        objParams[2] = new SqlParameter("@P_DEGREENO", degreeno);
                        objParams[3] = new SqlParameter("@P_BRANCHNO", branchno);
                        objParams[4] = new SqlParameter("@P_SCHEMENO", schemeno);
                        objParams[5] = new SqlParameter("@P_SEMESTERNO", semesterno);
                        objParams[6] = new SqlParameter("@P_COURSENO", courseno);
                        objParams[7] = new SqlParameter("@P_STUDIDS", studids);
                        objParams[8] = new SqlParameter("@P_SECTIONNO", sectionnos);
                        objParams[9] = new SqlParameter("@P_UA_NO", userno);
                        objParams[10] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[10].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_STUDENT_UPDATE_COURSE_WISE_SECTION_ALLOTMENT", objParams, false);
                        if (ret != null)
                            if (ret.ToString() != "-99")
                                retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.UpdateStudentCourseWiseSection-> " + ex.ToString());
                    }
                    return retStatus;
                }

                //added by aashna 06-05-2023
                public DataSet GetApplicationDetailsInterviw_Transfree(int intake, string StudyType, string programNos, string branchNos, int progresslevel)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_INTAKE", intake);
                        objParams[1] = new SqlParameter("@P_STUDYLEVELNOS", StudyType);
                        objParams[2] = new SqlParameter("@P_PROGRAMNOS", programNos);
                        objParams[3] = new SqlParameter("@P_BRANCHNOS", branchNos);
                        objParams[4] = new SqlParameter("@P_PROGRESS_LEVEL", progresslevel);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_INTERVIEW_APPLICATION_DETAILS_SHOW_TRANSFERE", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic-> " + ex.ToString());
                    }
                    return ds;
                }

                //ADDED BY AASHNA 04-07-2023
                public DataSet GetOnlineSearchDetailsForDataCorrection(string SearchValue, int DynamicFilter)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);

                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_SEARCHVALUE", SearchValue);
                        objParams[1] = new SqlParameter("@P_DYNAMIC_FILTER", DynamicFilter);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_SEARCH_STUDENT_DETAILS_FOR_DATA_CORRECTION", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetOnlineSearchDetailsByAdmin-> " + ex.ToString());
                    }

                    return ds;
                }

                public DataSet GetStudentDetailsofDataCorrection(int studentId)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);

                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_USERNO", studentId);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_DATA_COREECTION_FACILITY_DATA", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetOnlineSearchDetailsByAdmin-> " + ex.ToString());
                    }

                    return ds;
                }

                public DataSet BindAllDropDown(string DynamicParameter)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_DYNAMICPARAMETER", DynamicParameter);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ONLINE_ADMISSION_BIND_ALL_DROPDOWN_LIST", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet Getdetailsofbranch(int ugpg, int USERNO, int Campusno, int Discipline, int AFFILIATED_NO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_UGPG", ugpg);
                        objParams[1] = new SqlParameter("@P_USERNO", USERNO);
                        objParams[2] = new SqlParameter("@P_CAMPUSNO", Campusno);
                        objParams[3] = new SqlParameter("@P_DISCIPLINE", Discipline);
                        objParams[4] = new SqlParameter("@P_AFFILIATED", AFFILIATED_NO);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_UGPG_DATA_FORPROGRAM", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.BranchController.GetAllBranchDetails-> " + ex.ToString());
                    }
                    return ds;
                }

                public int UpdateUserEdcucationalDetails(int UserNo, string NameOfSchool, string Address, string Region, string YearAttended, string Type, string TypeNo, string StudyLevel, int Ugpg)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[10];

                        objParams[0] = new SqlParameter("@P_USERNO", UserNo);
                        objParams[1] = new SqlParameter("@P_SCHOOL_NAME", NameOfSchool);
                        objParams[2] = new SqlParameter("@P_SCHOOL_ADDRESS", Address);
                        objParams[3] = new SqlParameter("@P_SCHOOL_REGION", Region);
                        objParams[4] = new SqlParameter("@P_YEAR_ATTENDED", YearAttended);
                        objParams[5] = new SqlParameter("@P_SCHOOL_TYPE", Type);
                        objParams[6] = new SqlParameter("@P_SCHOOL_TYPE_NO", TypeNo);
                        objParams[7] = new SqlParameter("@P_UGPGOT", StudyLevel);
                        objParams[8] = new SqlParameter("@P_NUGPGOT", Ugpg);
                        objParams[9] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[9].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_UPDATE_STUDENT_EDUCATION_DETAILS_DATA_ENTRY", objParams, true);

                        if (ret.ToString() == "1" && ret != null)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else if (ret.ToString() == "2")
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        }
                        else if (ret.ToString() == "3")
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordDeleted);
                        }
                        else if (ret.ToString() == "4")
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordFound);
                        }
                        else if (ret.ToString() == "-99")
                        {
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int UpdateDataCorrection(int USERNO, int COMMNDTYPE, string FIRSTNAME, string MIDLLENAME, string LASTNAME, int GENDER,
int Religion, DateTime Dob, string PlaceofBirth, int Citizenno, string Mobile, string EmailID, string AcrNo, string AcrDate, string PlaceIsuue,
string PassNo, string fatherName, int FatherOccNo, string MotherName, int MotherOccuNO, int InCOme, string caddress, int Ccountry, int Cprovience,
int Ccity, string Czip, string Paddress, int PCounty, int PState, int Pcity, string Ppin, int STUDENTTYPE, int uano, int monilecode)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[35];
                        objParams[0] = new SqlParameter("@P_USERNO", USERNO);
                        objParams[1] = new SqlParameter("@P_COMMAND_TYPE", COMMNDTYPE);
                        objParams[2] = new SqlParameter("@P_FIRSTNAME", FIRSTNAME);
                        objParams[3] = new SqlParameter("@P_MIDDLENAME", MIDLLENAME);
                        objParams[4] = new SqlParameter("@P_LASTNAME", LASTNAME);
                        objParams[5] = new SqlParameter("@P_GENDER", GENDER);
                        objParams[6] = new SqlParameter("@P_RELIGIONNO", Religion);
                        objParams[7] = new SqlParameter("@P_DOB", Dob);
                        objParams[8] = new SqlParameter("@P_PLCAOFBIRTH", PlaceofBirth);
                        objParams[9] = new SqlParameter("@P_CITIZEN_NO", Citizenno);
                        objParams[10] = new SqlParameter("@P_MOBILE", Mobile);
                        objParams[11] = new SqlParameter("@P_EMAILID", EmailID);
                        objParams[12] = new SqlParameter("@P_ACRNO", AcrNo);
                        objParams[13] = new SqlParameter("@P_ACR_DATE_ISSUE", AcrDate);
                        objParams[14] = new SqlParameter("@P_ACR_PLACE_OF_ISSUE", PlaceIsuue);
                        objParams[15] = new SqlParameter("@P_PASSPORTNO", PassNo);
                        objParams[16] = new SqlParameter("@P_FATHERNAME", fatherName);
                        objParams[17] = new SqlParameter("@P_FATHER_OCCUPATIONNO", FatherOccNo);
                        objParams[18] = new SqlParameter("@P_MOTHERNAME", MotherName);
                        objParams[19] = new SqlParameter("@P_MOTHER_OCCUPATIONNO", MotherOccuNO);
                        objParams[20] = new SqlParameter("@P_HOUSEHOLD_INCOMENO", InCOme);
                        objParams[21] = new SqlParameter("@P_CADDRESS", caddress);
                        objParams[22] = new SqlParameter("@P_CCOUNTRY", Ccountry);
                        objParams[23] = new SqlParameter("@P_CPROVIENCE", Cprovience);
                        objParams[24] = new SqlParameter("@P_CCITY", Ccity);
                        objParams[25] = new SqlParameter("@P_CPIN", Czip);
                        objParams[26] = new SqlParameter("@P_PADDRESS", Paddress);
                        objParams[27] = new SqlParameter("@P_PCOUNTRY", PCounty);
                        objParams[28] = new SqlParameter("@P_PPROVIENCE", PState);
                        objParams[29] = new SqlParameter("@P_PCITY", Pcity);
                        objParams[30] = new SqlParameter("@P_PPIN", Ppin);
                        objParams[31] = new SqlParameter("@P_STUDENT_TYPE", STUDENTTYPE);
                        objParams[32] = new SqlParameter("@P_UANO", uano);
                        objParams[33] = new SqlParameter("@P_MOBILECODE", monilecode);
                        objParams[34] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                        objParams[34].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_UPDATE_DATA_CORRECTION_FACITTY_DATA", objParams, false);
                        if (ret != null)
                            if (ret.ToString() != "-99")
                                retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.UpdateStudentSection-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int UpdateBranchPreferenceDataEntry(User objUR, string CollegeId, String DegreeNo, string Branchno, string Area, string campusNos, string Admbatchs, string Branch_Pref, int uano, int Ugpg)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        //Add Branch change data
                        objParams = new SqlParameter[14];
                        objParams[0] = new SqlParameter("@P_USERNO", objUR.USERNO);
                        objParams[1] = new SqlParameter("@P_PROGRAM_TYPE", objUR.UGPG);
                        objParams[2] = new SqlParameter("@P_AREAOFINTEREST", Area);
                        objParams[3] = new SqlParameter("@P_COLLEGE_ID", CollegeId);
                        objParams[4] = new SqlParameter("@P_DEGREENO", DegreeNo);
                        objParams[5] = new SqlParameter("@P_BRANCHNO", Branchno);
                        objParams[6] = new SqlParameter("@P_CAMPUSNO", objUR.Campus);
                        objParams[7] = new SqlParameter("@P_WEEKDAYS", objUR.WeekDays);
                        objParams[8] = new SqlParameter("@P_CAMPUSNOS", campusNos);
                        objParams[9] = new SqlParameter("@P_ADMBATCHS", Admbatchs);
                        objParams[10] = new SqlParameter("@P_BRANCH_PREF", Branch_Pref);
                        objParams[11] = new SqlParameter("@P_UANO", uano);
                        objParams[12] = new SqlParameter("@P_UGPG", Ugpg);
                        objParams[13] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                        objParams[13].Direction = ParameterDirection.Output;

                        object ret = (objSQLHelper.ExecuteNonQuerySP("PKG_USER_UPDATE_BRANCH_PREF_USA_ERP_DATA_CORRECTION", objParams, true));

                        if (ret.ToString() == "1" && ret != null)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        if (ret.ToString() == "4" && ret != null)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordFound);
                        }
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.BranchController.AddBranchPreference-> " + ex.ToString());
                    }
                    return retStatus;
                }
                public DataSet GetStudentPreference(int Commandtype, int UserNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[]
                  {
                    new SqlParameter("@P_COMMANDTYPE",Commandtype),
                    new SqlParameter("@P_USERNO",UserNo)
                  };
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_ADMP_GET_STUDENT_APLICATION_PREFE", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.plicationController.GetRecordByUserno() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }

                public string AddBranchPreferenceTranferee(int UserNo, string CollegeId, string DegreeNo, string Branchno, string Area, string Branch_Pref, string docnos, string docnames, string filenames, string Previous_School, string Previous_Program, int Semesterno)
                {
                    string retStatus = string.Empty;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        //Add Branch change data
                        objParams = new SqlParameter[13];
                        objParams[0] = new SqlParameter("@P_USERNO", UserNo);
                        objParams[1] = new SqlParameter("@P_COLLEGE_ID", CollegeId);
                        objParams[2] = new SqlParameter("@P_DEGREENO", DegreeNo);
                        objParams[3] = new SqlParameter("@P_AREAOFINTEREST", Area);
                        objParams[4] = new SqlParameter("@P_BRANCHNO", Branchno);
                        objParams[5] = new SqlParameter("@P_BRANCH_PREF", Branch_Pref);
                        objParams[6] = new SqlParameter("@P_DOCNOS", docnos);
                        objParams[7] = new SqlParameter("@P_DOCNAMES", docnames);
                        objParams[8] = new SqlParameter("@P_FILENAMES", filenames);
                        objParams[9] = new SqlParameter("@P_PREVIOUS_SCHOOL", Previous_School);
                        objParams[10] = new SqlParameter("@P_PREVIOUS_PROGRAM", Previous_Program);
                        objParams[11] = new SqlParameter("@P_SEMESTERNO", Semesterno);
                        objParams[12] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                        objParams[12].Direction = ParameterDirection.Output;

                        object ret = (objSQLHelper.ExecuteNonQuerySP("PKG_USER_ADD_STUDENT_BRANCH_PREF_TRANSFEREE_NEW", objParams, true));

                        retStatus = Convert.ToString(ret);
                    }
                    catch (Exception ex)
                    {
                        retStatus = retStatus.ToString();
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.BranchController.AddBranchPreferenceTranferee-> " + ex.ToString());
                    }
                    return retStatus;
                }

                //added by aashna 18-07-2023
                public int InsMultipleLoginReasonLog(int UA_NO, int UA_TYPE, string Reason, int userid, string ipadd, int PurposeID, string OTP)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);

                        SqlParameter[] objParams = new SqlParameter[7];
                        objParams[0] = new SqlParameter("@P_UA_NO", UA_NO);
                        objParams[1] = new SqlParameter("@P_UA_TYPE", UA_TYPE);
                        objParams[2] = new SqlParameter("@P_LOGIN_REASON", Reason);
                        objParams[3] = new SqlParameter("@P_LOGIN_BY", userid);
                        objParams[4] = new SqlParameter("@P_IPADDRESS", ipadd);
                        objParams[5] = new SqlParameter("@P_PURPOSEID", PurposeID);
                        objParams[6] = new SqlParameter("@P_OTP", OTP);

                        object ret = objSQLHelper.ExecuteDataSetSP("PKG_ACD_INSERT_MULTIPLE_LOGIN_REASON_LOG", objParams);
                        if (ret != null)
                            if (ret.ToString() != "-99")
                                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.InsMultipleLoginReasonLog-> " + ex.ToString());
                    }

                    return retStatus;
                }

                public DataSet GetStudentScholarshipDetails(string SearchValue, int DynamicFilter, int idno, int Sessionno, int commandtype)  //StudentController.cs
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);

                        SqlParameter[] objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_SEARCHVALUE", SearchValue);
                        objParams[1] = new SqlParameter("@P_DYNAMIC_FILTER", DynamicFilter);
                        objParams[2] = new SqlParameter("@P_IDNO", idno);
                        objParams[3] = new SqlParameter("@P_SESSIONNO", Sessionno);
                        objParams[4] = new SqlParameter("@P_COMMAND_TYPE", commandtype);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_SEARCH_AND_GET_STUDENT_SCHOLARSHIP_INFORMATION_DETAILS", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentsForScheme-> " + ex.ToString());
                    }

                    return ds;
                }

                public DataSet InsertUpdteStudentScholarshipDetails(DataSet dsxml, int Sessionno, int idno, int uano)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);

                        SqlParameter[] objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_DSXML", dsxml.GetXml());
                        objParams[1] = new SqlParameter("@P_SESSIONNO", Sessionno);
                        objParams[2] = new SqlParameter("@P_IDNO", idno);
                        objParams[3] = new SqlParameter("@P_UANO", uano);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_INSERT_UPDATE_STUDENT_SCHOLARSHIP", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentsForScheme-> " + ex.ToString());
                    }

                    return ds;
                }

                public DataSet UpdateStudentScholarshipApprovalDetails(int status, int Sessionno, int idno, int uano)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);

                        SqlParameter[] objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_STATUS", status);
                        objParams[1] = new SqlParameter("@P_SESSIONNO", Sessionno);
                        objParams[2] = new SqlParameter("@P_IDNO", idno);
                        objParams[3] = new SqlParameter("@P_UANO", uano);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_UPDATE_STUDENT_SCHOLARSHIP_APPROVAL", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentsForScheme-> " + ex.ToString());
                    }

                    return ds;
                }

                public DataSet GetStudentInfoById(int studentId)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objDataAccess = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] sqlParams = new SqlParameter[]
                        {
                    new SqlParameter("@P_IDNO", studentId)
                        };
                        ds = objDataAccess.ExecuteDataSetSP("PKG_GET_STUDENT_INFO_FOR_UPDATE", sqlParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.GetStudentInfoById() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }
                //added by nehal on 23022024
                public int ImportDataVerifyExcelSheet(DataTable dtBulkData, int flag, string ipaddress, string macaddress, int uano)
                {
                    int retv = 0;
                    try
                    {
                        SQLHelper objSQL = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objPar = null;
                        {
                            objPar = new SqlParameter[6];
                            objPar[0] = new SqlParameter("@TBL_MIDYEAR", dtBulkData);
                            objPar[1] = new SqlParameter("@P_FLAG", flag);
                            objPar[2] = new SqlParameter("@P_IPADDRESS", ipaddress);
                            objPar[3] = new SqlParameter("@P_MACADDRESS", macaddress);
                            objPar[4] = new SqlParameter("@P_CREATEDBY", uano);
                            objPar[5] = new SqlParameter("@P_OUT", SqlDbType.Int);
                            objPar[5].Direction = ParameterDirection.Output;
                        };

                        object val = objSQL.ExecuteNonQuerySP("PKG_HISTORICAL_AND_MIDYEAR_DATA_MIGRATION_UTILITY", objPar, true);

                        if (val != null)
                        {
                            if (val.ToString().Equals("-99"))
                                retv = -99;
                            else
                                retv = Convert.ToInt16(val.ToString());
                        }
                        else
                            retv = -99;
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLogicLayer.BusinessLogic.StudentController.ImportDataVerifyExcelSheet-> " + ex.ToString());
                    }
                    return retv;
                }
				
				//added by nehal on 01032024
                public DataSet GetHistoricalmirgratedata()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objDataAccess = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] sqlParams = new SqlParameter[]
                        {
                        };
                        ds = objDataAccess.ExecuteDataSetSP("PKG_GET_HISTORICAL_AND_MIDYEAR_DATA_MIGRATION_UTILITY_REPORT", sqlParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.StudentController.GetHistoricalmirgratedata() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }

            }
        }//END: BusinessLayer.BusinessLogic
    }//END: UAIMS
}//END: IITMS