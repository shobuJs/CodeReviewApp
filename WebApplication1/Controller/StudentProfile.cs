using System;
using System.Collections.Generic;

//using static IITMS.UAIMS.BusinessLayer.BusinessEntities.OrganizationMasterEntity;

namespace IITMS.UAIMS.BusinessLayer.BusinessEntities
{
    public class StudentProfile
    {
        #region DropDown Bind

        public class BindDropDown
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        public class GenderDropDown
        {
            public int GenderNo { get; set; }
            public string GenderName { get; set; }
        }

        public class ReligionDropDown
        {
            public int ReligionNo { get; set; }
            public string ReligionName { get; set; }
        }

        public class OccupationDropDown
        {
            public int OccupationNo { get; set; }
            public string OccupationName { get; set; }
        }

        public class RelationshipDropDown
        {
            public int RelationshipNo { get; set; }
            public string RelationshipName { get; set; }
        }

        public class CountryDropDown
        {
            public int CountryNo { get; set; }
            public string CountryName { get; set; }
        }

        public class VisaStatusDropDown
        {
            public int VisaStatusID { get; set; }
            public string VISASTATUS { get; set; }
        }

        public class VisaTypeDropDown
        {
            public int VISATYPEID { get; set; }
            public string VISATYPE { get; set; }
        }

        public class CitizenshipDropDown
        {
            public int CitizenshipNo { get; set; }
            public string CitizenshipName { get; set; }
        }

        public class AllDropDownBindMethod
        {
            #region AllDropDownBindMethod

            public List<GenderDropDown> GenderDropDown { get; set; }
            public List<ReligionDropDown> ReligionDropDown { get; set; }
            public List<OccupationDropDown> OccupationDropDown { get; set; }
            public List<RelationshipDropDown> RelationshipDropDown { get; set; }
            public List<CountryDropDown> CountryDropDown { get; set; }
            public List<VisaStatusDropDown> VisaStatusDropDown { get; set; }
            public List<VisaTypeDropDown> VisaTypeDropDown { get; set; }
            public List<CitizenshipDropDown> CitizenshipDropDown { get; set; }

            #endregion AllDropDownBindMethod
        }

        #endregion DropDown Bind

        public class PersonalDetails
        {
            #region Personal Details

            public int Idno
            {
                get;
                set;
            }

            public string Regno
            {
                get;
                set;
            }

            public string Student_FirstName
            {
                get;
                set;
            }

            public string Student_MiddleName
            {
                get;
                set;
            }

            public string Student_LastName
            {
                get;
                set;
            }

            public string Extension_Name
            {
                get;
                set;
            }

            public int Gender_No
            {
                get;
                set;
            }

            public DateTime DOB
            {
                get;
                set;
            }

            public string DOBString
            {
                get;
                set;
            }

            public string Place_of_Birth
            {
                get;
                set;
            }

            public int Civil_Status
            {
                get;
                set;
            }

            public int Citizenship
            {
                get;
                set;
            }

            public int Religion_No
            {
                get;
                set;
            }

            public string Student_Mobile
            {
                get;
                set;
            }

            public string Student_Email
            {
                get;
                set;
            }

            public string Face_Book_Name
            {
                get;
                set;
            }

            public string Father_Name
            {
                get;
                set;
            }

            public string Father_Mobile
            {
                get;
                set;
            }

            public int Father_Occupation
            {
                get;
                set;
            }

            public string Mother_Name
            {
                get;
                set;
            }

            public string Mother_Mobile
            {
                get;
                set;
            }

            public int Mother_Occupation
            {
                get;
                set;
            }

            public int Number_of_Brothers
            {
                get;
                set;
            }

            public int Number_of_Sisters
            {
                get;
                set;
            }

            public string Guardian_Name
            {
                get;
                set;
            }

            public string Guardian_Mobile
            {
                get;
                set;
            }

            public int Guardian_Relation
            {
                get;
                set;
            }

            public string Guardian_Address
            {
                get;
                set;
            }

            public int StudentAge
            {
                get;
                set;
            }

            public int Status
            {
                get;
                set;
            }

            public string StudentID
            {
                get;
                set;
            }

            public string CampusName
            {
                get;
                set;
            }

            public string CollegeName
            {
                get;
                set;
            }

            public string CourseName
            {
                get;
                set;
            }

            public string CurriculumName
            {
                get;
                set;
            }

            public string Semester
            {
                get;
                set;
            }

            public string AdvisorName
            {
                get;
                set;
            }

            public int Dual_Citizenship
            {
                get;
                set;
            }

            public string Spouse_Name
            {
                get;
                set;
            }

            public int Spouse_Occupation
            {
                get;
                set;
            }

            public int No_of_Children
            {
                get;
                set;
            }

            public string PassportNo
            {
                get;
                set;
            }

            public string IssuedDateString
            {
                get;
                set;
            }

            public DateTime IssuedDate
            {
                get;
                set;
            }

            public int CountryNo
            {
                get;
                set;
            }

            public int Visa_Type
            {
                get;
                set;
            }

            public int Visa_Status
            {
                get;
                set;
            }

            public string ICARD_NO
            {
                get;
                set;
            }

            public DateTime Authorized_Stay_From
            {
                get;
                set;
            }

            public string Authorized_Stay_FromString
            {
                get;
                set;
            }

            public DateTime Authorized_Stay_To
            {
                get;
                set;
            }

            public string Authorized_Stay_ToString
            {
                get;
                set;
            }

            public string Remark
            {
                get;
                set;
            }

            public string Face_Book_Link
            {
                get;
                set;
            }

            #endregion Personal Details
        }

        public class AddressDetails
        {
            #region Address Details

            public int Idno
            {
                get;
                set;
            }

            public string LAddress
            {
                get;
                set;
            }

            public int LCountry
            {
                get;
                set;
            }

            public int LState
            {
                get;
                set;
            }

            public int LCity
            {
                get;
                set;
            }

            public string PAddress
            {
                get;
                set;
            }

            public int PCountry
            {
                get;
                set;
            }

            public int PState
            {
                get;
                set;
            }

            public int PCity
            {
                get;
                set;
            }

            #endregion Address Details
        }

        public class StudentAddressDetails
        {
            #region StudentAddressDetails

            public List<AddressDetails> AddressDetails { get; set; }
            public List<BindDropDown> BindDropDown { get; set; }

            #endregion StudentAddressDetails
        }

        public class EducationDetails
        {
            #region Education Details

            private int _idno = 0;
            private int _Classified = 0;
            private int _Course = 0;
            private int _PrefferedModality = 0;
            private int _Levelno = 0;
            private int _SchoolNo = 0;
            private int _Region = 0;
            private int _YearAttend = 0;
            private int _Type = 0;

            public int Idno
            {
                get { return _idno; }
                set { _idno = value; }
            }

            public int Classified
            {
                get { return _Classified; }
                set { _Classified = value; }
            }

            public int Course
            {
                get { return _Course; }
                set { _Course = value; }
            }

            public int PrefferedModality
            {
                get { return _PrefferedModality; }
                set { _PrefferedModality = value; }
            }

            public int LevelNo
            {
                get { return _Levelno; }
                set { _Levelno = value; }
            }

            public int SchoolNo
            {
                get { return _SchoolNo; }
                set { _SchoolNo = value; }
            }

            public int Region
            {
                get { return _Region; }
                set { _Region = value; }
            }

            public int YearAttend
            {
                get { return _YearAttend; }
                set { _YearAttend = value; }
            }

            public int Type
            {
                get { return _Type; }
                set { _Type = value; }
            }

            #endregion Education Details
        }

        public class PhotoSignature
        {
            #region Photo&Signature

            private int _idno = 0;
            private byte[] _photo = null;
            private byte[] _signature = null;

            public int Idno
            {
                get { return _idno; }
                set { _idno = value; }
            }

            public byte[] Photo
            {
                get { return _photo; }
                set { _photo = value; }
            }

            public byte[] Signature
            {
                get { return _signature; }
                set { _signature = value; }
            }

            #endregion Photo&Signature
        }

        public class EnlistedSubjects
        {
            #region EnlistedSubjects

            public string SemesterName { get; set; }
            public string Subject_Code { get; set; }
            public string Subject_Name { get; set; }
            public string Subject_Type { get; set; }
            public decimal Units { get; set; }
            public string Section { get; set; }
            public string Teacher { get; set; }
            public string Advising_Status { get; set; }

            #endregion EnlistedSubjects
        }

        #region OverAllResult

        public class OverAllResult
        {
            #region OverAllResult

            public List<OverAllResultSession> OverAllResultSession { get; set; }
            public List<OverAllResultCourses> OverAllResultCourses { get; set; }

            #endregion OverAllResult
        }

        public class OverAllResultSession
        {
            #region OverAllResult

            public string SemesterName { get; set; }
            public int SemesterNo { get; set; }
            public string SessionName { get; set; }
            public int SessionNo { get; set; }
            public int Total_Subject { get; set; }
            public string GWA { get; set; }
            public string GW_Status { get; set; }
            public string DateofResult { get; set; }

            #endregion OverAllResult
        }

        public class OverAllResultCourses
        {
            #region OverAllResult

            public int SemesterNo { get; set; }
            public int SessionNo { get; set; }
            public string Subject_Code { get; set; }
            public string Subject_Name { get; set; }
            public string Subject_Type { get; set; }
            public string Units { get; set; }
            public string FinalGrade { get; set; }
            public string Status { get; set; }

            #endregion OverAllResult
        }

        #endregion OverAllResult

        #region StudentGrades

        public class StudentGrades
        {
            public List<StudentResultGrades> StudentResultGrades { get; set; }
            public List<StudentExamGrades> StudentExamGrades { get; set; }
            public List<StudentExamComponent> StudentExamComponent { get; set; }
        }

        public class StudentResultGrades
        {
            #region OverAllResult

            public string SemesterName { get; set; }
            public int SemesterNo { get; set; }
            public string SessionName { get; set; }
            public int SessionNo { get; set; }
            public string SubjectCode { get; set; }
            public string SubjectName { get; set; }
            public int CourseNo { get; set; }
            public string SubjectType { get; set; }
            public string Final_Grade { get; set; }
            public string GW_Status { get; set; }
            public string Date { get; set; }
            public string Is_Audit { get; set; }

            #endregion OverAllResult
        }

        public class StudentExamGrades
        {
            #region OverAllResult

            public string SemesterName { get; set; }
            public int SemesterNo { get; set; }
            public int CourseNo { get; set; }
            public string ExamName { get; set; }
            public int ExamNo { get; set; }
            public string Grade { get; set; }
            public string GW_Status { get; set; }
            public string Total { get; set; }
            #endregion OverAllResult
        }

        public class StudentExamComponent
        {
            #region OverAllResult

            public string SemesterName { get; set; }
            public int SemesterNo { get; set; }
            public int CourseNo { get; set; }
            public string ExamName { get; set; }
            public int ExamNo { get; set; }
            public string Asses_Component { get; set; }
            public string Asses_ComponentNo { get; set; }
            public string ObtainedMarks { get; set; }
            public string GW_Status { get; set; }
            public string Date { get; set; }

            #endregion OverAllResult
        }

        #endregion StudentGrades

        #region Attendance Details

        private List<AttDetailsByIDNO> LstAttDetailsByIDNO { get; set; }

        public class AttDetailsByIDNO
        {
            public string IDNO { get; set; }
            public string RegNo { get; set; }
            public string RollNo { get; set; }
            public string StudName { get; set; }
            public string SemesterName { get; set; }
            public int SemesterNo { get; set; }
            public string CCODE { get; set; }
            public string CourseName { get; set; }
            public int CourseNo { get; set; }
            public string SubType { get; set; }
            public string SubID { get; set; }
            public int Tot_Classes { get; set; }
            public int Tot_Present { get; set; }
            public int Tot_Absent { get; set; }
            public string ATT_Percent { get; set; }
            public string FacultyName { get; set; }
        }

        public class AttDetailsCourseWise
        {
            public string IDNO { get; set; }
            public string RegNo { get; set; }
            public string RollNo { get; set; }
            public string StudName { get; set; }
            public string SemesterName { get; set; }
            public int SemesterNo { get; set; }
            public string ATT_Date { get; set; }
            public string ATT_Status { get; set; }
            public string CourseName { get; set; }
            public int CourseNo { get; set; }
            public string SlotNo { get; set; }
            public string Period { get; set; }
        }

        #endregion Attendance Details

        #region Student Course Details

        public class StudentCourseDeatilAll
        {
            public List<StudentCourseDeatils> StudentCourseDeatils { get; set; }
            public List<StudentEducationDeatils> StudentEducationDeatils { get; set; }
            public List<NameOfSchoolDropDown> NameOfSchoolDropDown { get; set; }
            public List<YearAttendedDropDown> YearAttendedDropDown { get; set; }
            public List<TypeDropDown> TypeDropDown { get; set; }
            //public List<StudentExamComponent> StudentExamComponent { get; set; }
        }

        public class StudentCourseDeatils
        {
            #region StudentCourseDeatils

            public string Intake { get; set; }
            public int IntakeNo { get; set; }
            public string SemesterName { get; set; }
            public int SemesterNo { get; set; }
            public string CollegeName { get; set; }
            public int CollegeNo { get; set; }
            public string CourseName { get; set; }
            public int CdbNo { get; set; }
            public string SchemeName { get; set; }
            public int SchemeNo { get; set; }
            public string LearningModality { get; set; }
            public int LearningModalityNo { get; set; }
            public string Advisor { get; set; }
            public string Mentor { get; set; }

            #endregion StudentCourseDeatils
        }

        public class StudentEducationDeatils
        {
            #region StudentEducationDeatils

            public string QualificationLevelName { get; set; }
            public int QualificationLevelNo { get; set; }
            public string SchoolName { get; set; }
            public int SchoolNo { get; set; }
            public string YearAttended { get; set; }
            public int YearAttendedNo { get; set; }
            public string TypeName { get; set; }
            public int TypeNo { get; set; }
            public string SchemeName { get; set; }
            public int Idno { get; set; }
            public int UserNo { get; set; }

            #endregion StudentEducationDeatils
        }

        public class NameOfSchoolDropDown
        {
            #region NameOfSchoolDropDown

            public string SchoolName { get; set; }
            public int SchoolNameNo { get; set; }

            #endregion NameOfSchoolDropDown
        }

        public class YearAttendedDropDown
        {
            #region YearAttendedDropDown

            public string YearAttended { get; set; }
            public int YearAttendedNo { get; set; }

            #endregion YearAttendedDropDown
        }

        public class TypeDropDown
        {
            #region TypeDropDown

            public string TypeName { get; set; }
            public int TypeNo { get; set; }

            #endregion TypeDropDown
        }

        #endregion Student Course Details
    }
}