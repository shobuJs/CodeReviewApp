using System;

namespace IITMS.UAIMS.BusinessLayer.BusinessEntities
{
    public class Student
    {
        //general info

        #region Private Member

        private string _idno = string.Empty;
        private int _courseno = 0;
        private string _ccode = string.Empty;

        private int _idNo = 0;
        private string _annual_income = string.Empty;
        private string _collegejss = string.Empty;
        private string _cetorderno = string.Empty;
        private string _cetdate = string.Empty;
        private string _cetamount = string.Empty;
        private string _regNo = string.Empty;
        private string _rollNo = string.Empty;
        private int _sectionNo = 0;
        private string _studName = string.Empty;
        private string _firstName = string.Empty;
        private string _MiddleName = string.Empty;
        private string _lastName = string.Empty;
        private string _studNameHindi = string.Empty;
        private string _fatherfirstName = string.Empty;
        private string _fatherName = string.Empty;
        private string _fatherMiddleName = string.Empty;
        private string _fatherLastName = string.Empty;
        private string _motherName = string.Empty;
        private string _fatherMobile = string.Empty;

        private string _motherMobile = string.Empty;
        private string _fatherOfficeNo = string.Empty;
        private string _motherOfficeNo = string.Empty;
        private DateTime _dob = DateTime.Today;
        private string _Age = string.Empty;

        private char _sex = ' ';
        private int _religionNo = 0;
        private char _married = ' ';
        private int _nationalityNo = 0;
        private int _categoryNo = 0;
        private int _admcategoryNo = 0;
        private string _addharcardno = string.Empty;

        // address info
        private string _pAddress = string.Empty;

        private string _pCity = string.Empty;

        // PG ENTRANCE EXAM SCORES

        private int _PGQUALIFYNO = 0;
        private string _PGENTROLLNO = string.Empty;
        private decimal _pgpercentage = 0.0m;
        private decimal _pgpercentile = 0.0m;
        private string _pgyearOfExam = string.Empty;
        private int _PGRANK = 0;
        private decimal _pgscore = 0.0m;

        private int _pState = 0;
        private string _pPinCode = string.Empty;
        private string _pMobile = string.Empty;

        // admission details
        private DateTime _admDate = DateTime.Today;

        private bool _hosteler = false;
        private int _degreeNo = 0;
        private int _batchNo = 0;
        private int _branchNo = 0;
        private int _classYear = 0;
        private int _semesterNo = 0;
        private int _pType = 0;
        private int _examPtype = 0;  //examPtype -> field use in GEC only

        private int _college_id = 0; //added by reena
        private int _stateNo = 0;
        private string _collegeCode = string.Empty;
        private string _birthPlace = string.Empty;
        private string _birthvillage = string.Empty;
        private string _birthtaluka = string.Empty;
        private string _birthdistrict = string.Empty;
        private string _birthdistate = string.Empty;

        private string _birthPinCode = string.Empty;
        private string _Specialization = string.Empty;

        private int _mToungeNo = 0;
        private string _otherLanguage = string.Empty;
        private decimal _height = 0.0m;
        private decimal _weight = 0;

        private string _identyMark = string.Empty;
        private int _bloodGroupNo = 0;
        private int _caste = 0;
        private string _subcaste = string.Empty;

        private string _countryDomicile = string.Empty;
        private bool _irregular = false;
        private bool _urban = false;
        private int _year = 0;
        private bool _pro = false;
        private int _schemeNo = 0;
        private string _lastRollNo = string.Empty;
        private int _roll2 = 0;
        private string _accNo = string.Empty;
        private string _visano = string.Empty;
        private string _passportNo = string.Empty;
        private string _emailID = string.Empty;

        //private bool _idType = false;
        private string _schemeType = string.Empty;

        private int _idType = 0;
        private bool _admCancel = false;
        private int _admBatch = 0;
        private DateTime _leaveDate = DateTime.Today;
        private bool _can = false;
        private DateTime _canDate = DateTime.Today;
        private string _remark = string.Empty;
        private int _facAdvisor = 0;
        private string _prjname = string.Empty;
        private string _qualifyexamname = string.Empty;
        private decimal _percentage = 0.0m;
        private decimal _percentile = 0.0m;
        private string _yearOfExam = string.Empty;
        private string _qexmRollNo = string.Empty;
        private int uano = 0;
        private int _shift = 0;
        private QualifiedExam[] _lastQualExams = null;
        private string _studId = string.Empty;
        private string _studentMobile = string.Empty;
        private int _cap_Institute = 0;

        //prospectus details
        private int _sessionNo = 0;

        private string _serialNo = string.Empty;
        private string _reciptNo = string.Empty;
        private DateTime _saleDate = DateTime.Today;
        private string _amount = string.Empty;
        private string _IPADDRESS;
        private DateTime _printDate = DateTime.Today;

        //BranchChange
        private int _newbranchNo = 0;

        //private GEC_Student[] _paidDemandDrafts = null; // multiple dd details

        private int _admroundNo = 0;
        private int _prosNo = 0;
        private string _District;
        private int _ALLINDIARANK = 0;
        private int _STATERANK = 0;
        private decimal _score = 0.0m;
        private string _paper = string.Empty;
        private string _paper_code = string.Empty;
        private string _colg_address = string.Empty;

        private decimal _PERCENTILE = 0.00m;
        private string _qualifyNo = string.Empty;
        private int _admquotano = 0;

        // STUDNET TEMP DATA UPLOAD
        private string _gatescore = string.Empty;

        private string _gatereg = string.Empty;
        private string _gatepaper = string.Empty;
        private string _gateyear = string.Empty;
        private string _ph = string.Empty;
        private string _documents = string.Empty;
        private int _paytypeno = 0;
        private int _gatescholarship = 0;

        //upload sign userd
        private byte[] _studsign = null;

        private byte[] _studphoto = null;

        private int _bankno = 0;
        private string _enrollNo = string.Empty;

        private int _phdstatus = 0;
        private int _phdsupervisorno = 0;

        private int _phdcosupervisorno1 = 0;
        private int _phdcosupervisorno2 = 0;

        private int _typesupervisorno = 0;
        private int _typecosupervisorno1 = 0;
        private int _typecosupervisorno2 = 0;

        private int _checkersdetailno = 0;
        private int _checkerno1 = 0;
        private int _checkerno2 = 0;
        private string _checkername1 = string.Empty;
        private string _checkername2 = string.Empty;
        private int _collatorno1 = 0;
        private int _collatorno2 = 0;

        private string _corres_address = string.Empty;
        private string _corres_pin = string.Empty;
        private string _corres_mob = string.Empty;
        private int _yearsofstudey = 0;
        private string _stateotherstate = string.Empty;

        private int _supervisorno = 0;
        private int _supervisormemberno = 0;
        private int _supervisorstatus = 0;

        private int _joinsupervisorno = 0;
        private int _joinsupervisormemberno = 0;
        private int _joinsupervisorstatus = 0;

        private int _institutefacultyno = 0;
        private int _institutefacmemberno = 0;
        private int _institutefacultystatus = 0;

        private int _drcno = 0;
        private int _drcmemberno = 0;
        private int _drcstatus = 0;

        private int _phdstatuscat = 0;

        private int _credits = 0;
        private string _topics = string.Empty;
        private string _workdone = string.Empty;
        private string _phdremark = string.Empty;
        private int _grade = 0;

        private DateTime _attempt1datewritten = DateTime.Today;
        private DateTime? _attempt2datewritten = DateTime.Today;
        private DateTime _attempt1dateoral = DateTime.Today;
        private DateTime? _attempt2dateoral = DateTime.Today;
        private DateTime _approveddate = DateTime.Today;

        private bool _net = false;
        private int _scholorship = 0;
        private int _physical_handicap = 0;

        private DateTime? _visaExpiryDate = null;
        private DateTime? _passportExpiryDate = null;
        private DateTime? _passportIssueDate = null;
        private DateTime? _stayPermitDate = null;
        private bool _indianOrigin = false;
        private string _agency = string.Empty;
        private string _scholarshipScheme = string.Empty;
        private string _passportIssuePlace = string.Empty;
        private string _citizenship = string.Empty;
        private double _csabAmout = 0.00;

        private int _collegeid = 0;
        private string _class_admited = string.Empty;
        private string _stateof_domecial = string.Empty;

        private string _AllIndiaRollNo = string.Empty;
        private string _StateRollNo = string.Empty;
        private DateTime? _DOR = null;
        private string _motheremail = string.Empty;
        private string _fatheremail = string.Empty;
        private int _claimType = 0;

        private string _workexp = string.Empty;
        private string _designation = string.Empty;
        private string _orglastwork = string.Empty;
        private string _totalworkexp = string.Empty;
        private string _epfno = string.Empty;

        // Added by Abhinay Start's Here
        private int _Secondjoinsupervisorno = 0;

        private int _Secondjoinsupervisormemberno = 0;
        private int _DrcChairNo = 0;
        private int _DrcChairmemberNo = 0;
        private string _SuperRole = string.Empty;
        private string _Research = string.Empty;
        private string _Completecourse = string.Empty;
        private string _ThesisTitleHindi = string.Empty;
        private string _Descipline = string.Empty;
        private decimal _PhddegreeTotalAmount = 0;
        private string _PhdFeesRef = string.Empty;
        private int _PhdPassoutyear = 0;
        private int _PhdConvocationyear = 0;
        private DateTime _PhdConvocationDate = DateTime.Today;
        private string _NADID = string.Empty;
        private int _PhdExaminer5 = 0;
        private int _PhdExaminer5Status = 0;
        private int _PhdExaminer6 = 0;
        private int _PhdExaminer6Status = 0;
        private int _PhdExaminer7 = 0;
        private int _PhdExaminer7Status = 0;
        private int _PhdExaminer8 = 0;
        private int _PhdExaminer8Status = 0;
        private int _PhdExaminer9 = 0;
        private int _PhdExaminer9Status = 0;
        private int _PhdExaminer10 = 0;
        private int _PhdExaminer10Status = 0;
        private int _PhdExaminer1 = 0;
        private int _PhdExaminer1Status = 0;
        private int _PhdExaminer2 = 0;
        private int _PhdExaminer2Status = 0;
        private int _PhdExaminer3 = 0;
        private int _PhdExaminer3Status = 0;
        private int _PhdExaminer4 = 0;
        private int _PhdExaminer4Status = 0;
        private string _PhdPriExaminer1 = string.Empty;
        private string _PhdPriExaminer2 = string.Empty;
        private string _PhdPriExaminer3 = string.Empty;
        private string _PhdPriExaminer4 = string.Empty;
        private string _PhdExaminerFile2 = string.Empty;
        private string _PhdExaminerFile3 = string.Empty;
        private string _PhdExaminerFile4 = string.Empty;
        private string _PhdSynName = string.Empty;
        private string _PhdSynFile = string.Empty;
        private DateTime _PhdPresyndate = DateTime.Today;
        private string _PhdPreSynName = string.Empty;
        private string _PhdPreSynFile = string.Empty;
        private string _PhdStatusValue = string.Empty;
        private string _PhdDegreeRemark = string.Empty;
        private int _special = 0;
        private string _ThesisTitle = string.Empty;
        private string _PhdExaminerFile1 = string.Empty;
        private int _scholarship = 0;
        private int _transport = 0;
        private int _hostel = 0;
        // Added by Abhinay End's Here

        #endregion Private Member

        //Added by neha 02/10/19

        #region "Sports"

        private int _sportsNo;
        private string _sportsType;
        private string _sportsName;
        private DateTime _sportsDate;
        private string _sportsVenue;
        private int _participationNo;
        private int _achievementNo;
        private int _IDNO;

        // private DateTime _entryDate;
        private string _sportsFilepath;

        private string _sportsFilename;
        private int _gameNo;

        #endregion "Sports"

        #region "NCC"

        private int _nccNo;
        private int _nccTypeNo;
        private int _nccRationNo;
        private string _campName;
        private string _Location;

        //private int _IDNO;
        private DateTime _campFromDate;

        private DateTime _campToDate;
        private decimal _duration;

        // private DateTime _entryDate;
        private string _nccFilepath;

        private string _nccFilename;
        private int _nccDocsNo;

        #endregion "NCC"

        #region Public Property Fields

        public int Scholarship
        {
            get { return _scholarship; }
            set { _scholarship = value; }
        }

        public string EpfNo
        {
            get { return _epfno; }
            set { _epfno = value; }
        }

        public string TotalWorkExp
        {
            get { return _totalworkexp; }
            set { _totalworkexp = value; }
        }

        public string OrgLastWork
        {
            get { return _orglastwork; }
            set { _orglastwork = value; }
        }

        public string Designation
        {
            get { return _designation; }
            set { _designation = value; }
        }

        public string WorkExp
        {
            get { return _workexp; }
            set { _workexp = value; }
        }

        public string Annual_income
        {
            get { return _annual_income; }
            set { _annual_income = value; }
        }

        public string CollegeJss
        {
            get { return _collegejss; }
            set { _collegejss = value; }
        }

        public string Cetorderno
        {
            get { return _cetorderno; }
            set { _cetorderno = value; }
        }

        public string Cetdate
        {
            get { return _cetdate; }
            set { _cetdate = value; }
        }

        public string Cetamount
        {
            get { return _cetamount; }
            set { _cetamount = value; }
        }

        public string AllIndiaRollNo
        {
            get { return _AllIndiaRollNo; }
            set { _AllIndiaRollNo = value; }
        }

        public string StateRollNo
        {
            get { return _StateRollNo; }
            set { _StateRollNo = value; }
        }

        public DateTime? DOR
        {
            get { return _DOR; }
            set { _DOR = value; }
        }

        public int ClaimType
        {
            get { return _claimType; }
            set { _claimType = value; }
        }

        public int Physical_Handicap
        {
            get { return _physical_handicap; }
            set { _physical_handicap = value; }
        }

        public string fatherfirstName
        {
            get { return _fatherfirstName; }
            set { _fatherfirstName = value; }
        }

        public string AddharcardNo
        {
            get { return _addharcardno; }
            set { _addharcardno = value; }
        }

        public int IdType
        {
            get { return _idType; }
            set { _idType = value; }
        }

        public string QexmRollNo
        {
            get { return _qexmRollNo; }
            set { _qexmRollNo = value; }
        }

        public string YearOfExam
        {
            get { return _yearOfExam; }
            set { _yearOfExam = value; }
        }

        public decimal Percentile
        {
            get { return _percentile; }
            set { _percentile = value; }
        }

        public decimal Percentage
        {
            get { return _percentage; }
            set { _percentage = value; }
        }

        public string Qualifyexamname
        {
            get { return _qualifyexamname; }
            set { _qualifyexamname = value; }
        }

        public string EmailID
        {
            get { return _emailID; }
            set { _emailID = value; }
        }

        public string RegNo
        {
            get { return _regNo; }
            set { _regNo = value; }
        }

        public string OtherLanguage
        {
            get { return _otherLanguage; }
            set { _otherLanguage = value; }
        }

        public int MToungeNo
        {
            get { return _mToungeNo; }
            set { _mToungeNo = value; }
        }

        public decimal Weight
        {
            get { return _weight; }
            set { _weight = value; }
        }

        public string BirthPlace
        {
            get { return _birthPlace; }
            set { _birthPlace = value; }
        }

        public string Birthvillage
        {
            get { return _birthvillage; }
            set { _birthvillage = value; }
        }

        public string Birthtaluka
        {
            get { return _birthtaluka; }
            set { _birthtaluka = value; }
        }

        public string Birthdistrict
        {
            get { return _birthdistrict; }
            set { _birthdistrict = value; }
        }

        public string Birthdistate
        {
            get { return _birthdistate; }
            set { _birthdistate = value; }
        }

        public decimal Height
        {
            get { return _height; }
            set { _height = value; }
        }

        public int IdNo
        {
            get { return _idNo; }
            set { _idNo = value; }
        }

        public string PCity
        {
            get { return _pCity; }
            set { _pCity = value; }
        }

        public string RollNo
        {
            get { return _rollNo; }
            set { _rollNo = value; }
        }

        public int SectionNo
        {
            get { return _sectionNo; }
            set { _sectionNo = value; }
        }

        public string StudName
        {
            get { return _studName; }
            set { _studName = value; }
        }

        public string MiddleName
        {
            get { return _MiddleName; }
            set { _MiddleName = value; }
        }

        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }

        public string StudNameHindi
        {
            get { return _studNameHindi; }
            set { _studNameHindi = value; }
        }

        public string firstName
        {
            get { return _firstName; }
            set { _firstName = value; }
        }

        public string FatherName
        {
            get { return _fatherName; }
            set { _fatherName = value; }
        }

        public string FatherMiddleName
        {
            get { return _fatherMiddleName; }
            set { _fatherMiddleName = value; }
        }

        public string FatherLastName
        {
            get { return _fatherLastName; }
            set { _fatherLastName = value; }
        }

        public string MotherName
        {
            get { return _motherName; }
            set { _motherName = value; }
        }

        public string FatherMobile
        {
            get { return _fatherMobile; }
            set { _fatherMobile = value; }
        }

        public string MotherMobile
        {
            get { return _motherMobile; }
            set { _motherMobile = value; }
        }

        public string FatherOfficeNo
        {
            get { return _fatherOfficeNo; }
            set { _fatherOfficeNo = value; }
        }

        public string MotherOfficeNo
        {
            get { return _motherMobile; }
            set { _motherMobile = value; }
        }

        public DateTime Dob
        {
            get { return _dob; }
            set { _dob = value; }
        }

        public string Age
        {
            get { return _Age; }
            set { _Age = value; }
        }

        public char Sex
        {
            get { return _sex; }
            set { _sex = value; }
        }

        public int ReligionNo
        {
            get { return _religionNo; }
            set { _religionNo = value; }
        }

        public char Married
        {
            get { return _married; }
            set { _married = value; }
        }

        public int NationalityNo
        {
            get { return _nationalityNo; }
            set { _nationalityNo = value; }
        }

        public int CategoryNo
        {
            get { return _categoryNo; }
            set { _categoryNo = value; }
        }

        public int AdmCategoryNo
        {
            get { return _admcategoryNo; }
            set { _admcategoryNo = value; }
        }

        // address info
        public string PAddress
        {
            get { return _pAddress; }
            set { _pAddress = value; }
        }

        public int PState
        {
            get { return _pState; }
            set { _pState = value; }
        }

        public string BirthPinCode
        {
            get { return _birthPinCode; }
            set { _birthPinCode = value; }
        }

        public string Specialization
        {
            get { return _Specialization; }
            set { _Specialization = value; }
        }

        public string PPinCode
        {
            get { return _pPinCode; }
            set { _pPinCode = value; }
        }

        public string PMobile
        {
            get { return _pMobile; }
            set { _pMobile = value; }
        }

        // admission details
        public DateTime AdmDate
        {
            get { return _admDate; }
            set { _admDate = value; }
        }

        public bool Hosteler
        {
            get { return _hosteler; }
            set { _hosteler = value; }
        }

        public int DegreeNo
        {
            get { return _degreeNo; }
            set { _degreeNo = value; }
        }

        public int BatchNo
        {
            get { return _batchNo; }
            set { _batchNo = value; }
        }

        public int BranchNo
        {
            get { return _branchNo; }
            set { _branchNo = value; }
        }

        public int ClassYear
        {
            get { return _classYear; }
            set { _classYear = value; }
        }

        public int SemesterNo
        {
            get { return _semesterNo; }
            set { _semesterNo = value; }
        }

        public int PType
        {
            get { return _pType; }
            set { _pType = value; }
        }

        public int ExamPtype  // only for gec project
        {
            get { return _examPtype; }
            set { _examPtype = value; }
        }

        public int StateNo
        {
            get { return _stateNo; }
            set { _stateNo = value; }
        }

        public string CollegeCode
        {
            get { return _collegeCode; }
            set { _collegeCode = value; }
        }

        public string IdentyMark
        {
            get { return _identyMark; }
            set { _identyMark = value; }
        }

        public int BloodGroupNo
        {
            get { return _bloodGroupNo; }
            set { _bloodGroupNo = value; }
        }

        public int Caste
        {
            get { return _caste; }
            set { _caste = value; }
        }

        public string Subcaste
        {
            get { return _subcaste; }
            set { _subcaste = value; }
        }

        public string CountryDomicile
        {
            get { return _countryDomicile; }
            set { _countryDomicile = value; }
        }

        public bool Irregular
        {
            get { return _irregular; }
            set { _irregular = value; }
        }

        public bool Urban
        {
            get { return _urban; }
            set { _urban = value; }
        }

        public int Year
        {
            get { return _year; }
            set { _year = value; }
        }

        public bool Pro
        {
            get { return _pro; }
            set { _pro = value; }
        }

        public int SchemeNo
        {
            get { return _schemeNo; }
            set { _schemeNo = value; }
        }

        public string LastRollNo
        {
            get { return _lastRollNo; }
            set { _lastRollNo = value; }
        }

        public int Roll2
        {
            get { return _roll2; }
            set { _roll2 = value; }
        }

        public string AccNo
        {
            get { return _accNo; }
            set { _accNo = value; }
        }

        public string Visano
        {
            get { return _visano; }
            set { _visano = value; }
        }

        public string PassportNo
        {
            get { return _passportNo; }
            set { _passportNo = value; }
        }

        //public string EmailID
        //{
        //    get { return _emailID; }
        //    set { _emailID = value; }
        //}
        //public bool IdType
        //{
        //    get { return _idType; }
        //    set { _idType = value; }
        //}
        public bool AdmCancel
        {
            get { return _admCancel; }
            set { _admCancel = value; }
        }

        public int AdmBatch
        {
            get { return _admBatch; }
            set { _admBatch = value; }
        }

        public DateTime LeaveDate
        {
            get { return _leaveDate; }
            set { _leaveDate = value; }
        }

        public bool Can
        {
            get { return _can; }
            set { _can = value; }
        }

        public DateTime CanDate
        {
            get { return _canDate; }
            set { _canDate = value; }
        }

        public string Remark
        {
            get { return _remark; }
            set { _remark = value; }
        }

        public int FacAdvisor
        {
            get { return _facAdvisor; }
            set { _facAdvisor = value; }
        }

        public string Prjname
        {
            get { return _prjname; }
            set { _prjname = value; }
        }

        public int Uano
        {
            get { return uano; }
            set { uano = value; }
        }

        public string SchemeType
        {
            get { return _schemeType; }
            set { _schemeType = value; }
        }

        public QualifiedExam[] LastQualifiedExams
        {
            get { return _lastQualExams; }
            set { _lastQualExams = value; }
        }

        public string StudId
        {
            get { return _studId; }
            set { _studId = value; }
        }

        public int AdmroundNo
        {
            get { return _admroundNo; }
            set { _admroundNo = value; }
        }

        public int Shift
        {
            get { return _shift; }
            set { _shift = value; }
        }

        public string StudentMobile
        {
            get { return _studentMobile; }
            set { _studentMobile = value; }
        }

        public int Cap_Institute
        {
            get { return _cap_Institute; }
            set { _cap_Institute = value; }
        }

        public string SerialNo
        {
            get { return _serialNo; }
            set { _serialNo = value; }
        }

        public DateTime SaleDate
        {
            get { return _saleDate; }
            set { _saleDate = value; }
        }

        public string Amount
        {
            get { return _amount; }
            set { _amount = value; }
        }

        public int SessionNo
        {
            get { return _sessionNo; }
            set { _sessionNo = value; }
        }

        public string IPADDRESS
        {
            get
            {
                return this._IPADDRESS;
            }
            set
            {
                if ((this._IPADDRESS != value))
                {
                    this._IPADDRESS = value;
                }
            }
        }

        public DateTime PrintDate
        {
            get { return _printDate; }
            set { _printDate = value; }
        }

        public string ReciptNo
        {
            get { return _reciptNo; }
            set { _reciptNo = value; }
        }

        public int NewBranchNo
        {
            get { return _newbranchNo; }
            set { _newbranchNo = value; }
        }

        public int ProsNo
        {
            get { return _prosNo; }
            set { _prosNo = value; }
        }

        public string PDISTRICT
        {
            get { return _District; }
            set { _District = value; }
        }

        public int ALLINDIARANK
        {
            get { return _ALLINDIARANK; }
            set { _ALLINDIARANK = value; }
        }

        public int STATERANK
        {
            get { return _STATERANK; }
            set { _STATERANK = value; }
        }

        public decimal Score
        {
            get { return _score; }
            set { _score = value; }
        }

        public string Paper
        {
            get { return _paper; }
            set { _paper = value; }
        }

        public string Paper_code
        {
            get { return _paper_code; }
            set { _paper_code = value; }
        }

        public decimal PERCENTILE
        {
            get { return _PERCENTILE; }
            set { _PERCENTILE = value; }
        }

        public string QUALIFYNO
        {
            get { return _qualifyNo; }
            set { _qualifyNo = value; }
        }

        public int ADMQUOTANO
        {
            get { return _admquotano; }
            set { _admquotano = value; }
        }

        public string GATE_SCORE
        {
            get { return _gatescore; }
            set { _gatescore = value; }
        }

        public string GATE_REG
        {
            get { return _gatereg; }
            set { _gatereg = value; }
        }

        public string GATE_PAPER
        {
            get { return _gatepaper; }
            set { _gatepaper = value; }
        }

        public string GATE_YEAR
        {
            get { return _gateyear; }
            set { _gateyear = value; }
        }

        public string PH
        {
            get { return _ph; }
            set { _ph = value; }
        }

        public string DOCUMENTS
        {
            get { return _documents; }
            set { _documents = value; }
        }

        public int PayTypeNO
        {
            get { return _paytypeno; }
            set { _paytypeno = value; }
        }

        public int GetScholarship
        {
            get { return _gatescholarship; }
            set { _gatescholarship = value; }
        }

        public byte[] StudSign
        {
            get { return _studsign; }
            set { _studsign = value; }
        }

        public byte[] StudPhoto
        {
            get { return _studphoto; }
            set { _studphoto = value; }
        }

        public int BankNo
        {
            get { return _bankno; }
            set { _bankno = value; }
        }

        public string EnrollNo
        {
            get { return _enrollNo; }
            set { _enrollNo = value; }
        }

        public int PhdStatus
        {
            get { return _phdstatus; }
            set { _phdstatus = value; }
        }

        public int PhdSupervisorNo
        {
            get { return _phdsupervisorno; }
            set { _phdsupervisorno = value; }
        }

        public int PhdCoSupervisorNo1
        {
            get { return _phdcosupervisorno1; }
            set { _phdcosupervisorno1 = value; }
        }

        public int PhdCoSupervisorNo2
        {
            get { return _phdcosupervisorno2; }
            set { _phdcosupervisorno2 = value; }
        }

        public int TypeSupervisorNo
        {
            get { return _typesupervisorno; }
            set { _typesupervisorno = value; }
        }

        public int TypeCoSupervisorNo1
        {
            get { return _typecosupervisorno1; }
            set { _typecosupervisorno1 = value; }
        }

        public int TypeCoSupervisorNo2
        {
            get { return _typecosupervisorno2; }
            set { _typecosupervisorno2 = value; }
        }

        public int CheckersdetailNo
        {
            get { return _checkersdetailno; }
            set { _checkersdetailno = value; }
        }

        public int CheckerNo1
        {
            get { return _checkerno1; }
            set { _checkerno1 = value; }
        }

        public int CheckerNo2
        {
            get { return _checkerno2; }
            set { _checkerno2 = value; }
        }

        public int CollatorNo1
        {
            get { return _collatorno1; }
            set { _collatorno1 = value; }
        }

        public int CollatorNo2
        {
            get { return _collatorno2; }
            set { _collatorno2 = value; }
        }

        public string CheckerName1
        {
            get { return _checkername1; }
            set { _checkername1 = value; }
        }

        public string CheckerName2
        {
            get { return _checkername2; }
            set { _checkername2 = value; }
        }

        public bool Net
        {
            get { return _net; }
            set { _net = value; }
        }

        public string Corres_address
        {
            get { return _corres_address; }
            set { _corres_address = value; }
        }

        public string Corres_pin
        {
            get { return _corres_pin; }
            set { _corres_pin = value; }
        }

        public string Corres_mob
        {
            get { return _corres_mob; }
            set { _corres_mob = value; }
        }

        public string Stateotherstate
        {
            get { return _stateotherstate; }
            set { _stateotherstate = value; }
        }

        public int Yearsofstudey
        {
            get { return _yearsofstudey; }
            set { _yearsofstudey = value; }
        }

        public string Colg_address
        {
            get { return _colg_address; }
            set { _colg_address = value; }
        }

        public int SupervisorNo
        {
            get { return _supervisorno; }
            set { _supervisorno = value; }
        }

        public int SupervisormemberNo
        {
            get { return _supervisormemberno; }
            set { _supervisormemberno = value; }
        }

        public int SupervisorStatus
        {
            get { return _supervisorstatus; }
            set { _supervisorstatus = value; }
        }

        public int JoinsupervisorNo
        {
            get { return _joinsupervisorno; }
            set { _joinsupervisorno = value; }
        }

        public int JoinsupervisormemberNo
        {
            get { return _joinsupervisormemberno; }
            set { _joinsupervisormemberno = value; }
        }

        public int JoinsupervisorStatus
        {
            get { return _joinsupervisorstatus; }
            set { _joinsupervisorstatus = value; }
        }

        public int InstitutefacultyNo
        {
            get { return _institutefacultyno; }
            set { _institutefacultyno = value; }
        }

        public int InstitutefacmemberNo
        {
            get { return _institutefacmemberno; }
            set { _institutefacmemberno = value; }
        }

        public int InstitutefacultyStatus
        {
            get { return _institutefacultystatus; }
            set { _institutefacultystatus = value; }
        }

        public int DrcNo
        {
            get { return _drcno; }
            set { _drcno = value; }
        }

        public int DrcmemberNo
        {
            get { return _drcmemberno; }
            set { _drcmemberno = value; }
        }

        public int Drcstatus
        {
            get { return _drcstatus; }
            set { _drcstatus = value; }
        }

        public int Phdstatuscat
        {
            get { return _phdstatuscat; }
            set { _phdstatuscat = value; }
        }

        public int Credits
        {
            get { return _credits; }
            set { _credits = value; }
        }

        public string Topics
        {
            get { return _topics; }
            set { _topics = value; }
        }

        public string Workdone
        {
            get { return _workdone; }
            set { _workdone = value; }
        }

        public string Phdremark
        {
            get { return _phdremark; }
            set { _phdremark = value; }
        }

        public int Grade
        {
            get { return _grade; }
            set { _grade = value; }
        }

        public int Scholorship
        {
            get { return _scholorship; }
            set { _scholorship = value; }
        }

        public DateTime Attempt1DateWritten
        {
            get { return _attempt1datewritten; }
            set { _attempt1datewritten = value; }
        }

        public DateTime? Attempt2DateWritten
        {
            get { return _attempt2datewritten; }
            set { _attempt2datewritten = value; }
        }

        public DateTime Attempt1DateOral
        {
            get { return _attempt1dateoral; }
            set { _attempt1dateoral = value; }
        }

        public DateTime? Attempt2DateOral
        {
            get { return _attempt2dateoral; }
            set { _attempt2dateoral = value; }
        }

        public DateTime ApprovedDate
        {
            get { return _approveddate; }
            set { _approveddate = value; }
        }

        public DateTime? VisaExpiryDate
        {
            get { return _visaExpiryDate; }
            set { _visaExpiryDate = value; }
        }

        public DateTime? PassportExpiryDate
        {
            get { return _passportExpiryDate; }
            set { _passportExpiryDate = value; }
        }

        public DateTime? PassportIssueDate
        {
            get { return _passportIssueDate; }
            set { _passportIssueDate = value; }
        }

        public DateTime? StayPermitDate
        {
            get { return _stayPermitDate; }
            set { _stayPermitDate = value; }
        }

        public bool IndianOrigin
        {
            get { return _indianOrigin; }
            set { _indianOrigin = value; }
        }

        public string Agency
        {
            get { return _agency; }
            set { _agency = value; }
        }

        public string ScholarshipScheme
        {
            get { return _scholarshipScheme; }
            set { _scholarshipScheme = value; }
        }

        public string PassportIssuePlace
        {
            get { return _passportIssuePlace; }
            set { _passportIssuePlace = value; }
        }

        public string Citizenship
        {
            get { return _citizenship; }
            set { _citizenship = value; }
        }

        public double CsabAmount
        {
            get { return _csabAmout; }
            set { _csabAmout = value; }
        }

        public int Collegeid
        {
            get { return _collegeid; }
            set { _collegeid = value; }
        }

        public string Class_admited
        {
            get { return _class_admited; }
            set { _class_admited = value; }
        }

        public string Stateof_domecial
        {
            get { return _stateof_domecial; }
            set { _stateof_domecial = value; }
        }

        public int College_ID
        {
            get { return _college_id; }
            set { _college_id = value; }
        }

        public string Motheremail
        {
            get { return _motheremail; }
            set { _motheremail = value; }
        }

        public string Fatheremail
        {
            get { return _fatheremail; }
            set { _fatheremail = value; }
        }

        // pg entrance exam scores

        public int PGQUALIFYNO
        {
            get { return _PGQUALIFYNO; }
            set { _PGQUALIFYNO = value; }
        }

        public string PGENTROLLNO
        {
            get { return _PGENTROLLNO; }
            set { _PGENTROLLNO = value; }
        }

        public decimal pgpercentage
        {
            get { return _pgpercentage; }
            set { _pgpercentage = value; }
        }

        public decimal pgpercentile
        {
            get { return _pgpercentile; }
            set { _pgpercentile = value; }
        }

        public string pgyearOfExam
        {
            get { return _pgyearOfExam; }
            set { _pgyearOfExam = value; }
        }

        public int PGRANK
        {
            get { return _PGRANK; }
            set { _PGRANK = value; }
        }

        public decimal pgscore
        {
            get { return _pgscore; }
            set { _pgscore = value; }
        }

        #endregion Public Property Fields

        #region "Sports"

        public int sportsNo
        {
            get { return _sportsNo; }
            set { _sportsNo = value; }
        }

        public string sportsType
        {
            get { return _sportsType; }
            set { _sportsType = value; }
        }

        public string sportsName
        {
            get { return _sportsName; }
            set { _sportsName = value; }
        }

        public DateTime sportsDate
        {
            get { return _sportsDate; }
            set { _sportsDate = value; }
        }

        public string sportsVenue
        {
            get { return _sportsVenue; }
            set { _sportsVenue = value; }
        }

        public int participationNo
        {
            get { return _participationNo; }
            set { _participationNo = value; }
        }

        public int achievementNo
        {
            get { return _achievementNo; }
            set { _achievementNo = value; }
        }

        public int idno
        {
            get { return _IDNO; }
            set { _IDNO = value; }
        }

        //public DateTime entryDate
        //{
        //    get { return _entryDate; }
        //    set { _entryDate = value; }
        //}

        public string sportsFilepath
        {
            get { return _sportsFilepath; }
            set { _sportsFilepath = value; }
        }

        public string sportsFilename
        {
            get { return _sportsFilename; }
            set { _sportsFilename = value; }
        }

        public int gameNo
        {
            get { return _gameNo; }
            set { _gameNo = value; }
        }

        #endregion "Sports"

        #region "NCC"

        public int nccNo
        {
            get { return _nccNo; }
            set { _nccNo = value; }
        }

        public int nccTypeNo
        {
            get { return _nccTypeNo; }
            set { _nccTypeNo = value; }
        }

        public int nccRationNo
        {
            get { return _nccRationNo; }
            set { _nccRationNo = value; }
        }

        public DateTime campFromDate
        {
            get { return _campFromDate; }
            set { _campFromDate = value; }
        }

        public DateTime campToDate
        {
            get { return _campToDate; }
            set { _campToDate = value; }
        }

        public string campName
        {
            get { return _campName; }
            set { _campName = value; }
        }

        public string Location
        {
            get { return _Location; }
            set { _Location = value; }
        }

        public decimal duration
        {
            get { return _duration; }
            set { _duration = value; }
        }

        //public DateTime entryDate
        //{
        //    get { return _entryDate; }
        //    set { _entryDate = value; }
        //}

        public string nccFilepath
        {
            get { return _nccFilepath; }
            set { _nccFilepath = value; }
        }

        public string nccFilename
        {
            get { return _nccFilename; }
            set { _nccFilename = value; }
        }

        public int nccDocsNo
        {
            get { return _nccDocsNo; }
            set { _nccDocsNo = value; }
        }

        #endregion "NCC"

        private int _userno = 0;
        private string _studfirstname = string.Empty;
        private string _studlastname = string.Empty;
        private string _name_with_initial = string.Empty;
        private string _emailid = string.Empty;
        private string _studmobile = string.Empty;
        private string _telphoneno = string.Empty;
        private string _nicno = string.Empty;
        private string _passportno = string.Empty;
        private string _stud_dob = string.Empty;
        private string _stud_sex = string.Empty;
        private string _stud_citizenship = string.Empty;
        private string _identi_mark = string.Empty;

        //Student Address
        private string _paddress = string.Empty;

        private string _pcountry = string.Empty;
        private string _pprovince = string.Empty;
        private string _pdistrict = string.Empty;

        //Student Education Details
        private int _altype = 0;

        private int _alstreamno = 0;
        private int _alattemptno = 0;
        private string _alsubject1 = string.Empty;
        private string _algrade1 = string.Empty;
        private string _alsubject2 = string.Empty;
        private string _algrade2 = string.Empty;
        private string _alsubject3 = string.Empty;
        private string _algrade3 = string.Empty;
        private string _alsubject4 = string.Empty;
        private string _algrade4 = string.Empty;
        private int _oltype = 0;
        private int _olstreamno = 0;
        private int _olattemptno = 0;
        private string _olsubject1 = string.Empty;
        private string _olgrade1 = string.Empty;
        private string _olsubject2 = string.Empty;
        private string _olgrade2 = string.Empty;
        private string _olsubject3 = string.Empty;
        private string _olgrade3 = string.Empty;
        private string _olsubject4 = string.Empty;
        private string _olgrade4 = string.Empty;
        private string _olsubject5 = string.Empty;
        private string _olgrade5 = string.Empty;
        private string _olsubject6 = string.Empty;
        private string _olgrade6 = string.Empty;

        //Faculty Details
        private int _stud_college_id = 0;

        private int _degreeno = 0;
        private int _branchno = 0;
        private int _campusno = 0;
        private int _weeksnos = 0;
        private int _semesterno = 0;
        private int _admbatch = 0;

        private string _password = string.Empty;
        private int _ua_no = 0;
        private string _stud_remark = string.Empty;
        private string _mobilecode = string.Empty;
        private string _hometelephonecode = string.Empty;

        // Added By Abhinay Lad Start's Here
        public int Transport
        {
            get { return _transport; }
            set { _transport = value; }
        }

        public int Hostel
        {
            get { return _hostel; }
            set { _hostel = value; }
        }

        public int Secondjoinsupervisorno
        {
            get { return _Secondjoinsupervisorno; }
            set { _Secondjoinsupervisorno = value; }
        }

        public int Secondjoinsupervisormemberno
        {
            get { return _Secondjoinsupervisormemberno; }
            set { _Secondjoinsupervisormemberno = value; }
        }

        public int DrcChairNo
        {
            get { return _DrcChairNo; }
            set { _DrcChairNo = value; }
        }

        public int DrcChairmemberNo
        {
            get { return _DrcChairmemberNo; }
            set { _DrcChairmemberNo = value; }
        }

        public string SuperRole
        {
            get { return _SuperRole; }
            set { _SuperRole = value; }
        }

        public string Research
        {
            get { return _Research; }
            set { _Research = value; }
        }

        public string Completecourse
        {
            get { return _Completecourse; }
            set { _Completecourse = value; }
        }

        public string ThesisTitleHindi
        {
            get { return _ThesisTitleHindi; }
            set { _ThesisTitleHindi = value; }
        }

        public string Descipline
        {
            get { return _Descipline; }
            set { _Descipline = value; }
        }

        public Decimal PhddegreeTotalAmount
        {
            get { return _PhddegreeTotalAmount; }
            set { _PhddegreeTotalAmount = value; }
        }

        public string PhdFeesRef
        {
            get { return _PhdFeesRef; }
            set { _PhdFeesRef = value; }
        }

        public int PhdPassoutyear
        {
            get { return _PhdPassoutyear; }
            set { _PhdPassoutyear = value; }
        }

        public int PhdConvocationyear
        {
            get { return _PhdConvocationyear; }
            set { _PhdConvocationyear = value; }
        }

        public DateTime PhdConvocationDate
        {
            get { return _PhdConvocationDate; }
            set { _PhdConvocationDate = value; }
        }

        public string NADID
        {
            get { return _NADID; }
            set { _NADID = value; }
        }

        public int PhdExaminer5
        {
            get { return _PhdExaminer5; }
            set { _PhdExaminer5 = value; }
        }

        public int PhdExaminer6
        {
            get { return _PhdExaminer6; }
            set { _PhdExaminer6 = value; }
        }

        public int PhdExaminer7
        {
            get { return _PhdExaminer7; }
            set { _PhdExaminer7 = value; }
        }

        public int PhdExaminer8
        {
            get { return _PhdExaminer8; }
            set { _PhdExaminer8 = value; }
        }

        public int PhdExaminer9
        {
            get { return _PhdExaminer9; }
            set { _PhdExaminer9 = value; }
        }

        public int PhdExaminer10
        {
            get { return _PhdExaminer10; }
            set { _PhdExaminer10 = value; }
        }

        public string PhdSynName
        {
            get { return _PhdSynName; }
            set { _PhdSynName = value; }
        }

        public string PhdSynFile
        {
            get { return _PhdSynFile; }
            set { _PhdSynFile = value; }
        }

        public DateTime PhdPresyndate
        {
            get { return _PhdPresyndate; }
            set { _PhdPresyndate = value; }
        }

        public string PhdPreSynName
        {
            get { return _PhdPreSynName; }
            set { _PhdPreSynName = value; }
        }

        public string PhdPreSynFile
        {
            get { return _PhdPreSynFile; }
            set { _PhdPreSynFile = value; }
        }

        public string PhdStatusValue
        {
            get { return _PhdStatusValue; }
            set { _PhdStatusValue = value; }
        }

        public int PhdExaminer5Status
        {
            get { return _PhdExaminer5Status; }
            set { _PhdExaminer5Status = value; }
        }

        public int PhdExaminer6Status
        {
            get { return _PhdExaminer6Status; }
            set { _PhdExaminer6Status = value; }
        }

        public int PhdExaminer7Status
        {
            get { return _PhdExaminer7Status; }
            set { _PhdExaminer7Status = value; }
        }

        public int PhdExaminer8Status
        {
            get { return _PhdExaminer8Status; }
            set { _PhdExaminer8Status = value; }
        }

        public int PhdExaminer9Status
        {
            get { return _PhdExaminer9Status; }
            set { _PhdExaminer9Status = value; }
        }

        public int PhdExaminer10Status
        {
            get { return _PhdExaminer10Status; }
            set { _PhdExaminer10Status = value; }
        }

        public string PhdDegreeRemark
        {
            get { return _PhdDegreeRemark; }
            set { _PhdDegreeRemark = value; }
        }

        public int PhdExaminer1
        {
            get { return _PhdExaminer1; }
            set { _PhdExaminer1 = value; }
        }

        public int PhdExaminer2
        {
            get { return _PhdExaminer2; }
            set { _PhdExaminer2 = value; }
        }

        public int PhdExaminer3
        {
            get { return _PhdExaminer3; }
            set { _PhdExaminer3 = value; }
        }

        public int PhdExaminer4
        {
            get { return _PhdExaminer4; }
            set { _PhdExaminer4 = value; }
        }

        public string PhdPriExaminer1
        {
            get { return _PhdPriExaminer1; }
            set { _PhdPriExaminer1 = value; }
        }

        public string PhdPriExaminer2
        {
            get { return _PhdPriExaminer2; }
            set { _PhdPriExaminer2 = value; }
        }

        public string PhdPriExaminer3
        {
            get { return _PhdPriExaminer3; }
            set { _PhdPriExaminer3 = value; }
        }

        public string PhdPriExaminer4
        {
            get { return _PhdPriExaminer4; }
            set { _PhdPriExaminer4 = value; }
        }

        public string PhdExaminerFile2
        {
            get { return _PhdExaminerFile2; }
            set { _PhdExaminerFile2 = value; }
        }

        public string PhdExaminerFile3
        {
            get { return _PhdExaminerFile3; }
            set { _PhdExaminerFile3 = value; }
        }

        public string PhdExaminerFile4
        {
            get { return _PhdExaminerFile4; }
            set { _PhdExaminerFile4 = value; }
        }

        public int PhdExaminer1Status
        {
            get { return _PhdExaminer1Status; }
            set { _PhdExaminer1Status = value; }
        }

        public int PhdExaminer2Status
        {
            get { return _PhdExaminer2Status; }
            set { _PhdExaminer2Status = value; }
        }

        public int PhdExaminer3Status
        {
            get { return _PhdExaminer3Status; }
            set { _PhdExaminer3Status = value; }
        }

        public int PhdExaminer4Status
        {
            get { return _PhdExaminer4Status; }
            set { _PhdExaminer4Status = value; }
        }

        public int Special
        {
            get { return _special; }
            set { _special = value; }
        }

        public string ThesisTitle
        {
            get { return _ThesisTitle; }
            set { _ThesisTitle = value; }
        }

        public string PhdExaminerFile1
        {
            get { return _PhdExaminerFile1; }
            set { _PhdExaminerFile1 = value; }
        }

        // Added By Abhinay Lad End's Here

        //Student Personal Details
        public int userno
        {
            get { return _userno; }
            set { _userno = value; }
        }

        public string studfirstname
        {
            get { return _studfirstname; }
            set { _studfirstname = value; }
        }

        public string studlastname
        {
            get { return _studlastname; }
            set { _studlastname = value; }
        }

        public string name_with_initial
        {
            get { return _name_with_initial; }
            set { _name_with_initial = value; }
        }

        public string emailid
        {
            get { return _emailid; }
            set { _emailid = value; }
        }

        public string studmobile
        {
            get { return _studmobile; }
            set { _studmobile = value; }
        }

        public string telphoneno
        {
            get { return _telphoneno; }
            set { _telphoneno = value; }
        }

        public string nicno
        {
            get { return _nicno; }
            set { _nicno = value; }
        }

        public string passportno
        {
            get { return _passportno; }
            set { _passportno = value; }
        }

        public string stud_dob
        {
            get { return _stud_dob; }
            set { _stud_dob = value; }
        }

        public string stud_sex
        {
            get { return _stud_sex; }
            set { _stud_sex = value; }
        }

        public string stud_citizenship
        {
            get { return _stud_citizenship; }
            set { _stud_citizenship = value; }
        }

        public string identi_mark
        {
            get { return _identi_mark; }
            set { _identi_mark = value; }
        }

        //Student Address
        public string paddress
        {
            get { return _paddress; }
            set { _paddress = value; }
        }

        public string pcountry
        {
            get { return _pcountry; }
            set { _pcountry = value; }
        }

        public string pprovince
        {
            get { return _pprovince; }
            set { _pprovince = value; }
        }

        public string pdistrict
        {
            get { return _pdistrict; }
            set { _pdistrict = value; }
        }

        //Student Education Details
        public int altype
        {
            get { return _altype; }
            set { _altype = value; }
        }

        public int alstreamno
        {
            get { return _alstreamno; }
            set { _alstreamno = value; }
        }

        public int alattemptno
        {
            get { return _alattemptno; }
            set { _alattemptno = value; }
        }

        public string alsubject1
        {
            get { return _alsubject1; }
            set { _alsubject1 = value; }
        }

        public string algrade1
        {
            get { return _algrade1; }
            set { _algrade1 = value; }
        }

        public string alsubject2
        {
            get { return _alsubject2; }
            set { _alsubject2 = value; }
        }

        public string algrade2
        {
            get { return _algrade2; }
            set { _algrade2 = value; }
        }

        public string alsubject3
        {
            get { return _alsubject3; }
            set { _alsubject3 = value; }
        }

        public string algrade3
        {
            get { return _algrade3; }
            set { _algrade3 = value; }
        }

        public string alsubject4
        {
            get { return _alsubject4; }
            set { _alsubject4 = value; }
        }

        public string algrade4
        {
            get { return _algrade4; }
            set { _algrade4 = value; }
        }

        public int oltype
        {
            get { return _oltype; }
            set { _oltype = value; }
        }

        public int olstreamno
        {
            get { return _olstreamno; }
            set { _olstreamno = value; }
        }

        public int olattemptno
        {
            get { return _olattemptno; }
            set { _olattemptno = value; }
        }

        public string olsubject1
        {
            get { return _olsubject1; }
            set { _olsubject1 = value; }
        }

        public string olgrade1
        {
            get { return _algrade1; }
            set { _algrade1 = value; }
        }

        public string olsubject2
        {
            get { return _olsubject2; }
            set { _olsubject2 = value; }
        }

        public string olgrade2
        {
            get { return _olgrade2; }
            set { _olgrade2 = value; }
        }

        public string olsubject3
        {
            get { return _olsubject3; }
            set { _olsubject3 = value; }
        }

        public string olgrade3
        {
            get { return _olgrade3; }
            set { _olgrade3 = value; }
        }

        public string olsubject4
        {
            get { return _olsubject4; }
            set { _olsubject4 = value; }
        }

        public string olgrade4
        {
            get { return _olgrade4; }
            set { _olgrade4 = value; }
        }

        public string olsubject5
        {
            get { return _olsubject5; }
            set { _olsubject5 = value; }
        }

        public string olgrade5
        {
            get { return _olgrade5; }
            set { _olgrade5 = value; }
        }

        public string olsubject6
        {
            get { return _olsubject6; }
            set { _olsubject6 = value; }
        }

        public string olgrade6
        {
            get { return _olgrade6; }
            set { _olgrade6 = value; }
        }

        public int stud_college_id
        {
            get { return _stud_college_id; }
            set { _stud_college_id = value; }
        }

        public int degreeno
        {
            get { return _degreeno; }
            set { _degreeno = value; }
        }

        public int branchno
        {
            get { return _branchno; }
            set { _branchno = value; }
        }

        public int campusno
        {
            get { return _campusno; }
            set { _campusno = value; }
        }

        public int weeksnos
        {
            get { return _weeksnos; }
            set { _weeksnos = value; }
        }

        public int semesterno
        {
            get { return _semesterno; }
            set { _semesterno = value; }
        }

        public int admbatch
        {
            get { return _admbatch; }
            set { _admbatch = value; }
        }

        public int ua_no
        {
            get { return _ua_no; }
            set { _ua_no = value; }
        }

        public string password
        {
            get { return _password; }
            set { _password = value; }
        }

        public string stud_remark
        {
            get { return _stud_remark; }
            set { _stud_remark = value; }
        }

        public string mobilecode
        {
            get { return _mobilecode; }
            set { _mobilecode = value; }
        }

        public string hometelephonecode
        {
            get { return _hometelephonecode; }
            set { _hometelephonecode = value; }
        }

        public string IDNO
        {
            get { return _idno; }
            set { _idno = value; }
        }

        public int Courseno
        {
            get { return _courseno; }
            set { _courseno = value; }
        }

        public string Ccode
        {
            get { return _ccode; }
            set { _ccode = value; }
        }
    }
}