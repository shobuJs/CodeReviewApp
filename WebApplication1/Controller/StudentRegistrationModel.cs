using System;

namespace IITMS.UAIMS.BusinessLayer.BusinessEntities
{
    public class StudentRegistrationModel
    {
        #region Properties

        //Student/Admission Details

        #region Student Details

        public string StudentName { get; set; }
        public string Surname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string AdhaarcardNo { get; set; }
        public int BloogGroupNo { get; set; }
        public string IsBloodDonate { get; set; }
        public string IsPhysicalHandicap { get; set; }
        public int TypeOfHandicap { get; set; }
        public string AlergyDetails { get; set; }
        public int MotherTongue { get; set; }
        public int Citizenship { get; set; }
        public string CommunityCode { get; set; }
        public int CommunityNo { get; set; }
        public int ReligionNo { get; set; }
        public int CasteNo { get; set; }
        public Boolean IsHosteller { get; set; }
        public int Distance { get; set; }
        public string LanguageKnown { get; set; }
        public string ForeignLanguageKnown { get; set; }
        public int NeedTransport { get; set; }
        public int AcademicYearNo { get; set; }
        public int DegreeNo { get; set; }
        public int BranchNo { get; set; }
        public int AdmissionType { get; set; }
        public int IDNO { get; set; }
        public int UA_NO { get; set; }
        public int Semesterno { get; set; }
        public string NativePlace { get; set; }
        public string Marital_Status { get; set; }
        public string SSCTransferCertNo { get; set; }
        public string HSCTransferCertNo { get; set; }
        public string DIPTransferCertNo { get; set; }

        public string EntranceName { get; set; }
        public string EntranceRollno { get; set; }

        #endregion Student Details

        //Address Details

        #region Address Details

        public string LAddressLine1 { get; set; }
        public int Lcity { get; set; }
        public string LOthercity { get; set; }  // other city entry
        public int LState { get; set; }
        public string LPinCode { get; set; }
        public string LSTDCode { get; set; }
        public string LLandLineNo { get; set; }
        public string LEmailId { get; set; }
        public string LStudentMobile { get; set; }
        public string LSMSSend { get; set; }

        public string PAddressLine1 { get; set; }
        public int PCity { get; set; }
        public string POtherCity { get; set; }  // other city entry
        public int PState { get; set; }
        public string PPinCode { get; set; }
        public string PSTDCode { get; set; }
        public string PLandLineNumber { get; set; }

        public string GAddressLine1 { get; set; }
        public int GCity { get; set; }
        public string GOtherCity { get; set; }  //other city entry
        public int GState { get; set; }
        public string GPinCode { get; set; }
        public string GSTDCode { get; set; }
        public string GLandLineNumber { get; set; }
        public string GEmailId { get; set; }
        public string GGaurdianMobile { get; set; }

        #endregion Address Details

        //Last Exam Details

        #region Last exam details

        public int SSCBoardCategory { get; set; }
        public int SSCLanguage { get; set; }
        public decimal SSCLanguageObtMark { get; set; }
        public decimal SSCLanguageMaxMark { get; set; }
        public decimal SSCLanguagePer { get; set; }
        public decimal SSCEnglishObtMark { get; set; }
        public decimal SSCEnglishMaxMark { get; set; }
        public decimal SSCEnglishPer { get; set; }
        public decimal SSCMathsObtMark { get; set; }
        public decimal SSCMathsMaxMark { get; set; }
        public decimal SSCMathsPer { get; set; }
        public decimal SSCScienceObtMark { get; set; }
        public decimal SSCScienceMaxMark { get; set; }
        public decimal SSCSciencePer { get; set; }
        public decimal SSCSocialScienceObtMark { get; set; }
        public decimal SSCSocialScienceMaxMark { get; set; }
        public decimal SSCSocialSciencePer { get; set; }
        public decimal SSCTotalMarkScore { get; set; }
        public decimal SSCTotalofMaxMark { get; set; }
        public string SSCYearofPassing { get; set; }
        public string SSCMediumOfInstruction { get; set; }
        public string SSCMarkCertNo { get; set; }
        public string SSCPassCertNo { get; set; }
        public string SSCTMRNo { get; set; }
        public string SSCRegisterNo { get; set; }
        public string SSCInstituteName { get; set; }
        public decimal SSCTotalPer { get; set; }
        public string SSCInstituteAddress { get; set; }
        public decimal SSCLanguageGdPoint { get; set; }
        public string SSCLanguageGrade { get; set; }
        public decimal SSCEnglishGdPoint { get; set; }
        public string SSCEnglishGrade { get; set; }
        public decimal SSCMathGdPoint { get; set; }
        public string SSCMathGrade { get; set; }
        public decimal SSCScienceGdPoint { get; set; }
        public string SSCScienceGrade { get; set; }
        public decimal SSCSocSciGdPoint { get; set; }
        public string SSCSocSciGrade { get; set; }
        public decimal SSCTotalGradePoint { get; set; }
        public decimal SSCCGPA { get; set; }

        public decimal ISEComputerApplicationObtMark { get; set; }
        public decimal ISEComputerApplicationMaxMark { get; set; }
        public decimal ISEComputerApplicationPer { get; set; }
        public decimal ISEHistoryObtMark { get; set; }
        public decimal ISEHistoryMaxMark { get; set; }
        public decimal ISEHistoryPer { get; set; }
        public decimal ISEComputerAppGdPoint { get; set; }
        public string ISEComputerAppGrade { get; set; }
        public decimal ISEHistoryGdPoint { get; set; }
        public string ISEHistoryGrade { get; set; }

        public int HSCBoardCategory { get; set; }
        public int HSCLanguage { get; set; }
        public decimal HSCLanguageObtMark { get; set; }
        public decimal HSCLanguageMaxMark { get; set; }
        public decimal HSCLanguagePer { get; set; }
        public decimal HSCEnglishObtMark { get; set; }
        public decimal HSCEnglishMaxMark { get; set; }
        public decimal HSCEnglishPer { get; set; }
        public decimal HSCMathsObtMark { get; set; }
        public decimal HSCMathsMaxMark { get; set; }
        public decimal HSCMathsPer { get; set; }
        public decimal HSCPhysicsMaxMark { get; set; }
        public decimal HSCPhysicsObtMark { get; set; }
        public decimal HSCPhysicsPer { get; set; }
        public decimal HSCChemMaxMark { get; set; }
        public decimal HSCChemObtMark { get; set; }
        public decimal HSCChemPer { get; set; }
        public int HSCOptionalSub { get; set; }
        public decimal HSCTotalMarkScore { get; set; }
        public decimal HSCTotalofMaxMark { get; set; }
        public string HSCYearofPassing { get; set; }
        public string HSCMediumOfInstruction { get; set; }
        public string HSCMarkCertificateNo { get; set; }
        public string HSCPassCertificateNo { get; set; }
        public string HSCTMRNo { get; set; }
        public string HSCRegisterNo { get; set; }
        public string HSCInstituteName { get; set; }
        public decimal HSCTotalPer { get; set; }
        public string HSCInstituteAddress { get; set; }
        public decimal HSCLangGdPoint { get; set; }
        public string HSCLangGrade { get; set; }
        public decimal HSCEnglishGdPoint { get; set; }
        public string HSCEnglishGrade { get; set; }
        public decimal HSCMathGdPoint { get; set; }
        public string HSCMathGrade { get; set; }
        public decimal HSCPhyGdPoint { get; set; }
        public string HSCPhyGrade { get; set; }
        public decimal HSCChemGdPoint { get; set; }
        public string HSCChemGrade { get; set; }
        public decimal HSCOptionalSubGdPoint { get; set; }
        public string HSCOptionalSubGrade { get; set; }
        public decimal HSCOptionalSubMaxMark { get; set; }
        public decimal HSCOptionalSubObtMark { get; set; }
        public decimal HSCOptionalSubPer { get; set; }

        public decimal VocationalTHObtMark { get; set; }
        public decimal VocationalTHMaxMark { get; set; }
        public decimal VocationalTHPer { get; set; }

        public decimal VocationalPR1ObtMark { get; set; }
        public decimal VocationalPR1MaxMark { get; set; }
        public decimal VocationalPR1Per { get; set; }
        public decimal VocationalPR2ObtMark { get; set; }
        public decimal VocationalPR2MaxMark { get; set; }
        public decimal VocationalPR2Per { get; set; }
        public decimal VocationalTHGdPoint { get; set; }
        public string VocationalTHGrade { get; set; }
        public decimal VocationalPR1GdPoint { get; set; }
        public string VocationalPR1Grade { get; set; }
        public decimal VocationalPR2GdPoint { get; set; }
        public string VocationalPR2Grade { get; set; }
        public decimal HSCTotalGradePoint { get; set; }
        public decimal HSCCGPA { get; set; }

        //LAST EXAM-DIPLOMA DETAILS
        public string NameofDiploma { get; set; }

        public string DiplomaCollegeName { get; set; }
        public string DiplomaBoard { get; set; }
        public string DiplomaRegNumber { get; set; }
        public string DiplomaYearOfPassing { get; set; }
        public decimal SemIObtainedMark { get; set; }
        public decimal SemIMaxMark { get; set; }
        public decimal SemIPer { get; set; }
        public decimal SemIIObtainedMark { get; set; }
        public decimal SemIIMaxMark { get; set; }
        public decimal SemIIPer { get; set; }
        public decimal SemIIIObtainedMark { get; set; }
        public decimal SemIIIMaxMark { get; set; }
        public decimal SemIIIPer { get; set; }
        public decimal SemIVObtainedMark { get; set; }
        public decimal SemIVMaxMark { get; set; }
        public decimal SemIVPer { get; set; }
        public decimal SemVObtainedMark { get; set; }
        public decimal SemVMaxMark { get; set; }
        public decimal SemVPer { get; set; }
        public decimal SemVIObtainedMark { get; set; }
        public decimal SemVIMaxMark { get; set; }
        public decimal SemVIPer { get; set; }
        public decimal SemVIIObtainedMark { get; set; }
        public decimal SemVIIMaxMark { get; set; }
        public decimal SemVIIPer { get; set; }
        public int DipDegree { get; set; }
        public string Specialization { get; set; }
        public decimal TotalMarkScoredDip { get; set; }
        public decimal TotalofMaxMarkDip { get; set; }
        public decimal TotalPercentageDip { get; set; }

        public string SSCotherBoard { get; set; }
        public int SSCInputSystem { get; set; }

        public string HSCotherBoard { get; set; }
        public int HSCInputSystem { get; set; }

        #endregion Last exam details

        //Entrance Details

        #region Entrance details

        public decimal CutOffMarks { get; set; }
        public decimal OverAllMarks { get; set; }
        public int CommunityRank { get; set; }
        public int OverAllRank { get; set; }
        public string TNEAApplicationNo { get; set; }
        public string AcknowledgeRecNo { get; set; }
        public string AdmOrderNo { get; set; }
        public decimal AdvPaymentAmt { get; set; }

        private DateTime _AdmOrderDate = DateTime.MinValue;

        public DateTime AdmOrderDate
        {
            get { return _AdmOrderDate; }
            set { _AdmOrderDate = value; }
        }

        private DateTime _AcknowledgeRecDate = DateTime.MinValue;

        public DateTime AcknowledgeRecDate
        {
            get { return _AcknowledgeRecDate; }
            set { _AcknowledgeRecDate = value; }
        }

        public string DoteApplicationNo { get; set; }
        public string DoteAllotmentOrderNo { get; set; }
        private DateTime _DoteAllotmentOrderDate = DateTime.MinValue;

        public DateTime DoteAllotmentOrderDate
        {
            get { return _DoteAllotmentOrderDate; }
            set { _DoteAllotmentOrderDate = value; }
        }

        #endregion Entrance details

        //Parent Details

        #region Parent details

        public string SingleParent { get; set; }
        public string FatherName { get; set; }
        public string FatherLastName { get; set; }
        public int FatherQualification { get; set; }
        public int FatherOccupation { get; set; }
        public string FatherOrgName { get; set; }
        public int FatherDesig { get; set; }

        public string FatherOtherDesig { get; set; }

        public string FatherOtherQual { get; set; }

        public int MotherDesig { get; set; }
        public string MotherOtherDesig { get; set; }
        public int MotherQual { get; set; }
        public string MotherOtherQual { get; set; }

        public int GuardianDesignation { get; set; }
        public string GuardianOtherDesig { get; set; }
        public int GuardianQual { get; set; }
        public string GuardianOtherQual { get; set; }

        public string FatherAnnualIncome { get; set; }
        public string FatherMobile { get; set; }
        public string FatherOrgAddress { get; set; }
        public int FatherOrgCity { get; set; }
        public int FatherOrgState { get; set; }
        public string FatherOrgPin { get; set; }
        public string FatherOrgSTD { get; set; }
        public string FatherOrgPhone { get; set; }
        public string MotherName { get; set; }
        public string MotherLastName { get; set; }

        //public int MotherQual { get; set; }
        public int MotherOccupation { get; set; }

        public string MotherMobile { get; set; }
        public string MotherOrgAdd { get; set; }
        public int MotherOrgCity { get; set; }
        public int MotherOrgState { get; set; }
        public string MotherOrgPin { get; set; }
        public string MotherOrgSTD { get; set; }
        public string MotherOrgPhone { get; set; }
        public string GaurdianName { get; set; }
        public string GaurdianLastName { get; set; }

        //public int GuardianQual { get; set; }
        public int GuardianOccupation { get; set; }

        public string GuardianOrgName { get; set; }

        // public int GuardianDesignation { get; set; }
        public string GuardianAnnualIncome { get; set; }

        public string GuardianOrgAdd { get; set; }
        public int GuardianOrgCity { get; set; }
        public int GuardianOrgState { get; set; }
        public string GuardianOrgPin { get; set; }
        public string GuardianOrgSTD { get; set; }
        public string GuardianOrgPhone { get; set; }
        public string MotherWorkingOrg { get; set; }
        public string MotherAnnualIncome { get; set; }
        public string Parent_Type { get; set; }

        #endregion Parent details

        //Extra Details

        #region Extra details

        public string RelativeDetails { get; set; }
        public string ReasonToChoose { get; set; }
        public string CommunityCertNo { get; set; }
        public DateTime CommunityCertIsueDate { get; set; }
        public string CommunityCertAuthority { get; set; }
        public string TransferCertNo { get; set; }
        public DateTime TransferCertDate { get; set; }
        public string ConductCertNo { get; set; }
        public DateTime ConductCertDate { get; set; }
        public string FirstAppearanceRegno { get; set; }
        public string FirstAppearanceYear { get; set; }
        public string SecondAppearanceRegno { get; set; }
        public string SecondAppearanceYear { get; set; }
        public string ThirdAppearanceRegno { get; set; }
        public string ThirdAppearanceYear { get; set; }
        public string MinorityCertificateNo { get; set; }
        private DateTime? _MinorityIssueDate = null;

        public DateTime? MinorityIssueDate
        {
            get { return _MinorityIssueDate; }
            set { _MinorityIssueDate = value; }
        }

        public string MinorityCertAuthority { get; set; }

        #endregion Extra details

        //Fees details

        #region Fees concession details

        public string FirstGraduate { get; set; }
        public string FirstGraduateCertNo { get; set; }
        private DateTime _FirstGraduateCertDate = DateTime.MinValue;

        public DateTime FirstGraduateCertDate
        {
            get { return _FirstGraduateCertDate; }
            set { _FirstGraduateCertDate = value; }
        }

        public string FirstGraduateCertAuth { get; set; }
        public string AICTEWaiver { get; set; }
        public string AICTECertNo { get; set; }
        private DateTime _AICTECertDate = DateTime.MinValue;

        public DateTime AICTECertDate
        {
            get { return _AICTECertDate; }
            set { _AICTECertDate = value; }
        }

        public string AICTECertAuth { get; set; }
        public string DravidarWelfare { get; set; }
        public string WelfareCertNo { get; set; }
        private DateTime _WelfareCertDate = DateTime.MinValue;

        public DateTime WelfareCertDate
        {
            get { return _WelfareCertDate; }
            set { _WelfareCertDate = value; }
        }

        public string WelfareCertAuth { get; set; }

        #endregion Fees concession details

        // Added By Pritish S. on 18/06/2020

        #region Course Details

        public string Ccode1 { get; set; }
        public string CourseName1 { get; set; }
        public string CollegeName1 { get; set; }
        public string AppearanceYear1 { get; set; }
        public string Result1 { get; set; }
        public string Ccode2 { get; set; }
        public string CourseName2 { get; set; }
        public string CollegeName2 { get; set; }
        public string AppearanceYear2 { get; set; }
        public string Result2 { get; set; }
        public string Ccode3 { get; set; }
        public string CourseName3 { get; set; }
        public string CollegeName3 { get; set; }
        public string AppearanceYear3 { get; set; }
        public string Result3 { get; set; }
        public string Ccode4 { get; set; }
        public string CourseName4 { get; set; }
        public string CollegeName4 { get; set; }
        public string AppearanceYear4 { get; set; }
        public string Result4 { get; set; }
        public string Ccode5 { get; set; }
        public string CourseName5 { get; set; }
        public string CollegeName5 { get; set; }
        public string AppearanceYear5 { get; set; }
        public string Result5 { get; set; }
        public string Ccode6 { get; set; }
        public string CourseName6 { get; set; }
        public string CollegeName6 { get; set; }
        public string AppearanceYear6 { get; set; }
        public string Result6 { get; set; }
        public string Ccode7 { get; set; }
        public string CourseName7 { get; set; }
        public string CollegeName7 { get; set; }
        public string AppearanceYear7 { get; set; }
        public string Result7 { get; set; }
        public string Ccode8 { get; set; }
        public string CourseName8 { get; set; }
        public string CollegeName8 { get; set; }
        public string AppearanceYear8 { get; set; }
        public string Result8 { get; set; }

        #endregion Course Details

        public int CollegeId { get; set; }
        public string FatherAdhaarcardNo { get; set; }
        public string MotherAdhaarcardNo { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public decimal CSObtMark { get; set; }
        public decimal CSMaxMark { get; set; }
        public decimal CSPer { get; set; }
        public decimal CSGdPoint { get; set; }
        public string CSGrade { get; set; }
        public string IdentityMark2 { get; set; }
        public string IdentityMark { get; set; }
        public string Subcaste { get; set; }
        public string EmergencyNum { get; set; }
        public string EmergencyName { get; set; }
        public string EmergencyRelation { get; set; }
        public string EmergencyEmail { get; set; }
        public string GRelation { get; set; }
        public int SSCLanguage2 { get; set; }
        public decimal SSCLanguage2ObtMark { get; set; }
        public decimal SSCLanguage2MaxMark { get; set; }
        public decimal SSCLanguage2Per { get; set; }
        public decimal SSCLanguage2GdPoint { get; set; }
        public string SSCLanguage2Grade { get; set; }
        public decimal BotanyObtMark { get; set; }
        public decimal BotanyMaxMark { get; set; }
        public decimal BotanyPer { get; set; }
        public decimal BotanyGdPoint { get; set; }
        public string BotanyGrade { get; set; }
        public decimal ZooObtMark { get; set; }
        public decimal ZooMaxMark { get; set; }
        public decimal ZooPer { get; set; }
        public decimal ZooGdPoint { get; set; }
        public string ZooGrade { get; set; }
        public decimal Percentile { get; set; }
        public string FEmailId { get; set; }
        public string FPANNumber { get; set; }
        public string MEmailId { get; set; }
        public string MPANNumber { get; set; }
        public decimal SemVIIIObtainedMark { get; set; }
        public decimal SemVIIIMaxMark { get; set; }
        public decimal SemVIIIPer { get; set; }

        #endregion Properties
    }
}